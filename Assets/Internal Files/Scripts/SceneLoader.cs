using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadGameOverScene()
    {
        // load the last scene in build settings
        StartCoroutine(WaitAndLoad(2));   
    }

    private IEnumerator WaitAndLoad(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadSceneAsync(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
