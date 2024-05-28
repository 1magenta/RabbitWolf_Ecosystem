using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public float energy = 100;

    protected virtual void Start()
    {
        // Register this creature with the EnvironmentManager
        EnvironmentManager.Instance.RegisterCreature(this);
    }

    protected virtual void Update()
    {
        // Decrease energy over time
        energy -= Time.deltaTime;

        // If energy falls below zero, die
        if (energy <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Unregister this creature with the EnvironmentManager
        EnvironmentManager.Instance.UnregisterCreature(this);

        // Destroy the game object
        Destroy(gameObject);
    }
}
