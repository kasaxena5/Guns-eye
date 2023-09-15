using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    [Header("Required Components")]
    [SerializeField] PlayerStats playerStats;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Slider healthBar;


    [Header("Configs")]
    [SerializeField] float scoreTransitionSpeed = 100;

    float displayScore;

    public void Start()
    {
        healthBar.maxValue = 1;
        healthBar.value = 1;
        playerStats.health = 1f;
    }

    private void Update()
    {
        displayScore = Mathf.MoveTowards(displayScore, playerStats.score, scoreTransitionSpeed * Time.deltaTime);
        UpdateScoreDisplay();
        UpdateHealthDisplay();
    }
    public void IncreaseScore(int amount)
    {
        playerStats.score += amount;
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = string.Format("Score: {0:0000}", displayScore);
    }

    private void UpdateHealthDisplay()
    {
        healthBar.value = playerStats.health;
    }
}
