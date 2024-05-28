using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour
{
    public float speed = 3.0f;
    public float turnSpeed = 120.0f;
    public float wanderRadius = 10.0f; // How far the animal can wander from its current position.
    public float wanderTimer = 5.0f; // Duration between wander direction changes.

    public float vitality = 100.0f; // Max 100
    public float stamina = 100.0f; // Max 100
    public float vitalityDecreaseRate = 0.5f; // How fast vitality decreases over time
    public float staminaDecreaseRate = 1.0f; // How fast stamina decreases over time

    private Transform target; // This can be set to food or prey later on.
    private float currentWanderTime = 0.0f;
    private Vector3 wanderPoint;

    private Rigidbody rb;



    protected virtual void FixedUpdate()
    {
        DecreaseVitality(vitalityDecreaseRate * Time.deltaTime);
        DecreaseStamina(staminaDecreaseRate * Time.deltaTime);

        if (target)
        {
            Move(target.position);
            // if close to the target, reset the target.
            if (Vector3.Distance(transform.position, target.position) < 1f)
            {
                target = null; // Reset target once close enough.
            }
        }
        else
        {
            Wander();
        }
    }

    protected void Move(Vector3 destination)
    {
        // Calculate direction to move towards.
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Adjust the animal's rotation to face the destination.
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }

    private void Wander()
    {
        currentWanderTime += Time.deltaTime;

        if (currentWanderTime >= wanderTimer || wanderPoint == Vector3.zero)
        {
            wanderPoint = RandomWanderPoint();
            currentWanderTime = 0.0f;
        }

        Move(wanderPoint);

    }

    private Vector3 RandomWanderPoint()
    {
        // Calculate a random wander point based on the current position and wander radius.
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        randomPoint.y = 0.5f; 
        return randomPoint;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public bool HasTarget()
    {
        return target != null;
    }

    protected virtual void Eat()
    {
        // Identify potential food sources nearby.
        Collider[] hits = Physics.OverlapSphere(transform.position, 5.0f);
    }

    protected void ConsumeFood(GameObject foodSource, float staminaGain)
    {
        stamina += staminaGain;
        if (stamina > 100)
        {
            stamina = 100;
        }

        // Remove the food source from the environment.
        Destroy(foodSource);
    }

    protected virtual void Die()
    {
        CountManager.Instance.DecrementCount(gameObject.tag);
        Destroy(gameObject);
    }

    protected void DecreaseVitality(float amount)
    {
        vitality -= amount;
        if (vitality <= 0)
        {
            Die();
        }
    }

    protected void DecreaseStamina(float amount)
    {
        stamina -= amount;
        if (stamina <= 80)
        {
            Eat();
        }

        if (stamina <= 0)
        {
            stamina = 0;
            Die();
        }
    }



}
