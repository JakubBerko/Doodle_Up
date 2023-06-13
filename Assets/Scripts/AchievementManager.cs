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

    [SerializeField]
    [HideInInspector]
    private List<AchievementItemController> achievementItems; //seznam achievementu

    private void Start()
    {
        LoadAchievementsTable();
    }
    public void ShowNotification()
    {
        Achievement achievement = database.achievements[(int)achievementToShow];//vytahne z databaze achievement
        achievementNotificationController.ShowNotification(achievement);//zavola kod z jineho skriptu, ktery spusti notifikaci
    }

    [ContextMenu("LoadAchievementsTable()")]
    private void LoadAchievementsTable()
    {
        foreach (AchievementItemController controller in achievementItems) //smaze existujici achievementy
        {
            DestroyImmediate(controller.gameObject);
        }
        achievementItems.Clear();
        foreach (Achievement achievement in database.achievements) //naète znovu achievementy a jejich stav, zda-li jsou odemcena atd.
        {
            GameObject obj = Instantiate(achievementItemPrefab, content);
            AchievementItemController controller = obj.GetComponent<AchievementItemController>();
            bool unlocked = PlayerPrefs.GetInt(achievement.id, 0) == 1;
            controller.unlocked = unlocked;
            controller.achievement = achievement;
            controller.RefreshView();
            achievementItems.Add(controller);
        }
        
    }
    public void UnlockAchievement() //self explainatory
    {
        UnlockAchievement(achievementToShow);
    }
    public void UnlockAchievement(Achievements achievement) //odemkne achievement a zobrazi notif. + naète znovu achievemnts list
    {
        achievementToShow = achievement;
        AchievementItemController item = achievementItems[(int)achievement];
        if (item.unlocked)
            return;

        ShowNotification();
        PlayerPrefs.SetInt(item.achievement.id, 1);
        item.unlocked = true;
        item.RefreshView();
    }
    public void LockAllAchievements() //zamkne vsechny achievementy
    {
        foreach (Achievement achievement in database.achievements)
        {
            PlayerPrefs.DeleteKey(achievement.id);
        }
        foreach (AchievementItemController controller in achievementItems)
        {
            controller.unlocked = false;
            controller.RefreshView();
        }
    }
}
