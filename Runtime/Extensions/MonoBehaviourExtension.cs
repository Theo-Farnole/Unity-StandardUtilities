using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtension
{
    /// <summary>
    /// Execute task after realtime.
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="time"></param>
    /// <param name="task"></param>
    public static Coroutine ExecuteAfterTime(this MonoBehaviour mono, float time, Action task)
    {
        return mono.StartCoroutine(ExecuteAfterTimeCoroutine(time, task));
    }

    /// <summary>
    /// Called from ExecuteAfterTime.
    /// </summary>
    static IEnumerator ExecuteAfterTimeCoroutine(float time, Action task)
    {
        yield return new WaitForSecondsRealtime(time);

        task();
    }

    /// <summary>
    /// Execute task after one frame.
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="time"></param>
    /// <param name="task"></param>
    public static Coroutine ExecuteAfterFrame(this MonoBehaviour mono, Action task)
    {
        return mono.StartCoroutine(ExecuteAfterFrameCoroutine(task));
    }

    static IEnumerator ExecuteAfterFrameCoroutine(Action task)
    {
        yield return new WaitForEndOfFrame();

        task();
    }
}
