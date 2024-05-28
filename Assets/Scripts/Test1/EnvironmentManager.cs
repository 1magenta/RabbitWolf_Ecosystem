using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance { get; private set; }

    public List<Creature> creatures = new List<Creature>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterCreature(Creature creature)
    {
        creatures.Add(creature);
    }

    public void UnregisterCreature(Creature creature)
    {
        creatures.Remove(creature);
    }

    public Plant FindNearbyPlant(Vector3 position)
    {
        // This is a very naive implementation, you might want to use a more efficient method
        foreach (Creature creature in creatures)
        {
            Plant plant = creature as Plant;
            if (plant != null && Vector3.Distance(position, plant.transform.position) < 5)
            {
                return plant;
            }
        }

        return null;
    }
}
