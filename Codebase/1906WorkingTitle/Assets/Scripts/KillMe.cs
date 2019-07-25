using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMe : MonoBehaviour
{
    public float _time;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _time);
    }
}
