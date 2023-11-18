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

    private Tween leftShieldRotateTween;
    private Tween rightShieldRotateTween;

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
        StopLeftShieldRotation();

        MoveRightShield();
        StopRightShieldRotation();
    }

    #endregion

    #region Custom Methods

    private void MoveLeftShield()
    {
        // This is for shield to move towards left
        if (Input.GetKey(KeyCode.A))
        {
            if (leftShieldRotateTween == null)
            {
                Vector3 rotateVector = new Vector3(0, 0, -360);
                leftShieldRotateTween = leftShieldController.transform.transform.
                    DOLocalRotate(rotateVector, rotateDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(rotationEase).SetLoops(-1);
            }
        }
        // This is for shield to move towards right
        else if (Input.GetKey(KeyCode.D))
        {
            if (leftShieldRotateTween == null)
            {
                Vector3 rotateVector = new Vector3(0, 0, 360);
                leftShieldRotateTween = leftShieldController.transform.transform.
                    DOLocalRotate(rotateVector, rotateDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(rotationEase).SetLoops(-1);
            }
        }
    }

    private void StopLeftShieldRotation()
    {
        // It means when player stops to press A, rotation will be stop
        if (Input.GetKeyUp(KeyCode.A))
        {
            leftShieldRotateTween.Kill();
            leftShieldRotateTween = null;
        }
        // It means when player stops to press D, rotation will be stop
        else if (Input.GetKeyUp(KeyCode.D))
        {
            leftShieldRotateTween.Kill();
            leftShieldRotateTween = null;
        }
    }

    private void MoveRightShield()
    {
        // This is for shield to move towards left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (rightShieldRotateTween == null)
            {
                Vector3 rotateVector = new Vector3(0, 0, -360);
                rightShieldRotateTween = rightShieldController.transform.transform.
                    DOLocalRotate(rotateVector, rotateDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(rotationEase).SetLoops(-1);
            }
        }
        // This is for shield to move towards right
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (rightShieldRotateTween == null)
            {
                Vector3 rotateVector = new Vector3(0, 0, 360);
                rightShieldRotateTween = rightShieldController.transform.transform.
                    DOLocalRotate(rotateVector, rotateDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(rotationEase).SetLoops(-1);
            }
        }
    }

    private void StopRightShieldRotation()
    {
        // It means when player stops to press left arrow, rotation will be stop
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rightShieldRotateTween.Kill();
            rightShieldRotateTween = null;
        }
        // It means when player stops to press right arrow, rotation will be stop
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightShieldRotateTween.Kill();
            rightShieldRotateTween = null;
        }
    }

    #endregion

}
