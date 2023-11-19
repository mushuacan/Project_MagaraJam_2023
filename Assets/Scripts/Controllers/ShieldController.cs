using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShieldController : MonoBehaviour
{
    #region Variables

    public static ShieldController Instance { get; private set; }

    [SerializeField] private GameObject leftShieldController;
    [SerializeField] private SpriteRenderer leftShield;
    [SerializeField] private GameObject rightShieldController;
    [SerializeField] private SpriteRenderer rightShield;
    [SerializeField] private GameObject fusionShieldController;
    [SerializeField] private SpriteRenderer fusionShield;
    [SerializeField] private GameObject colorMachineGo;
    [SerializeField] private Ease rotationEase;
    [SerializeField] private float rotateDuration;

    private Tween leftShieldRotateTween;
    private Tween rightShieldRotateTween;
    private Tween fusionShieldRotateTween;
    private Tween fusionShieldTween;
    private Tween colorMachineTween;

    public AllColors LeftShieldColor { get; private set; }
    public AllColors RightShieldColor { get; private set; }
    public AllColors FusionShieldColor { get; private set; }

    #endregion

    #region Action Variables



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
        GameManager.OnGameStarted += RotateColorMachine;
        GameManager.OnGameOver += GameOver;
        FusionShieldCollisionController.OnShieldsMerge += ActivateFusionShield;
        FusionShieldCollisionController.OnShieldsBrokeUp += DeactivateFusionShield;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= RotateColorMachine;
        GameManager.OnGameOver -= GameOver;
        FusionShieldCollisionController.OnShieldsMerge -= ActivateFusionShield;
        FusionShieldCollisionController.OnShieldsBrokeUp -= DeactivateFusionShield;
    }

    private void Start()
    {
        SetRightShieldsColorAtStart(ColorManager.Instance.ReturnColor(AllColors.Red), AllColors.Red);
        SetLeftShieldsColorAtStart(ColorManager.Instance.ReturnColor(AllColors.Blue), AllColors.Blue);
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOn)
        {
            MoveLeftShield();
            StopLeftShieldRotation();

            MoveRightShield();
            StopRightShieldRotation();

            ChangeShieldColor();

            RotateFusionShield();
        }
    }

    #endregion

    #region Custom Methods

    #region Rotation Methods
    private void MoveLeftShield()
    {
        // This is for shield to move towards left
        if (Input.GetKey(KeyCode.A))
        {
            if (leftShieldRotateTween == null)
            {
                Vector3 rotateVector = new Vector3(0, 0, 360);
                leftShieldRotateTween = leftShieldController.transform.
                    DOLocalRotate(rotateVector, rotateDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(rotationEase).SetLoops(-1);
            }
        }
        // This is for shield to move towards right
        else if (Input.GetKey(KeyCode.D))
        {
            if (leftShieldRotateTween == null)
            {
                Vector3 rotateVector = new Vector3(0, 0, -360);
                leftShieldRotateTween = leftShieldController.transform.
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
                Vector3 rotateVector = new Vector3(0, 0, 360);
                rightShieldRotateTween = rightShieldController.transform.
                    DOLocalRotate(rotateVector, rotateDuration, RotateMode.FastBeyond360).SetRelative(true).SetEase(rotationEase).SetLoops(-1);
            }
        }
        // This is for shield to move towards right
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (rightShieldRotateTween == null)
            {
                Vector3 rotateVector = new Vector3(0, 0, -360);
                rightShieldRotateTween = rightShieldController.transform.
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

    #region Change Color Mechanic

    private void ChangeShieldColor()
    {
        // Change the Left shield's color
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetShieldColor(LeftShieldColor, RightShieldColor, true);
        }
        // Change the Right shield's color
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetShieldColor(LeftShieldColor, RightShieldColor, false);
        }
    }

    /// <summary>
    /// Set the shields color. This function finds available main color and give that color to the target shield.
    /// </summary>
    /// <param name="_leftShieldColor">Use global Left Shield Variable that stores left shields Color Key</param>
    /// <param name="_rightShieldColor">Use global Right Shield Variable that stores right shields Color Key</param>
    /// <param name="isLeft">Indicate the target shield to change it's color. True for Left Shield, false for Right Shield</param>
    private void SetShieldColor(AllColors _leftShieldColor, AllColors _rightShieldColor, bool isLeft)
    {
        AllColors availableColor;

        List<AllColors> totalColors = new List<AllColors>();
        totalColors.Add(AllColors.Red);
        totalColors.Add(AllColors.Green);
        totalColors.Add(AllColors.Blue);


        totalColors.Remove(_leftShieldColor);
        totalColors.Remove(_rightShieldColor);

        availableColor = totalColors[0];

        print("<color=yellow>" + "Available Color is: " + availableColor.ToString() + "</color>");


        if (GameManager.Instance.IsFusionActive)
        {
            // Pop You Can Not Change The Color of the shield While Fusion Shield is Active
        }
        else
        {
            if (isLeft)
            {
                leftShield.color = ColorManager.Instance.ReturnColor(availableColor);
                LeftShieldColor = availableColor;
            }
            else
            {
                rightShield.color = ColorManager.Instance.ReturnColor(availableColor);
                RightShieldColor = availableColor;
            }
        }
    }

    #endregion

    private void RotateColorMachine()
    {
        Vector3 rotateVector = new Vector3(0, 0, 360);
        colorMachineTween = colorMachineGo.transform.
                    DOLocalRotate(rotateVector, rotateDuration * 10, RotateMode.FastBeyond360).SetRelative(true).SetEase(rotationEase).SetLoops(-1);
    }

    public void SetLeftShieldsColorAtStart(Color color, AllColors mainColorKey)
    {
        leftShield.color = color;

        LeftShieldColor = mainColorKey;
    }
    public void SetRightShieldsColorAtStart(Color color, AllColors mainColorKey)
    {
        rightShield.color = color;

        RightShieldColor = mainColorKey;
    }

    private void SetFusionShieldColor(AllColors firstColor, AllColors secondColor)
    {
        List<AllColors> colorCombinationList = new List<AllColors>();

        colorCombinationList.Add(firstColor);
        colorCombinationList.Add(secondColor);

        // This means it will be magenta
        if (colorCombinationList.Contains(AllColors.Red) && colorCombinationList.Contains(AllColors.Blue))
        {
            fusionShield.color = ColorManager.Instance.ReturnColor(AllColors.Magenta);
            FusionShieldColor = AllColors.Magenta;
        }
        else if (colorCombinationList.Contains(AllColors.Red) && colorCombinationList.Contains(AllColors.Green))
        {
            fusionShield.color = ColorManager.Instance.ReturnColor(AllColors.Yellow);
            FusionShieldColor = AllColors.Yellow;
        }
        else if (colorCombinationList.Contains(AllColors.Blue) && colorCombinationList.Contains(AllColors.Green))
        {
            fusionShield.color = ColorManager.Instance.ReturnColor(AllColors.Cyan);
            FusionShieldColor = AllColors.Cyan;
        }

    }

    public void ActivateFusionShield()
    {
        GameManager.Instance.IsFusionActive = true;

        SetFusionShieldColor(LeftShieldColor, RightShieldColor);

        float fusionShieldRotationZ = Mathf.Abs(leftShieldController.transform.eulerAngles.z) + Mathf.Abs(rightShieldController.transform.eulerAngles.z);

        if (Mathf.Abs(leftShieldController.transform.eulerAngles.z - rightShieldController.transform.eulerAngles.z) > 90)
        {
            fusionShieldRotationZ = (fusionShieldRotationZ + 360) / 2;
        }
        else
        {
            fusionShieldRotationZ = (fusionShieldRotationZ) / 2;
        }

        fusionShieldController.transform.DORotate(new Vector3(0, 0, fusionShieldRotationZ), 0.001f);

        fusionShield.gameObject.SetActive(true);

        DOVirtual.DelayedCall(0.01f, delegate 
        {
            fusionShieldTween.Kill();

            float fullScale = 1f;
            float duration = 0.75f;

            fusionShieldTween = fusionShield.gameObject.transform.DOScaleY(fullScale, duration);
        });
        
    }

    public void DeactivateFusionShield()
    {
        GameManager.Instance.IsFusionActive = false;
        fusionShieldTween.Kill();
        fusionShieldRotateTween.Kill();

        float duration = 0.3f;

        fusionShieldTween = fusionShield.gameObject.transform.DOScaleY(0, duration).OnComplete(delegate 
        {
            fusionShield.gameObject.SetActive(false);
        });
    }

    private void RotateFusionShield()
    {
        if (GameManager.Instance.IsFusionActive)
        {
            float fusionShieldRotationZ = Mathf.Abs(leftShieldController.transform.eulerAngles.z) + Mathf.Abs(rightShieldController.transform.eulerAngles.z);

            if (Mathf.Abs(leftShieldController.transform.eulerAngles.z - rightShieldController.transform.eulerAngles.z) > 90)
            {
                fusionShieldRotationZ = (fusionShieldRotationZ + 360) / 2;
            }
            else
            {
                fusionShieldRotationZ = (fusionShieldRotationZ) / 2;
            }

            fusionShieldRotateTween = fusionShieldController.transform.DORotate(new Vector3(0, 0, fusionShieldRotationZ), 0.1f);
        }
    }

    private void GameOver()
    {
        colorMachineTween.Kill();
        fusionShieldTween.Kill();
        fusionShieldRotateTween.Kill();
        leftShieldRotateTween.Kill();
        rightShieldRotateTween.Kill();
    }

    #endregion

}
