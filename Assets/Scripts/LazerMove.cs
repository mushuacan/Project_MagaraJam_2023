using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LazerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private int derece;
    private bool lazerActive;
    private Tween moveTween;
    [SerializeField] private Ease moveEase;

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lazerActive = false;
            transform.position = Vector2.zero;
            Debug.Log("Space detected");
            derece = Random.Range(0, 360);
            transform.Rotate(0, 0, derece);
            transform.position += transform.right * 12;
            lazerActive = true;
            moveTween = transform.DOMove(new Vector3(0, 0, 0), 4, false).SetEase(moveEase);
        }
    }
}
