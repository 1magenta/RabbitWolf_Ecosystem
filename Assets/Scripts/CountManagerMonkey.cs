using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountManagerMonkey : MonoBehaviour
{
    public static CountManagerMonkey Instance; // Singleton instance

    public int MonkeyCount { get; private set; } = 0;
    public int FoodCount { get; private set; } = 0;

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
            case "Food":
                FoodCount++;
                break;
            case "Monkey":
                MonkeyCount++;
                break;
        }
    }

    public void DecrementCount(string tag)
    {
        switch (tag)
        {
            case "Food":
                FoodCount--;
                break;
            case "Monkey":
                MonkeyCount--;
                break;
        }
    }
}
