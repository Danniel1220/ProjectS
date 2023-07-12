using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreTracker : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;
    public float highscore = 0f;

    void Start()
    {
        highscoreText = GameObject.Find("Canvas").transform.Find("Highscore").GetComponent<TextMeshProUGUI>();        
    }

    void Update()
    {
        highscoreText.SetText("Scor: " + highscore);
    }
}
