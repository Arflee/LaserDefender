using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score = 0;

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
        Destroy(this.gameObject);
    }

}
