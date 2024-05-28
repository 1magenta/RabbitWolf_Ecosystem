using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManagerMonkey : MonoBehaviour
{
    public EnvironmentMonkey environment;
    public GameObject monkeyPrefab;
    /*public GameObject wolfPrefab;*/
    public int initialMonkeyCount = 50;
    /*public int initialWolfCount = 10;*/

    private void Start()
    {
        if (environment == null)
        {
            environment = FindObjectOfType<EnvironmentMonkey>();
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
        for (int i = 0; i < initialMonkeyCount; i++)
        {
            SpawnAnimal(monkeyPrefab);
            CountManagerMonkey.Instance.IncrementCount("Monkey");
        }
        
    }

    private void SpawnAnimal(GameObject animalPrefab)
    {
        // We're using the spawnArea from the Environment script to determine where animals can spawn.
        Vector3 randomPosition = new Vector3(
            Random.Range(-environment.spawnArea.x / 2, environment.spawnArea.x / 2),
            0.5f, // Assuming animals spawn slightly above the ground.
            Random.Range(-environment.spawnArea.z / 2, environment.spawnArea.z / 2)
        );

        Instantiate(animalPrefab, randomPosition, Quaternion.identity);
    }

    // Optionally: Update loop to monitor the simulation, apply rules, etc.
    private void Update()
    {
        // Any simulation-wide checks or updates can go here.
    }
}
