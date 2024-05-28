using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountManager : MonoBehaviour
{
    public static CountManager Instance; // Singleton instance

    public int PlantCount { get; private set; } = 0;
    public int RabbitCount { get; private set; } = 0;
    public int WolfCount { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementCount(string tag)
    {
        switch (tag)
        {
            case "Grass":
                PlantCount++;
                break;
            case "Rabbit":
                RabbitCount++;
                break;
            case "Wolf":
                WolfCount++;
                break;
        }
    }

    public void DecrementCount(string tag)
    {
        switch (tag)
        {
            case "Grass":
                PlantCount--;
                break;
            case "Rabbit":
                RabbitCount--;
                break;
            case "Wolf":
                WolfCount--;
                break;
        }
    }
}
