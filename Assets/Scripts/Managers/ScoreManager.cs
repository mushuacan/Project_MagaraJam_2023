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
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= ScorePanelActivate;
        GameManager.OnGameOver -= ScorePanelDeactivate;
    }

    #endregion

    #region Custom Methods

    private void ScorePanelActivate()
    {
        float openDuration = 0.25f;
        scorePanel.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), openDuration).OnComplete(delegate 
        {
            scorePanel.transform.DOScale(Vector3.one, openDuration / 2);
        });
    }

    private void ScorePanelDeactivate()
    {
        float closeDuration = .5f;
        scorePanel.transform.DOScale(Vector3.zero, closeDuration);
    }

    #endregion

}
