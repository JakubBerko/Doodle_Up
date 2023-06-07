using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrinkingPlatform : MonoBehaviour
{
    public float shrinkDistance = 10f; // Vzdálenost, ve které se platforma zaène zmenšovat
    public float shrinkAmount = 1f; // Hodnota, o kterou se platforma zmenší

    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Doodler").transform; // nalezení hráèe
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position); // Vzdálenost hráèe od platformy

        if (distance < shrinkDistance)
        {
            float shrinkElement = 0.85f + (distance / shrinkDistance) * shrinkAmount; // Výpoèet faktoru zmenšení

            transform.localScale = Vector3.one * shrinkElement; // Zmenšení platformy
        }
    }
}
