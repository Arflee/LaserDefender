using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;

    private void Start()
    {
        optionsMenu.SetActive(false);
    }

    public void ToggleMainMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ToggleOptionsMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void LoadMainMenuScene()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync(1);
        FindObjectOfType<GameSession>().ResetGame();
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
