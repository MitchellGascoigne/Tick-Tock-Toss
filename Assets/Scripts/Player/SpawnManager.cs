using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    SpawnPoint[] spawnPoints;
    void Awake ()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    public Transform GetSpawnPoint ()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
    }
}
