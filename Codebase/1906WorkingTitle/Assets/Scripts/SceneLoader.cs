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

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
            progress.value = async.progress;
        }

    }

}
