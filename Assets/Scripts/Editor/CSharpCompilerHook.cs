using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.IO;

[InitializeOnLoad]
public class CSharpCompilerHook 
{
    private static MethodHooker _hooker;
    static CSharpCompilerHook()
    { 
        Type type = Type.GetType("UnityEditor.Scripting.Compilers.MicrosoftCSharpCompiler,UnityEditor.dll");
        MethodInfo method = type.GetMethod("FillCompilerOptions", BindingFlags.NonPublic | BindingFlags.Instance);
        type = typeof(CSharpCompilerHook);
        MethodInfo replace = type.GetMethod("FillCompilerOptionsAnalyzer", BindingFlags.Static | BindingFlags.NonPublic);
        MethodInfo proxy = type.GetMethod("FillCompilerOptions", BindingFlags.Static | BindingFlags.NonPublic);
        _hooker = new MethodHooker(method, replace, proxy);
        _hooker.Install();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void FillCompilerOptionsAnalyzer(object father,List<string> arguments, out string argsPrefix)
    {
        string path = Path.Combine(Application.dataPath,"Plugins","x86" , AnalyzerUtility.ANALYZER_NAME);
        if(File.Exists(path))
        {
            arguments.Add("-a:" + path);
        }
        FillCompilerOptions(father,arguments, out argsPrefix);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void FillCompilerOptions(object father, List<string> arguments, out string argsPrefix)
    {
        argsPrefix = "";
    }
}
