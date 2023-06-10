using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
/*
[CustomEditor(typeof(AchievementDatabase))]
public class AchievementDatabaseEditor : Editor
{
    private AchievementDatabase database;

    private void OnEnable()
    {
        database = target as AchievementDatabase;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate Enum"))
        {
            GenerateEnum();
        }
    }

    private void GenerateEnum()
    {
        string filePath = Path.Combine(Application.dataPath, "Achievements.cs");
        string code = "public enum Achievements{";
        foreach (Achievement achievement in database.achievements)
        {
            code += achievement.id + ",";
        }
        code += "}";
        File.WriteAllText(filePath, code);
        AssetDatabase.ImportAsset("Assets/Achievements.cs");
    }
}
*/