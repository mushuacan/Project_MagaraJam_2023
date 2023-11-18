using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Tilemaps;

public class LazerMove : MonoBehaviour
{
    private int derece;
    private bool lazerActive;
    private bool hasCreated = false;
    private Tween moveTween;
    [SerializeField] private Ease moveEase;
    private AllColors lazerColor;  
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
            ColorChoose(Random.Range(1, 10));
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

        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    void TakePlace()
    {
        transform.position = Vector2.zero;
        derece = Random.Range(0, 360);
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

    void ColorChoose(int Chooser)
    {
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
        if (Chooser < 7) //Hýz ayarý
        {
            lazerTimer = 4;
        }
        else
        {
            lazerTimer = 6;
        }
    }
}
