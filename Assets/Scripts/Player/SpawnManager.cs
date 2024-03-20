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
        Instance = this;

        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    public Transform GetSpawnPoint ()
    {
        Debug.Log(spawnPoints.Length);
        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
    }
}
