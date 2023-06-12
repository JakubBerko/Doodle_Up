using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malee; //reorderable list

[CreateAssetMenu()] //zobrazitene v assets menu pr vytvareni assetu
public class AchievementDatabase : ScriptableObject //chci mit achievement jako scriptable object
{
    [Reorderable(sortable = false, paginate = false)] //uprava vlastnosti reorderable listu
    public AchievementsArray achievements;
    [System.Serializable]

    public class AchievementsArray : ReorderableArray<Achievement> { }
}
