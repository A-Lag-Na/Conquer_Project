using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    Text text;
    Slider progress;
    bool restart;
    float delay;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        progress = transform.GetChild(0).GetChild(1).GetComponent<Slider>();
        restart = true;
        delay = 0.55f;
        StartCoroutine(LoadNewScene(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
            StartCoroutine(LoadingText());
    }

    IEnumerator LoadingText()
    {
        restart = false;
        text.text = "Loading";
        yield return new WaitForSeconds(delay);
        text.text = "Loading.";
        yield return new WaitForSeconds(delay);
        text.text = "Loading..";
        yield return new WaitForSeconds(delay);
        text.text = "Loading...";
        yield return new WaitForSeconds(delay);
        restart = true;
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
            progress.value = async.progress * 10f;
        }
    }
}
