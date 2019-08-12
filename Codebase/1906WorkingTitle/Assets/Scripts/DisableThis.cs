using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableThis : MonoBehaviour
{
    public float _time;

    IEnumerator Timer(float _time)
    {
        yield return new WaitForSeconds(_time);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Timer(_time));
    }
}

