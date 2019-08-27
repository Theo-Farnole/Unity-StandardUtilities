using UnityEngine;
using System.Collections;

// code from
// https://www.gamasutra.com/blogs/VivekTank/20180709/321571/Different_Ways_Of_Shaking_Camera_In_Unity.php
public class CameraShake : Singleton<CameraShake>
{
    [SerializeField] private CameraShakeData _data;

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine(_data.Duration, _data.Magnitude));
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 orignalPosition = transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = orignalPosition + new Vector3(x, y);
            elapsed += Time.unscaledDeltaTime;

            yield return new WaitForEndOfFrame();
        }

        transform.position = orignalPosition;
    }
}