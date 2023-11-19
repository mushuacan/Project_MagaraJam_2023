using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorMachineController : MonoBehaviour
{
    #region Variables

    [SerializeField] private SpriteRenderer ColorMachineSprite;
    [SerializeField] private Sprite ColorMachineStartSprite;
    [SerializeField] private Sprite[] ColorMachineHealthSprites;

    #endregion

    #region Action Variables

    public static Action OnMachineGetsHit;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        OnMachineGetsHit += DecreaseHealth;
        GameManager.OnGameStarted += GameStart;
    }

    private void OnDisable()
    {
        OnMachineGetsHit -= DecreaseHealth;
        GameManager.OnGameStarted -= GameStart;
    }

    #endregion

    #region Custom Methods

    private void GameStart()
    {
        ColorMachineSprite.sprite = ColorMachineStartSprite;
    }

    private void DecreaseHealth()
    {
        // 0th Sprite will be broken machine sprite. Other ones are the health of machines
        // This line below will Activated when the other parts of machine states Images has come.

        DOVirtual.DelayedCall(0.05f, delegate 
        {
            ColorMachineSprite.sprite = ColorMachineHealthSprites[GameManager.Instance.PlayerHealth];
        });
        // Play Get Hit Particle.

    }

    #endregion

    #region Collision Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            OnMachineGetsHit?.Invoke();

            collision.gameObject.SetActive(false);
        }
    }

    #endregion
}
