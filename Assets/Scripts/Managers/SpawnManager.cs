using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject lazer = LazerPool.SharedInstance.GetPooledObject();
        if (lazer != null)
        {
            lazer.SetActive(true);
        }
    }

    
}
