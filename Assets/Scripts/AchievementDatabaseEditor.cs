using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
/* //Editor komponent (mam zakomentovanou jelikoz je jen jedna a nebuildne se s ní hra)
[CustomEditor(typeof(AchievementDatabase))]
public class AchievementDatabaseEditor : Editor
{
    private AchievementDatabase database;

    private void OnEnable() 
    {
        database = target as AchievementDatabase; //priradi instanci AchievementDatabase do "database"
    }

    public override void OnInspectorGUI() //pridava button do inspektoru a zavola funkci
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate Enum"))
        {
            GenerateEnum();
        }
    }

    private void GenerateEnum() //tvori enumy a ulozi ho do databaze achievements
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