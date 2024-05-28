using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Animals
{
    public float preySearchRadius = 30.0f;
    public GameObject wolfPrefab;

    private float[] breedVitalities = { 60, 40 };
    private int nextBreedIndex = 0;
    private float breedCheckDelay = 0;


    private void Awake()
    {
        CountManager.Instance.IncrementCount("Wolf");
        // Randomly initialize the stamina and vitality
        stamina = Random.Range(40, 101); 
        vitality = Random.Range(70, 101); 
        // Set a random delay between 0 to 10 seconds for the initial breeding check
        breedCheckDelay = Random.Range(0f, 20f);
    }


    protected virtual void FixedUpdate()
    {
        base.FixedUpdate();
        // wolf search for rabbits
        if (!HasTarget())
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, preySearchRadius);
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Rabbit")) 
                {
                    SetTarget(hit.transform);
                    break; 
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
        // wolf search and eat rabbit
        Collider[] hits = Physics.OverlapSphere(transform.position, 5.0f);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Rabbit"))
            {
                Debug.Log("Wolf hit rabbit£º " + stamina);
                // The wolf consumes the rabbit and gains stamina.
                ConsumeFood(hit.gameObject, 70);
                if (hit.gameObject != null)
                {
                    CountManager.Instance.DecrementCount("Rabbit");
                }
                break;
            }
        }
    }

    private void Breed()
    {
        if (nextBreedIndex >= breedVitalities.Length || vitality > breedVitalities[nextBreedIndex])
        {
            nextBreedIndex = 0; // reset to the highest vitality threshold
        }

        if (nextBreedIndex < breedVitalities.Length && vitality <= breedVitalities[nextBreedIndex] && stamina >= 50)
        {
            //only 50% will breed
            if (Random.value <= 0.5f)
            {
                CreateNewWolf();
                stamina -= 25; // reduce stamina by a certain amount after breeding
                vitality -= 5;
                nextBreedIndex++; // move to the next breeding threshold
            }
        }

        if (nextBreedIndex >= breedVitalities.Length || vitality > breedVitalities[nextBreedIndex])
        {
            nextBreedIndex = 0; // Reset to the highest vitality threshold
        }
    }

    private void CreateNewWolf()
    {
        Debug.Log("Wolf Born, vitality = " + vitality + "stamina = " + stamina);
        Instantiate(wolfPrefab, transform.position + new Vector3(Random.Range(-3, 3), 0, Random.Range(-3, 3)), Quaternion.identity);
    }
}
