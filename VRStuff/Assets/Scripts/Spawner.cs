using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject spawnObject;

    [SerializeField]
    int maxCount = 1;

    [SerializeField]
    float spawnTime = 0.0f;

    float spawntimer = 0.0f;

    void Start()
    {
        Instantiate(spawnObject, transform);
    }

    void Update()
    {
        if(GameManager.State != GameManager.eState.on && GameManager.State != GameManager.eState.end)
        {
            if(transform.childCount < maxCount)
            {
                spawntimer += Time.deltaTime;
                if (spawntimer >= spawnTime)
                {
                    Instantiate(spawnObject, transform);
                    spawntimer = 0;
                }
            }
        }
    }
}
