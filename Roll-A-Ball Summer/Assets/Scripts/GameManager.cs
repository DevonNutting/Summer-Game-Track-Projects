using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Create a variable to contain an instance of this script, so we can make sure there is only ever one copy of it in our game
    // This is what you call a Singleton pattern
    public static GameManager Instance { get; private set;} // A variable called 'Instance' that holds an instance of this script

   public int score; // A variable to track the player's current score
   public TextMeshProUGUI scoreText; // A variable to track the text object for the score
   public GameObject victoryTextObject; // A variable to track the text for the win condition; This text will appear once the player collects all pickups
   public GameObject pickupParent; // A variable that will hold the 'pickup parent' game object; we need this in order to count how many pickups are in the level at the start of the game
   public Transform enemyParent; // A variable that holds the 'EnemyParent' gameObject. We use this to access & destory all enemies when the player wins
   [SerializeField] private int _pickupTotal = 0; // A variable to track how many pickups are in the level; we will count how many there are in the Start() method using the pickup parent

    public void Awake() 
    {
        if (Instance == null) // If the 'Instance' variable is currently empty
        {
            Instance = this; // Assign this instance of this script to the 'Instance' variable
        } else {
            // If another copy of this script already exists in this scene, delete all the extra copies
            Debug.LogWarning("Cannot have more than once instance of 'GameManager' in the scene!");
            Destroy(this.gameObject);
        }
    }

    void Start()
   {
        score = 0; // Assign score variable to 0 when the game starts
        UpdateUI(); // Update the score text to reflect the current score when the game starts
        victoryTextObject.SetActive(false); // Disable the victory text when the game starts; the player has yet to actually win yet
        _pickupTotal = pickupParent.transform.childCount;
   }


   public void UpdateScore(int amount) // A function that updates the score variable 
   {
        score += amount;
        UpdateUI();

        if(score >= _pickupTotal)
        {
          WinGame();
        }
   }


    public void UpdateUI() // A function that updates the score text with the current score variable
    {
            scoreText.text = score.ToString() + " / " + _pickupTotal;
    }

    public void WinGame() // A function that enables the victory text when the player has beat the level
    {
        victoryTextObject.SetActive(true); // Enable the victory text so it appears on screen
        AudioManager.Instance.PlaySound("Victory SFX"); // Tell the Audio Manager to play the Victory SFX\

        foreach (Transform enemy in enemyParent) Destroy(enemy.gameObject); // For each enemy nested inside the 'enemyParent', destroy each enemy
    }

    public void GameOver() // A function that is called whenever the player loses the game
    {
        Invoke("LoadCurrentLevel", 2f);
    }

    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
    }
}





