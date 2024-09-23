/// < Mark Gaskins 2024 > ///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public int score, enemiesLeft = 100, enemiesGotIn;
    [SerializeField] TextMeshProUGUI scoreText, enemiesLeftText, enemiesGotInText, scoreTextWin;
    public Transform WaypointsParent;
    public AudioSource CoreFXPlayer;
    public GameObject WinCanvas, MainCanvas;
    void Start()
    {
        CoreFXPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        scoreText.text = "SCORE: " + score.ToString("D5");

        enemiesLeftText.text = "ENEMIES LEFT: "+ enemiesLeft.ToString("D3");

        enemiesGotInText.text = "ENEMIES GOT IN: "+enemiesGotIn.ToString();

        if (enemiesLeft == 0) Invoke(nameof(WinGame), 3);
    }


    public void WinGame()
    {
        
        scoreTextWin.text = scoreText.text.Replace("SCORE: ", string.Empty);

        WinCanvas.SetActive(true);
        MainCanvas.SetActive(false);
        
    }
}

