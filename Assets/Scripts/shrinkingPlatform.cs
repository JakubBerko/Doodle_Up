using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrinkingPlatform : MonoBehaviour
{
    public float shrinkDistance = 10f; // Vzd�lenost, ve kter� se platforma za�ne zmen�ovat
    public float shrinkAmount = 0.5f; // Hodnota, o kterou se platforma zmen��

    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Doodler").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position); // Vzd�lenost hr��e od platformy

        if (distance < shrinkDistance)
        {
            float shrinkElement = 1f - (distance / shrinkDistance) * shrinkAmount; // V�po�et faktoru zmen�en�

            // Zmen�en� platformy
            transform.localScale = Vector3.one * shrinkElement;
        }
    }
}
