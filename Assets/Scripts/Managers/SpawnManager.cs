using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Pool;
using Unity.VisualScripting;

public class SpawnManager : MonoBehaviour
{

    private float laserSpawnTime;

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
        StartCoroutine(LazerCreate());
    }

    IEnumerator LazerCreate()
    {
        GameObject lazer = LazerPool.SharedInstance.GetPooledObject();
        if (lazer != null)
        {
            lazer.SetActive(true);
        }
        yield return new WaitForSeconds(laserSpawnTime);

        if (GameManager.Instance.IsGameOn)
        {
            StartCoroutine(LazerCreate());
        }
    }

    
}
