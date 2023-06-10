using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayTimeScaleZero : MonoBehaviour
{
    public void Delay()
    {
        StartCoroutine(DelayTimeScaleZeroF());
    }
    
    IEnumerator DelayTimeScaleZeroF()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 0f;
    }
}
