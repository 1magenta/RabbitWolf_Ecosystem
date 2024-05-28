using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public Environment environment;
    public GameObject rabbitPrefab;
    public GameObject wolfPrefab;
    public int initialRabbitCount = 50;
    public int initialWolfCount = 10;

    private void Start()
    {
        if (environment == null)
        {
            environment = FindObjectOfType<Environment>();
            if (environment == null)
            {
                Debug.LogError("Environment not found. Please attach Environment script to a GameObject.");
                return;
            }
        }

        InitializeAnimals();
    }

    private void InitializeAnimals()
    {
        for (int i = 0; i < initialRabbitCount; i++)
        {
            SpawnAnimal(rabbitPrefab);
        }

        for (int i = 0; i < initialWolfCount; i++)
        {
            SpawnAnimal(wolfPrefab);
        }
    }

    private void SpawnAnimal(GameObject animalPrefab)
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-environment.spawnArea.x / 2, environment.spawnArea.x / 2),
            0.5f,
            Random.Range(-environment.spawnArea.z / 2, environment.spawnArea.z / 2)
        );

        Instantiate(animalPrefab, randomPosition, Quaternion.identity);
    }


    private void Update()
    {

    }
}
