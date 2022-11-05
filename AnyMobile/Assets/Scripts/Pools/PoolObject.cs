using System;
using System.Collections;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public event Action OnBackToPool;
    public Action OnTackedFromPool;

    public bool InPool => !gameObject.activeInHierarchy;

    protected virtual void OnEnable() => OnTackedFromPool?.Invoke();

    public void BackToPool(float time) => StartCoroutine(Timer(time));
    public void StopTimer() => StopCoroutine(Timer());

    private IEnumerator Timer(float time = 0)
    {
        yield return new WaitForSeconds(time);
        BackToPool();
    }

    public void BackToPool()
    {
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
        OnBackToPool?.Invoke();
    }
}
