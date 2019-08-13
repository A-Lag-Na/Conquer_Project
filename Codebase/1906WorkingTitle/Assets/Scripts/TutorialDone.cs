using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TutorialDone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject clone = Instantiate(Resources.Load<GameObject>("Prefabs/UI/SceneLoader"));
            StartCoroutine(clone.GetComponent<SceneLoader>().LoadNewScene(2));
        }
    }
}
