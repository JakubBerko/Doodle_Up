using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AchievementItemController : MonoBehaviour
{
    [SerializeField] Image unlockedIcon;
    [SerializeField] Image lockedIcon;

    [SerializeField] TextMeshProUGUI nazev;
    [SerializeField] TextMeshProUGUI popis;

    public bool unlocked;
    public Achievement achievement;

    public void RefreshView()
    {
        nazev.text = achievement.nazev;
        popis.text = achievement.popis;

        unlockedIcon.enabled = unlocked;
        lockedIcon.enabled = !unlocked;
    }

    private void OnValidate()
    {
        RefreshView();
    }
}
