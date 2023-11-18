using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class FusionShieldCollisionController : MonoBehaviour
{
    #region Variables



    #endregion

    #region Action Variables

    public static Action OnShieldsMerge;
    public static Action OnShieldsBrokeUp;

    #endregion

    #region Unity Methods



    #endregion

    #region Custom Methods



    #endregion

    #region Collision Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NormalShield"))
        {
            OnShieldsMerge?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NormalShield"))
        {
            OnShieldsBrokeUp?.Invoke();
        }
    }

    #endregion
}
