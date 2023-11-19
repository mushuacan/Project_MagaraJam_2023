using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject gameOverObject;
    [SerializeField] private Image gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        GameManager.OnGameOver += ActivateGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= ActivateGameOver;
    }

    #endregion

    #region Custom Methods

    private void ActivateGameOver()
    {
        ScoreManager.Instance.CalculateHighScore(GameManager.Instance.Score);

        gameOverObject.gameObject.SetActive(true);

        gameOverScoreText.text = "Score: " + GameManager.Instance.Score.ToString();
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");

        float panelActivationTime = 0.3f;
        float gameOverObjectsDuration = 0.5f;

        DOVirtual.DelayedCall(0.2f, delegate 
        {
            gameOverPanel.transform.DOScale(Vector3.one, panelActivationTime).OnComplete(delegate 
            {
                DOVirtual.DelayedCall(0, delegate 
                {
                    gameOverText.transform.DOScale(Vector3.one, gameOverObjectsDuration);
                });
                DOVirtual.DelayedCall(0.25f, delegate
                {
                    gameOverScoreText.transform.DOScale(Vector3.one, gameOverObjectsDuration);
                    highScoreText.transform.DOScale(Vector3.one, gameOverObjectsDuration);
                });
                DOVirtual.DelayedCall(0.5f, delegate
                {
                    retryButton.transform.DOScale(Vector3.one, gameOverObjectsDuration).OnComplete(delegate 
                    {
                        retryButton.onClick.AddListener(RetryButton);
                    });

                    quitButton.transform.DOScale(Vector3.one, gameOverObjectsDuration).OnComplete(delegate
                    {
                        quitButton.onClick.AddListener(QuitButton);
                    }); 
                });

            });
        });
    }

    private void DeactivateGameOver()
    {
        gameOverPanel.transform.DOScale(Vector3.zero, 0.75f).OnComplete(delegate 
        {
            gameOverObject.gameObject.SetActive(false);
            GameManager.OnGameStarted?.Invoke();
        });
    }

    private void RetryButton()
    {
        retryButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();

        DeactivateGameOver();
    }

    private void QuitButton()
    {
        retryButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
        Application.Quit();
    }

    #endregion
}
