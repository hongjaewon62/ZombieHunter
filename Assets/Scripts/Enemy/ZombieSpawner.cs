using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public Zombie[] zombiePrefab;
    public Transform[] spawnPoints;
    public GameObject parentObj;
    private List<Zombie> zombies = new List<Zombie>();

    void Update()
    {
        if(!GameManager.instance.isGameover)
        {
            if (UiManager.instance.tempSurviveTime < 60)
            {
                if (zombies.Count <= 80)
                {
                    Spawn();
                }
            }
            else if (UiManager.instance.tempSurviveTime > 60)
            {
                if (zombies.Count <= 100)
                {
                    Spawn();
                }
            }
            else if (UiManager.instance.tempSurviveTime > 180)
            {
                if (zombies.Count <= 200)
                {
                    Spawn();
                }
            }
            else if (UiManager.instance.tempSurviveTime > 300)
            {
                if (zombies.Count <= 250)
                {
                    Spawn();
                }
            }
            else if (UiManager.instance.tempSurviveTime > 600)
            {
                if (zombies.Count <= 500)
                {
                    Spawn();
                }
            }
        }
    }

    private void Spawn()
    {
        int zombieCount = 10;
        for(int i = 0; i < zombieCount; i++)
        {
            CreateZombie();
        }
    }
    private void CreateZombie()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Zombie zombie;
        if(UiManager.instance.tempSurviveTime < 60)
        {
            zombie = Instantiate(zombiePrefab[Random.Range(0, 12)], spawnPoint.position, spawnPoint.rotation);
            zombie.transform.parent = parentObj.transform;
            zombies.Add(zombie);
            zombie.onDeath += () => zombies.Remove(zombie);
            zombie.onDeath += () => Destroy(zombie.gameObject, 4f);
        }
        else if(UiManager.instance.tempSurviveTime > 60)
        {
            zombie = Instantiate(zombiePrefab[Random.Range(0, 21)], spawnPoint.position, spawnPoint.rotation);
            zombie.transform.parent = parentObj.transform;
            zombies.Add(zombie);
            zombie.onDeath += () => zombies.Remove(zombie);
            zombie.onDeath += () => Destroy(zombie.gameObject, 4f);
        }
        else if (UiManager.instance.tempSurviveTime > 180)
        {
            zombie = Instantiate(zombiePrefab[Random.Range(0, 26)], spawnPoint.position, spawnPoint.rotation);
            zombie.transform.parent = parentObj.transform;
            zombies.Add(zombie);
            zombie.onDeath += () => zombies.Remove(zombie);
            zombie.onDeath += () => Destroy(zombie.gameObject, 4f);
        }
        else if (UiManager.instance.tempSurviveTime > 300)
        {
            zombie = Instantiate(zombiePrefab[Random.Range(0, 31)], spawnPoint.position, spawnPoint.rotation);
            zombie.transform.parent = parentObj.transform;
            zombies.Add(zombie);
            zombie.onDeath += () => zombies.Remove(zombie);
            zombie.onDeath += () => Destroy(zombie.gameObject, 4f);
        }
        else if (UiManager.instance.tempSurviveTime > 480)
        {
            zombie = Instantiate(zombiePrefab[Random.Range(13, 36)], spawnPoint.position, spawnPoint.rotation);
            zombie.transform.parent = parentObj.transform;
            zombies.Add(zombie);
            zombie.onDeath += () => zombies.Remove(zombie);
            zombie.onDeath += () => Destroy(zombie.gameObject, 4f);
        }
    }
}
