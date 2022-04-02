using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using Debug = UnityEngine.Debug;

[CustomEditor(typeof(TestScript))]
public class TestScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("RunTest"))
            RunTests((TestScript)this.target);
    }

    private void RunTests(TestScript testScript)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Report");

        var sw = new Stopwatch();
        foreach (var texture in testScript.Textures)
        {
            sw.Restart();

            var assetPath = AssetDatabase.GetAssetPath(texture);
            var fileName = Path.GetFileNameWithoutExtension(assetPath);

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceSynchronousImport);

            var time = sw.Elapsed;

            sb.AppendLine($"{fileName}: {time}");
        }

        Debug.Log(sb.ToString());
    }
}