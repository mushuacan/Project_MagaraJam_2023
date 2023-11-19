using UnityEngine;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    #region Variables

    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject MainGameObject;
    [SerializeField] private int playerHealth;
    
    public int MainColorScorePoint;
    public int FusionColorScorePoint;

    public bool IsGameOn {get; set;}
    public int PlayerHealth {get; private set;}
    public bool IsFusionActive { get; set; }
    public int Score { get; set; }
    public int HighScore { get; set; }

    #endregion

    #region Action Variables

    public static Action OnGameStarted;
    public static Action OnGameOver;
    public static Action<int> OnScoreGained;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        IsGameOn = false;
        PlayerHealth = playerHealth;
        Score = 0;

        if (!PlayerPrefs.HasKey("HighScore"))
        {
            HighScore = 0;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }

    private void OnEnable()
    {
        OnGameStarted += StartGame;
        ColorMachineController.OnMachineGetsHit += MachineGetsHit;
        OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        OnGameStarted -= StartGame;
        ColorMachineController.OnMachineGetsHit -= MachineGetsHit;
        OnGameOver -= GameOver;
    }

    #endregion

    #region Custom Methods

    private void StartGame()
    {
        PlayerHealth = playerHealth;
        Score = 0;
        IsGameOn = true;
    }

    private void GameOver()
    {
        IsGameOn = false;
    }

    private void MachineGetsHit()
    {
        PlayerHealth--;

        if (PlayerHealth <= 0)
        {
            // Play Explosion Particle or Anim
            OnGameOver?.Invoke();
        }
        else
        {
            // Play Machine Get Hit Particle or Anim
        }
    }

    #endregion

}

#region Enums

public enum AllColors 
{
    Red = 0,
    Green = 1,
    Blue = 2,
    Magenta = 3, 
    Yellow = 4,
    Cyan = 5,
}

#endregion
