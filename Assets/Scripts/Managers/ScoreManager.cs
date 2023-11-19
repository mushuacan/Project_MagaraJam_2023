using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    #region Variables

    public static ScoreManager Instance { get; private set; }

    [SerializeField] private Image scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float scoreIncreaseSpeed;

    private int currentScore;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        GameManager.OnGameStarted += ScorePanelActivate;
        GameManager.OnGameOver += ScorePanelDeactivate;
        GameManager.OnScoreGained += IncreaseScore;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= ScorePanelActivate;
        GameManager.OnGameOver -= ScorePanelDeactivate;
        GameManager.OnScoreGained -= IncreaseScore;
    }

    #endregion

    #region Custom Methods

    private void ScorePanelActivate()
    {
        GameManager.Instance.Score = 0;
        scoreText.text = GameManager.Instance.Score.ToString();

        float openDuration = 0.25f;
        scorePanel.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), openDuration).OnComplete(delegate 
        {
            scorePanel.transform.DOScale(Vector3.one, openDuration / 2);
        });
    }

    private void ScorePanelDeactivate()
    {
        float closeDuration = .25f;

        scorePanel.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), closeDuration / 2).OnComplete(delegate 
        {
            scorePanel.transform.DOScale(Vector3.zero, closeDuration);
        });
        
    }

    private void IncreaseScore(int scoreToAdd)
    {
        GameManager.Instance.Score += scoreToAdd;
        StartCoroutine(IncreaseScoreAnimation(GameManager.Instance.Score));
    }

    private IEnumerator IncreaseScoreAnimation(int targetScore)
    {
        while (currentScore < targetScore)
        {
            currentScore++;
            scoreText.text = currentScore.ToString();
            yield return new WaitForSeconds(scoreIncreaseSpeed);
        }
    }

    #endregion

}
