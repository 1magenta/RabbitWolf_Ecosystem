using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public GameObject foodPrefab; 
    public int initialFoodCount = 100; // Number of food items to spawn initially.
    public Vector3 spawnArea = new Vector3(100f, 0f, 100f); // Area in which food can be randomly spawned.

    public float foodRegrowthRate = 5.0f; 
    public int maxFoodCount = 200;

    private void Awake()
    {
        CountManager.Instance.IncrementCount("Grass");
    }
    private void Start()
    {
        // Populate the environment with initial food items.
        for (int i = 0; i < initialFoodCount; i++)
        {
            RegenerateFood();
        }

        StartCoroutine(FoodGrowthCycle());
    }

    IEnumerator FoodGrowthCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(foodRegrowthRate);        
            RegenerateFood();
            if (CurrentFoodCount() < maxFoodCount)
            {
                Debug.Log("New plant grow");
                CountManager.Instance.IncrementCount("Grass");
                RegenerateFood();
            }
        }
    }

    // Method to regenerate a food item at a random position within the spawn area.
    public void RegenerateFood()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            0.5f,
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );

        Instantiate(foodPrefab, randomPosition, Quaternion.identity);
        CountManager.Instance.IncrementCount("Grass");
    }

    private int CurrentFoodCount()
    {
        return GameObject.FindGameObjectsWithTag("Grass").Length; 
    }

    public bool IsFoodAvailable()
    {
        if(gameObject != null)
        {
            return true;
        }
        return false;
    }
    public Vector3 GetSpawnArea()
    {
        return spawnArea;
    }
}
