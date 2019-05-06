using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGameOverScene()
    {
        // load the last scene in build settings
        SceneManager.LoadSceneAsync(SceneManager.sceneCountInBuildSettings - 1); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
