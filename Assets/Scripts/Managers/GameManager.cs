using UnityEngine;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    #region Variables

    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject MainGameObject;

    public bool IsGameOn {get; set;}

    #endregion

    #region Action Variables

    public static Action OnGameStarted;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        IsGameOn = false;
    }

    private void Start()
    {
        OnGameStarted?.Invoke();
    }

    #endregion

    #region Custom Methods



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
