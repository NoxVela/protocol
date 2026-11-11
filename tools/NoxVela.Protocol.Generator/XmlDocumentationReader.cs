using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace NoxVela.Protocol.Generator;

/// <summary>
/// Loads and queries XML documentation generated for the NoxVela.Protocol assembly.
/// </summary>
public static class XmlDocumentationReader
{
    private static readonly ConcurrentDictionary<string, string> _members = new(StringComparer.Ordinal);
    private static bool _initialized;

    public static void Initialize(string rootProtocolDir)
    {
        if (_initialized) return;
        _initialized = true;

        var xmlPath = ResolveXmlDocPath(rootProtocolDir);
        if (xmlPath == null || !File.Exists(xmlPath))
        {
            Console.WriteLine("[XmlDocumentationReader] Warning: NoxVela.Protocol.xml not found. Schemas will be generated without XML doc comments.");
            return;
        }

        try
        {
            Console.WriteLine($"[XmlDocumentationReader] Loading XML documentation from: {xmlPath}");
            var xdoc = XDocument.Load(xmlPath);
            var members = xdoc.Descendants("member");
            int count = 0;

            foreach (var member in members)
            {
                var name = member.Attribute("name")?.Value;
                if (string.IsNullOrWhiteSpace(name)) continue;

                var summary = member.Element("summary")?.Value;
                var remarks = member.Element("remarks")?.Value;

                var rawText = !string.IsNullOrWhiteSpace(summary) ? summary : remarks;
                if (!string.IsNullOrWhiteSpace(rawText))
                {
                    var cleaned = CleanSummary(rawText);
                    if (!string.IsNullOrEmpty(cleaned))
                    {
                        _members[name] = cleaned;
                        count++;
                    }
                }
            }

            Console.WriteLine($"[XmlDocumentationReader] Loaded {count} documented member elements from XML doc.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[XmlDocumentationReader] Warning: Failed to parse XML documentation: {ex.Message}");
        }
    }

    public static string? GetTypeDescription(Type type)
    {
        var typeName = GetNormalizedTypeName(type);
        var key = $"T:{typeName}";
        return _members.TryGetValue(key, out var desc) ? desc : null;
    }

    public static string? GetPropertyDescription(Type type, string propertyName)
    {
        var typeName = GetNormalizedTypeName(type);
        var key = $"P:{typeName}.{propertyName}";
        if (_members.TryGetValue(key, out var desc)) return desc;

        // Walk base class hierarchy
        var current = type.BaseType;
        while (current != null && current != typeof(object))
        {
            var baseName = GetNormalizedTypeName(current);
            var baseKey = $"P:{baseName}.{propertyName}";
            if (_members.TryGetValue(baseKey, out var baseDesc)) return baseDesc;
            current = current.BaseType;
        }

        // Check implemented interfaces
        foreach (var iface in type.GetInterfaces())
        {
            var ifaceName = GetNormalizedTypeName(iface);
            var ifaceKey = $"P:{ifaceName}.{propertyName}";
            if (_members.TryGetValue(ifaceKey, out var ifaceDesc)) return ifaceDesc;
        }

        return null;
    }

    public static string? GetFieldDescription(Type type, string fieldName)
    {
        var typeName = GetNormalizedTypeName(type);
        var key = $"F:{typeName}.{fieldName}";
        return _members.TryGetValue(key, out var desc) ? desc : null;
    }

    private static string GetNormalizedTypeName(Type type)
    {
        var name = type.FullName ?? type.Name;
        // In XML docs nested types are separated with dot, not plus
        return name.Replace('+', '.');
    }

    private static string? ResolveXmlDocPath(string rootProtocolDir)
    {
        // 1. Next to the executing generator assembly
        var localAssemblyPath = Path.Combine(AppContext.BaseDirectory, "NoxVela.Protocol.xml");
        if (File.Exists(localAssemblyPath)) return localAssemblyPath;

        // 2. Direct paths under src/NoxVela.Protocol/bin/
        var candidates = new[]
        {
            Path.Combine(rootProtocolDir, "src", "NoxVela.Protocol", "bin", "Debug", "net10.0", "NoxVela.Protocol.xml"),
            Path.Combine(rootProtocolDir, "src", "NoxVela.Protocol", "bin", "Release", "net10.0", "NoxVela.Protocol.xml"),
            Path.Combine(rootProtocolDir, "src", "NoxVela.Protocol", "bin", "Debug", "netstandard2.1", "NoxVela.Protocol.xml"),
            Path.Combine(rootProtocolDir, "src", "NoxVela.Protocol", "bin", "Release", "netstandard2.1", "NoxVela.Protocol.xml")
        };

        foreach (var cand in candidates)
        {
            if (File.Exists(cand)) return cand;
        }

        // 3. Fallback search inside bin
        var binDir = Path.Combine(rootProtocolDir, "src", "NoxVela.Protocol", "bin");
        if (Directory.Exists(binDir))
        {
            var found = Directory.GetFiles(binDir, "NoxVela.Protocol.xml", SearchOption.AllDirectories).FirstOrDefault();
            if (found != null) return found;
        }

        return null;
    }

    private static string CleanSummary(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;

        // Replace <see cref="..." /> with shortened symbol name
        text = Regex.Replace(text, @"<see\s+cref=""([^""]+)""\s*/>", match =>
        {
            var cref = match.Groups[1].Value;
            if (cref.Length > 2 && cref[1] == ':')
            {
                var kind = cref[0];
                var raw = cref.Substring(2);
                var parts = raw.Split('.');
                if ((kind == 'F' || kind == 'P' || kind == 'M') && parts.Length >= 2)
                {
                    return $"{parts[^2]}.{parts[^1]}";
                }
                return parts[^1];
            }
            return cref;
        });

        // Replace <see langword="..."/> with bare keyword
        text = Regex.Replace(text, @"<see\s+langword=""([^""]+)""\s*/>", "$1");

        // Replace parameter and type parameter references
        text = Regex.Replace(text, @"<paramref\s+name=""([^""]+)""\s*/>", "$1");
        text = Regex.Replace(text, @"<typeparamref\s+name=""([^""]+)""\s*/>", "$1");

        // Replace paragraph tags with space
        text = Regex.Replace(text, @"</?para>", " ");

        // Strip any remaining XML tags
        text = Regex.Replace(text, @"<[^>]+>", " ");

        // Decode HTML/XML entities (&lt;, &gt;, &amp;, etc.)
        text = WebUtility.HtmlDecode(text);

        // Normalize spaces and line endings
        text = Regex.Replace(text, @"\s+", " ").Trim();

        return text;
    }
}
