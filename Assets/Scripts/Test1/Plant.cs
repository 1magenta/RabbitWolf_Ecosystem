using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Creature
{
    //private Vector3 originalScale;
    //private int initialEnergy = 200;

    protected override void Start()
    {
        base.Start();
        //originalScale = transform.localScale;
    }

    protected override void Update()
    {
        //base.Update();
        // Adjust the size of the plant according to its remaining energy
        //transform.localScale = originalScale * (energy / initialEnergy);

        // Destroy the plant if its energy is depleted
        if (energy <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Herbivore")
        {
            energy -= 20;
            Debug.Log("plantEnerge: " + energy);
        }
    }

}
