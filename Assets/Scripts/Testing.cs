using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public RuntimeAnimatorController newAnimatorController;
    void Start()
    {
        OverrideAnim();
    }
    void OverrideAnim()
    {
        Animator animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = newAnimatorController;
    }
}
