using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Animals
{
    public float foodSearchRadius = 15.0f;
    
    public GameObject rabbitPrefab;
    private float[] breedVitalities = { 80, 60, 40, 20 };
    private int nextBreedIndex = 0;
    private float breedCheckDelay = 0;


    private void Awake()
    {
        CountManager.Instance.IncrementCount("Rabbit");
        // Randomly initialize the stamina and vitality within a range
        stamina = Random.Range(40, 101);  // Assuming stamina can be 100 at max
        vitality = Random.Range(70, 101); // Assuming vitality can be 100 at max

        // Set a random delay between 0 to 10 seconds for the initial breeding check
        breedCheckDelay = Random.Range(0f, 20f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        // Logic to make the rabbit search for food
        if (!HasTarget()) // We'll assume HasTarget() checks if the animal currently has a target
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, foodSearchRadius);
            foreach (Collider hit in hits)
            {
                // If the rabbit finds a food item, set it as the target
                if (hit.CompareTag("Grass")) // Assuming food items have a "Food" tag
                {
                    SetTarget(hit.transform);
                    break; // Break out of loop once a food item is found
                }
            }
        }

        if (breedCheckDelay > 0)
        {
            breedCheckDelay -= Time.deltaTime;
            return;
        }
        Breed();
    }

    protected override void Eat()
    {
        // Logic for rabbit to search and eat grass
        Collider[] hits = Physics.OverlapSphere(transform.position, 5.0f);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Grass"))
            {
                // The rabbit consumes the grass and gains stamina.
                Debug.Log("Rabbit hit grass£º " + stamina);
                ConsumeFood(hit.gameObject, 50);
                if(hit.gameObject != null)
                {
                    CountManager.Instance.DecrementCount("Grass");
                }
                break;
            }
        }
    }

    private void Breed()
    {
        if (nextBreedIndex < breedVitalities.Length && vitality <= breedVitalities[nextBreedIndex] && stamina >= 50)
        {
            //only 50% of rabbits will breed
            if (Random.value <= 0.5f)
            {
                CreateNewRabbit();
                stamina -= 25; // reduce stamina by a certain amount after breeding
                vitality -= 5;
                nextBreedIndex++; // move to the next breeding threshold
            }
        }

        if (nextBreedIndex >= breedVitalities.Length || vitality > breedVitalities[nextBreedIndex])
        {
            nextBreedIndex = 0; // reset to the highest vitality threshold
        }
    }

    private void CreateNewRabbit()
    {
        Debug.Log("Rabbit Born, vitality = " + vitality + "stamina = " + stamina);
        Vector3 randomPosition = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        Instantiate(rabbitPrefab, transform.position + randomPosition, Quaternion.identity);
    }
}