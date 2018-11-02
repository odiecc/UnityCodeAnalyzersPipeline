using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;

[InitializeOnLoad]
public class EditorCompiler
{
    private static bool showAlert = false;

    static EditorCompiler()
    {
        CompilationPipeline.assemblyCompilationFinished += (value, messages) =>
        {
            Debug.Log(string.Format("{0}完成编译",value));
            for (int i = 0; i < messages.Length; i++)
            {
                if (AnalyzerUtility.isIgnore(messages[i].message))
                    continue;
                if (messages[i].message != null && showAlert != true)
                {
                    showAlert = true;
                    EditorUtility.DisplayDialog("提示", "代码编译出错或代码规范错误!", "知道了");
                    break;
                }
            }
        };
    }
}
