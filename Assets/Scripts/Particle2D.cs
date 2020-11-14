using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Particle2D : MonoBehaviour
{
    [SerializeField] float mass;
    [SerializeField] float gravity;
    [SerializeField] bool shouldIgnoreForces = false;
    
    [Range(0f, 1f)] [SerializeField] float dampingConstant;

    [HideInInspector]  Vector2 velocity = new Vector2( 0.0f, 0.0f);
    [HideInInspector]  Vector2 acceleration = new Vector2(0.0f, 0.0f);
    [HideInInspector]  Vector2 accumulatedForces = new Vector2(0.0f, 0.0f);
    [HideInInspector]  float inverseMass;

    private void Start()
    {
        velocity.y = -gravity;
        Integrator.addToList(this);
        inverseMass = (1.0f / mass);
    }

    public void Integrate(double dt)
    {
        Vector3 pos3 = (velocity * (float)dt);
        this.gameObject.transform.position += pos3;

        Vector2 resultingAcc = acceleration;

        if (!shouldIgnoreForces)//accumulate forces here
        {
            resultingAcc += accumulatedForces * inverseMass;
        }

        velocity += (resultingAcc * (float)dt);
        float damping = (float)Math.Pow(dampingConstant, dt);
        velocity *= damping;

        //clearAccumulatedForces((PhysicsDataPtr)pData);
    }
}
