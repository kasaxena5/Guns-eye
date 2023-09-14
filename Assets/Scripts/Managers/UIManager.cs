using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [SerializeField] float transitionSpeed = 100;

    int score;
    float displayScore;
    public TMP_Text scoreText;

    private void Update()
    {
        displayScore = Mathf.MoveTowards(displayScore, score, transitionSpeed * Time.deltaTime);
        UpdateScoreDisplay();
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public void UpdateScoreDisplay()
    {
        scoreText.text = string.Format("Score: {0:0000}", displayScore);
    }
}
