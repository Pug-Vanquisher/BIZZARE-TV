using UnityEngine;
using System.Collections.Generic;

public class WeightScale : MonoBehaviour
{
    float forceToMass;

    public float combinedForce;
    public float calculatedMass;

    public int registeredRigidbodies;

    Dictionary<Rigidbody, float> impulsePerRigidBody = new Dictionary<Rigidbody, float>();

    private float currentDeltaTime;
    private float lastDeltaTime;

    [SerializeField]
    private AudioSource newItemOnScalesSound; 

    private void Awake()
    {
        forceToMass = 1f / Physics.gravity.magnitude;
    }

   virtual protected void UpdateWeight()
    {
        registeredRigidbodies = impulsePerRigidBody.Count;
        combinedForce = 0;

        foreach (var force in impulsePerRigidBody.Values)
        {
            combinedForce += force;
        }

        calculatedMass = (float)(combinedForce * forceToMass);
    }

    private void FixedUpdate()
    {
        lastDeltaTime = currentDeltaTime;
        currentDeltaTime = Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (impulsePerRigidBody.ContainsKey(collision.rigidbody))
                impulsePerRigidBody[collision.rigidbody] = Mathf.Abs(collision.impulse.y / lastDeltaTime);
            else
                impulsePerRigidBody.Add(collision.rigidbody, Mathf.Abs(collision.impulse.y / lastDeltaTime));

            UpdateWeight();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (impulsePerRigidBody.Count == 0)
            {
                newItemOnScalesSound.Play();
            }

            if (impulsePerRigidBody.ContainsKey(collision.rigidbody))
                impulsePerRigidBody[collision.rigidbody] = Mathf.Abs(collision.impulse.y / lastDeltaTime);
            else
                impulsePerRigidBody.Add(collision.rigidbody, Mathf.Abs(collision.impulse.y / lastDeltaTime));

            UpdateWeight();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            impulsePerRigidBody.Remove(collision.rigidbody);
            UpdateWeight();
        }
    }
}