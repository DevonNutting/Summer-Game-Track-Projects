using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuContent;
    [SerializeField] GameObject howToPlayContent;

    private void Awake()
    {
        mainMenuContent.SetActive(true);
        howToPlayContent.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleTutorialMenu()
    {
        mainMenuContent.SetActive(!mainMenuContent.activeSelf);
        howToPlayContent.SetActive(!howToPlayContent.activeSelf);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
