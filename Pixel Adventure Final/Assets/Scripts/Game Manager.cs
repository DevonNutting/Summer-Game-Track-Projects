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
        //  If the 'Instance' var is already assigned
       if (Instance != null)
       {
           Debug.LogError("There's more than one GameManager! " + transform + " - " + Instance);
           Destroy(gameObject); // Destroy this extra copy of GameManager (There can only be one)
           return;
       }
       Instance = this; // Assign this copy of GameManager to the 'Instance' var

       // Reset the player's point value at the start of the level
       _playerPoints = 0;
   }


   private void Start()
   {
        UdpateUI();
        AudioManager.Instance.PlaySound("MainTheme"); // Tell the Audio Manager to play the theme music
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

   private void LoadStartMenu()
   {
        // Load the Start Menu Scene
        SceneManager.LoadScene(0);
   }


   public void GameOver()
   {
       // This method is called from the Player on death so the Manager can reset the stage
       Invoke("LoadStartMenu", 3f);
   }

   public void LevelComplete()
   {
        // this method is called when the player touches the flag goal at the end of the level
        // Reset the level to play again; Alternatively, this is where you can load the next level of your game
        Invoke("LoadStartMenu", 3f);
   }


}



