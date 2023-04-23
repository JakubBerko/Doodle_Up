using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public AchievementDatabase database;

    public AchievementNotificationController achievementNotificationController;

    public GameObject achievementItemPrefab;
    public Transform content;

    public Achievements achievementToShow;

    public void ShowNotification()
    {
        Achievement achievement = database.achievements[(int)achievementToShow];
        achievementNotificationController.ShowNotification(achievement);
    }

    private void LoadAchievementsTable()
    {
        foreach (Achievement achievement in database.achievements)
        {
            GameObject obj = Instantiate(achievementItemPrefab, content);
        }
    }
}
