using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShieldController : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject leftShieldController;
    [SerializeField] private GameObject leftShield;
    [SerializeField] private GameObject rightShieldController;
    [SerializeField] private GameObject rightShield;
    [SerializeField] private Ease rotationEase;
    [SerializeField] private float rotateDuration;

    private Tween leftRotateTween;
    private Tween rightRotateTween;

    #endregion

    #region Action Variables



    #endregion

    #region Unity Methods

    private void Start()
    {

    }

    private void Update()
    {
        MoveLeftShield();
    }

    #endregion

    #region Custom Methods

    private void MoveLeftShield()
    {
        // This is for shield to move towards left
        if (Input.GetKey(KeyCode.A))
        {
            if (!leftRotateTween.active)
            {
                Vector3 rotateVector = new Vector3(0, 0, -90);
                leftRotateTween = leftShieldController.transform.transform.
                    DOLocalRotate(rotateVector, rotateDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(rotationEase);
            }
        }
        // This is for shield to move towards right
        else if (Input.GetKey(KeyCode.D))
        {
            if (!leftRotateTween.active)
            {
                Vector3 rotateVector = new Vector3(0, 0, 90);
                rightRotateTween = leftShieldController.transform.transform.
                    DOLocalRotate(rotateVector, rotateDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(rotationEase);
            }
        }
    }

    #endregion

}
