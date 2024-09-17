/// < Mark Gaskins 2024 > ///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score, enemiesLeft = 100, enemiesGotIn;
    [SerializeField] TextMeshProUGUI scoreText, enemiesLeftText, enemiesGotInText;
    public Transform WaypointsParent;

    void Start()
    {
        
    }

    void Update()
    {
        scoreText.text = "SCORE: " + score.ToString("D5");

        enemiesLeftText.text = "ENEMIES LEFT: "+ enemiesLeft.ToString("D3");

        enemiesGotInText.text = "ENEMIES GOT IN: "+enemiesGotIn.ToString();
    }

}

