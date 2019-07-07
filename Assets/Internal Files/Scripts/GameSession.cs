using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    private int score = 0;
    private bool cursorIsShowing = false;

    [SerializeField] private Slider slider1 = default;
    [SerializeField] private Slider slider2 = default;
    [SerializeField] private Slider slider3 = default;

    private void Awake()
    {
        if (FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

        
    }

    private void Start()
    {
        slider1.value = slider2.value = slider3.value = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (cursorIsShowing)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                cursorIsShowing = false;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                cursorIsShowing = true;
            }
            
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int score)
    {
        this.score += score;
    }

    public void ResetGame()
    {
        score = 0;
    }

}
