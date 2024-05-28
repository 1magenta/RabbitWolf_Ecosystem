using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : Creature
{
    public float hungerRate = 1;
    public float eatAmount = 20;
    public float speed = 2.0f;

    private Vector3 targetPosition;
    private Plant targetPlant = null;


    protected override void Start()
    {
        base.Start();
        SetNewRandomTarget();
    }

    protected override void Update()
    {
        base.Update();
        MoveToTarget();
    }

    private void SetNewRandomTarget()
    {
        Plant nearbyPlant = EnvironmentManager.Instance.FindNearbyPlant(transform.position);
        if (nearbyPlant != null)
        {
            targetPlant = nearbyPlant;
            targetPosition = nearbyPlant.transform.position;
        }
         targetPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }

    private void MoveToTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, targetPosition) < 0.5)
        {
            SetNewRandomTarget();
        }
    }

    private IEnumerator EatAndMoveAway(Plant plant)
    {
        // Make the animal stop
        targetPosition = transform.position;

        // Eat the plant
        float amountEaten = Mathf.Min(plant.energy, eatAmount);
        //targetPlant.energy -= amountEaten; 
        energy += amountEaten;
        Debug.Log("animalEnerge: " + energy);

        // Wait for 1 seconds
        yield return new WaitForSeconds(1f);

        SetNewRandomTarget();
        MoveToTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        Plant plant = other.gameObject.GetComponent<Plant>();
        if (plant != null)
        {
            /*            // Eat the plant
                        float amountEaten = Mathf.Min(plant.energy, eatAmount);
                        //targetPlant.energy -= amountEaten; 
                        energy += amountEaten;
                        Debug.Log("animalEnerge: " + energy);
            */

            StartCoroutine(EatAndMoveAway(plant));
        }
    }
}
