using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroLetterLoader : MonoBehaviour
{

    private Slider progress;

    void Start()
    {
        progress = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
    }


    public IEnumerator LoadNewScene(int scene)
    {
        // This line waits for 1.5 seconds before executing the next line in the coroutine.
        yield return new WaitForSeconds(1.5f);

        // Async load passed in scene
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // update progress while loading
        while (!async.isDone)
        {
            yield return null;
            progress.value = async.progress * 100f;
        }
    }
}
