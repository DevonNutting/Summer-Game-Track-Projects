using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text scoreText;
    public Transform playerTransform;

    private void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        scoreText.SetText(playerTransform.position.z.ToString("0"));
    }   
}
