using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    int score = 0;
    public TextMeshProUGUI scoreText;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadScore();
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("scoreKey", score);
    }

    public void LoadScore()
    {
        score = PlayerPrefs.GetInt("scoreKey");
        scoreText.text = "Score: " + score;
    }

    void OnApplicationQuit()
    {
        SaveScore();
    }
}
