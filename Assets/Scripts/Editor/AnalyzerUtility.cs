using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyzerUtility
{
    public const string ANALYZER_NAME = "Analyzer.dll";

    private static Func<string, bool>[] Rules = new Func<string, bool>[] {
        (value) =>
        {
            //TODO: value传入的是编译时相关提示信息，跟根据解析决定是否忽略触发提示
            if(value.IndexOf("warning Analyzer") != -1)
                return true;
            return false;
        },

    };

    public static bool isIgnore(string value)
    {
        for (int i = 0; i < Rules.Length; i++)
        {
            if (Rules[i](value))
                return true;
        }
        return false;
    }
}
