using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollisionController : MonoBehaviour
{
    #region Variables

    [SerializeField] private ShieldType shieldType;
    private AllColors colorType;

    #endregion

    #region Action Variables

    public static Action OnShieldUsed;

    #endregion

    #region Unity Methods



    #endregion

    #region Custom Methods

    private void GetShieldTypeColor(ShieldType shieldType)
    {
        switch (shieldType)
        {
            case ShieldType.LeftShield:
                colorType = ShieldController.Instance.LeftShieldColor;
                break;

            case ShieldType.RightShield:
                colorType = ShieldController.Instance.RightShieldColor;
                break;

            case ShieldType.FusionShield:
                colorType = ShieldController.Instance.FusionShieldColor;
                break;
        }
    }

    #endregion

    #region Collision Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            GetShieldTypeColor(shieldType);
            LazerMove lazerObject = collision.gameObject.GetComponent<LazerMove>();

            if (lazerObject.lazerColor == colorType)
            {
                print("<color=green>" + "Shield Protected the Machine!" + "</color>");
                OnShieldUsed?.Invoke();
                collision.gameObject.SetActive(false);
            }
            
        }
    }

    #endregion
}

public enum ShieldType
{
    LeftShield = 0,
    RightShield = 1,
    FusionShield = 2,
}
