using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        //SceneManager.LoadScene(sceneName);
        StartCoroutine(AsyncLoad(sceneName));
    }

    IEnumerator AsyncLoad(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!op.isDone)
        {
            yield return null;
        }
    }
}
