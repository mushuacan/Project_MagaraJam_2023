using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LazerMove : MonoBehaviour
{
    private int derece;
    private bool lazerActive;
    private Tween moveTween;
    [SerializeField] private Ease moveEase;
    private AllColors lazerColor;
    private int lazerTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lazerActive = false;
            ColorChoose(Random.Range(1, 9));//~%22 ana renkler ~%11 ara renkler || Ayrýca seçtiði renge göre hýzýný da ayarlar
            Konumlan();
            lazerActive = true;
            moveTween = transform.DOMove(new Vector3(0, 0, 0), lazerTimer, false).SetEase(moveEase); //Harekete geç
        }
    }
    void Konumlan()
    {
        transform.position = Vector2.zero;
        derece = Random.Range(0, 360);
        transform.Rotate(0, 0, derece);
        transform.position += transform.right * 12;
    }
    void ColorChoose(int Chooser)
    {
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
