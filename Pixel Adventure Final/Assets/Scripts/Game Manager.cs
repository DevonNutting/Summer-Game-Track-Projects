using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{


   [SerializeField][Tooltip("Reference to the point text game object, assign in editor")]
   private TextMeshProUGUI _pointText;


   [SerializeField][Tooltip("Value for the player's current points")]
   private int _playerPoints = 0;


   public static GameManager Instance {get; private set;}
   private void Awake()
   {
       if (Instance != null)
       {
           Debug.LogError("There's more than one GameManager! " + transform + " - " + Instance);
           Destroy(gameObject);
           return;
       }
       Instance = this;


       _playerPoints = 0;
   }


   private void Start()
   {
       UdpateUI();
   }


   private void UdpateUI()
   {
       // Updates the UI based on the player's current points
       _pointText.text = _playerPoints.ToString();
   }


   public void UpdateScore(int value)
   {
       // Updates the Player's points by the given value
       _playerPoints += value;
       UdpateUI();
   }


   private void RestartCurrentLevel()
   {
       // Restarts the curernt level, can only be called in this script
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       AudioManager.Instance.PlaySound("MainTheme");
   }


   public void GameOver()
   {
       // This method is called from the Player on death so the Manager can reset the stage
       Invoke("RestartCurrentLevel", 3f);
   }


}



