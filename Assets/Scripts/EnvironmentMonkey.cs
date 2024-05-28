using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMonkey : MonoBehaviour
{
    public GameObject foodPrefab; // Prefab for the food item. This can be any 3D model representing food.
    public int initialFoodCount = 100; // Number of food items to spawn initially.
    public GameObject enviroTreePrefab;
    public int initialTreeCount = 100;
    public Vector3 spawnArea = new Vector3(100f, 0f, 100f); // Area in which food can be randomly spawned.

    public float foodRegrowthRate = 5.0f; // Time in seconds for new food to grow. Lower value = faster growth.
    public int maxFoodCount = 200;

    private void Awake()
    {
        CountManagerMonkey.Instance.IncrementCount("Food");
        /*    CountManager.Instance.IncrementCount("Tree");*/
    }
    private void Start()
    {
        // Populate the environment with initial food items.
        for (int i = 0; i < initialFoodCount; i++)
        {
            RegenerateFood();
        }
        InitializeEnviro();
        StartCoroutine(FoodGrowthCycle());
    }
    private void InitializeEnviro()
    {
        for (int i = 0; i < initialFoodCount; i++)
        {
            Spawn(foodPrefab);
        }
        for (int i = 0; i < initialTreeCount; i++)
        {
            Spawn(enviroTreePrefab);
        }
    }

    private void Spawn(GameObject enviroPrefab)
    {
        // We're using the spawnArea from the Environment script to determine where animals can spawn.
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            0.5f, // Assuming animals spawn slightly above the ground.
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );

        Instantiate(enviroPrefab, randomPosition, Quaternion.identity);
    }


    // Coroutine for regenerating food items based on a set interval (foodRegrowthRate).
    IEnumerator FoodGrowthCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(foodRegrowthRate);        
            RegenerateFood();
            if (CurrentFoodCount() < maxFoodCount)
            {
                Debug.Log("New Food grow");
                CountManagerMonkey.Instance.IncrementCount("Food");
                RegenerateFood();
            }
        }
    }

    // Method to regenerate a food item at a random position within the spawn area.
    public void RegenerateFood()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            0.5f, // Assuming food spawns slightly above the ground to prevent clipping.
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );

        Instantiate(foodPrefab, randomPosition, Quaternion.identity);
        CountManagerMonkey.Instance.IncrementCount("Food");
    }

    private int CurrentFoodCount()
    {
        return GameObject.FindGameObjectsWithTag("Food").Length; // Assuming food items have the tag "Food".
    }

    // [Optional] Method to decrease food count in the environment or check for food availability.
    public bool IsFoodAvailable()
    {
        // This is a placeholder. You can replace this with actual logic to check for food entities.
        return true;
    }
    public Vector3 GetSpawnArea()
    {
        return spawnArea;
    }
}
