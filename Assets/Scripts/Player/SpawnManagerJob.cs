using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
public struct PlayerManagerUpdate : IJobParallelFor
{
    public NativeArray<PlayerManager.Data> SpawnPointDataArray;

    public void Execute(int index)
    {
        var data = SpawnPointDataArray[index];
        // Perform your logic on the spawn point data here
        // For example, you might want to update some values or perform specific actions
        // data.PlayerID += 1;
        // data.IsAlive = false;
        // data.SpawnPosition = new Vector3(0, 0, 0);
        SpawnPointDataArray[index] = data;
    }
}

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private List<PlayerManager> spawnPoints;

    private NativeArray<PlayerManager.Data> spawnPointDataArray;

    private void OnEnable()
    {
        spawnPointDataArray = new NativeArray<PlayerManager.Data>(spawnPoints.Count, Allocator.Persistent);
    }

    private void OnDisable()
    {
        spawnPointDataArray.Dispose();
    }

    private void Update()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnPointDataArray[i] = new PlayerManager.Data
            {
                PlayerID = i, // Set an example value for PlayerID
                IsAlive = true, // Set an example value for IsAlive
                SpawnPosition = spawnPoints[i].transform.position // Set the spawn position
            };
        }

        // Create a job with the required data
        var job = new PlayerManagerUpdate
        {
            SpawnPointDataArray = spawnPointDataArray
        };

        // Schedule the job for parallel execution
        var jobHandle = job.Schedule(spawnPoints.Count, 1);

        // Ensure the job is completed before moving on
        jobHandle.Complete();
    }
}
