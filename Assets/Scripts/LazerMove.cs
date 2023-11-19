using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Tilemaps;
using Unity.Mathematics;

public class LazerMove : MonoBehaviour
{
    private int derece;
    public static int choosenLazerColor;
    private bool lazerActive;
    private bool hasCreated = false;
    private Tween moveTween;
    [SerializeField] private Ease moveEase;
    public AllColors lazerColor;
    private int lazerTimer;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        if (hasCreated)
        {
            ColorChoose(SpawnManager.Instance.lazerType);
            DOVirtual.DelayedCall(0.1f, SetObjectColor);
            TakePlace();
            moveTween = transform.DOMove(new Vector3(0, 0, 0), lazerTimer, false).SetEase(moveEase); //Harekete geç
        }
        else
        {
            hasCreated = true;
        }


        GameManager.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        moveTween.Kill();
    }

    void Update()
    {
        if (transform.position.x == 0 && transform.position.y == 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void GameOver()
    {
        moveTween.Kill();

        if (this != null && this.isActiveAndEnabled)
        {
            gameObject.SetActive(false);
        }
    }

    void TakePlace()
    {
        transform.position = Vector2.zero;
        derece = UnityEngine.Random.Range(0, 360);
        transform.Rotate(0, 0, derece);
        transform.position += transform.right * 12;
    }
    
    private void SetObjectColor()
    {
        if (ColorManager.Instance != null)
        {
            spriteRenderer.color = ColorManager.Instance.ReturnColor(lazerColor);
        }
    }

    void ColorChoose(string Mode)
    {
        int Chooser;
        if (Mode == "Ana")
        {
            Chooser = UnityEngine.Random.Range(1, 7);
        }
        else if (Mode == "Ara")
        {
            Chooser = UnityEngine.Random.Range(7, 10);
        }
        else if (Mode == "All")
        {
            Chooser = UnityEngine.Random.Range(1, 10);
        }
        else if (Mode == "2Ana")
        {
            do
            {
                Chooser = UnityEngine.Random.Range(1, 7);
            } while (Mathf.Abs(Chooser - choosenLazerColor) <= 1);
            choosenLazerColor = Chooser;
        }
        else
        {
            Chooser = UnityEngine.Random.Range(1, 7);
        }
        //~%22 ana renkler ~%11 ara renkler || Ayrýca seçtiði renge göre hýzýný da ayarlar
        if (Chooser == 1 || Chooser == 2) //ana renkler
        {
            lazerColor = AllColors.Red;
        }
        else if (Chooser == 3 || Chooser == 4)
        {
            lazerColor = AllColors.Blue;
        }
        else if (Chooser == 5 || Chooser == 6)
        {
            lazerColor = AllColors.Green;
        }
        else if (Chooser == 7) //ara renkler
        {
            lazerColor = AllColors.Cyan;
        }
        else if (Chooser == 8)
        {
            lazerColor = AllColors.Magenta;
        }
        else if (Chooser == 9)
        {
            lazerColor = AllColors.Yellow;
        }
        if (Time.time < 100) //Hýz ayarý
        {
            lazerTimer = 6; 
        }
        else if (Chooser < 7)
        {
            lazerTimer = 6; //ana renkler
        }
        else if (Chooser < 10)
        {
            lazerTimer = 7; //ara renkler
        }
    }
}
