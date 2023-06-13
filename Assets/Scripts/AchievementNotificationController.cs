using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Animator))]
public class AchievementNotificationController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI achievementTitleLabel;
    private Animator m_animator;
    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }
    public void ShowNotification(Achievement achievement) //ukáže notif. animaci
    {
        achievementTitleLabel.text = achievement.nazev;
        m_animator.SetTrigger("Appear");
    }
}
