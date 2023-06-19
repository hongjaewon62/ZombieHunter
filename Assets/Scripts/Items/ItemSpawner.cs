using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    public Transform playerTransform;

    public float maxDistance = 5f;

    public float MaxSpawnCooldown = 7f;
    public float MinSpawnCooldown = 2f;
    private float spawnTime;

    private float lastSpawnTime;

    private void Start()
    {
        spawnTime = Random.Range(MinSpawnCooldown, MaxSpawnCooldown);
        lastSpawnTime = 0;
    }

    private void Update()
    {
        if(!GameManager.instance.isGameover)
        {
            if (Time.time >= lastSpawnTime + spawnTime && playerTransform != null)
            {
                lastSpawnTime = Time.time;
                spawnTime = Random.Range(MinSpawnCooldown, MaxSpawnCooldown);
                Spawn();
            }
        }
    }

    private void Spawn()
    {
        Vector3 spawnPosition = GetRandomPointOnNavMesh(playerTransform.position, maxDistance);
        spawnPosition += Vector3.up * 0.5f;

        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);

        Destroy(item, 10f);
    }

    private Vector3 GetRandomPointOnNavMesh(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas);

        return hit.position;
    }
}
