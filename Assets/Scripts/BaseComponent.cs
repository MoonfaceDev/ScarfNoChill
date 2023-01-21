using System;
using System.Collections;
using UnityEngine;

public class BaseComponent : MonoBehaviour
{
    public Coroutine StartTimeout(Action action, float duration)
    {
        return StartCoroutine(TimeoutCoroutine(action, duration));
    }

    public void CancelTimeout(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private static IEnumerator TimeoutCoroutine(Action action, float duration)
    {
        yield return new WaitForSeconds(duration);
        action();
    }
}