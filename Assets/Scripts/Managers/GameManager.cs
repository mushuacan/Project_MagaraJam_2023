using UnityEngine;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    #region Variables

    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject MainGameObject;
    [SerializeField] private int playerHealth;

    public bool IsGameOn {get; set;}
    public int PlayerHealth {get; private set;}
    public bool IsFusionActive { get; set; }

    #endregion

    #region Action Variables

    public static Action OnGameStarted;
    public static Action OnGameOver;

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
    }

    private void OnEnable()
    {
        ColorMachineController.OnMachineGetsHit += MachineGetsHit;
        OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        ColorMachineController.OnMachineGetsHit -= MachineGetsHit;
        OnGameOver -= GameOver;
    }

    private void Start()
    {
        // Just for now. There will be first tutorials and touch to start screen. These line of codes will execute when player closes them
        OnGameStarted?.Invoke();
        IsGameOn = true;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    #endregion

    #region Custom Methods

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

            print("<color=red>" + "Game Over" + "</color>");
        }
        else
        {
            // Play Machine Get Hit Particle or Anim
            print("<color=yellow>" + "Machine Gets Hit" + "</color>");
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
