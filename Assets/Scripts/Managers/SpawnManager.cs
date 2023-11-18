using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lazerer());
    }
    IEnumerator Lazerer()
    {
        GameObject lazer = LazerPool.SharedInstance.GetPooledObject();
        if (lazer != null)
        {
            lazer.SetActive(true);
        }
        yield return new WaitForSeconds(0.2f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Lazerer());
        }
    }
}
