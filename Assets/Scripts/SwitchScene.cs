using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    public void LoadScene(string sceneName)

    {
        StartCoroutine(DelaySceneLoad(sceneName));
        
    }

    IEnumerator DelaySceneLoad(string sceneName)
    {
        
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneName);
    }
}


