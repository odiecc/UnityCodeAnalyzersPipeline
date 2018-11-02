using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using UnityEngine;
using UnityEditor;

using SyntaxTree.VisualStudio.Unity.Bridge;

[InitializeOnLoad]
public class ProjectFileHook
{
    class Utf8StringWriter : StringWriter
    { 
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }

    static ProjectFileHook()
    {
        ProjectFilesGenerator.ProjectFileGeneration += (string name, string content) =>
        {
            var document = XDocument.Parse(content);
            var str = new Utf8StringWriter();
            string path = Path.Combine(Application.dataPath, "Plugins", "x86", AnalyzerUtility.ANALYZER_NAME);
            if (File.Exists(path))
            {
                XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
                XElement itemGroup = new XElement(ns + "ItemGroup");
                XElement Analyzer = new XElement(ns + "Analyzer", new XAttribute("Include", "Assets\\Plugins\\x86\\" + AnalyzerUtility.ANALYZER_NAME));
                itemGroup.Add(Analyzer);
                document.Root.Add(itemGroup);
            }
            document.Save(str);
            return str.ToString();
        };
    }
}