using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Pool;
using Unity.VisualScripting;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    private float laserSpawnTime;
    private float TimeInGame;
    private float TimeGameStarted;
    [HideInInspector]
    public string lazerType;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        laserSpawnTime = 3f;

        GameManager.OnGameStarted += StartLazers;
    }
    private void OnDisable()
    {
        GameManager.OnGameStarted -= StartLazers;
    }

    private void StartLazers ()
    {
        TimeGameStarted = Time.time;
        StartCoroutine(LazerCreate());
    }

    private void LazerSetActive()
    {
        GameObject lazer = LazerPool.SharedInstance.GetPooledObject();
        if (lazer != null)
        {
            lazer.SetActive(true);
        }
    }

    IEnumerator LazerCreate()
    {
        
        //Debug.Log(Time.time);
        TimeInGame = Time.time - TimeGameStarted;
        if (TimeInGame < 10)
        {
            lazerType = "Ana";
            laserSpawnTime = 4;
            LazerSetActive();

        }
        else if (TimeInGame < 20)
        {
            lazerType = "Ana";
            laserSpawnTime = 3;
            LazerSetActive();
        }
        else if (TimeInGame < 40)
        {
            lazerType = "2Ana";
            laserSpawnTime = 4;
            LazerSetActive();
            LazerSetActive();
        }
        else if (TimeInGame < 60)
        {
            lazerType = "Ara";
            laserSpawnTime = 4;
            LazerSetActive();
        }
        else
        {
            int lazerRandom = Random.Range(0, 100);
            if (lazerRandom <= 50)
            {
                lazerType = "2Ana";
                laserSpawnTime = 3;
                LazerSetActive();
                LazerSetActive();
            } else if (lazerRandom <= 70)
            {
                lazerType = "Ara";
                laserSpawnTime = 3;
                LazerSetActive();
            } else if (lazerRandom <= 90)
            {
                lazerType = "Ana";
                laserSpawnTime = 2;
                LazerSetActive();
            } else if(lazerRandom <= 100)
            {
                lazerType = "All";
                laserSpawnTime = 2;
                LazerSetActive();
            }
        }
        
        yield return new WaitForSeconds(laserSpawnTime);
        if (GameManager.Instance.IsGameOn)
        {
            StartCoroutine(LazerCreate());
        }
    }

    
}
