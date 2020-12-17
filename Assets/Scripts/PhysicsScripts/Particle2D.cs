using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using DG.Tweening;

public class Particle2D : MonoBehaviour
{
    [SerializeField] float mass;
    [SerializeField] float gravity;
    public bool shouldIgnoreForces = false;
    
    [Range(0f, 1f)] [SerializeField] float dampingConstant;

    public Vector2 velocity = new Vector2( 0.0f, 0.0f);
    public Vector2 acceleration = new Vector2(0.0f, 0.0f);
    public Vector2 accumulatedForces = new Vector2(0.0f, 0.0f);
    [HideInInspector] public float inverseMass;

    [HideInInspector] public int particleId = -1;
    [HideInInspector] public List<int> particlesInContactWith = new List<int>();

    [HideInInspector] public SquareCollider squareCollider;

    static int nextPartId = 0; 

    private void Start()
    {
        particleId = ++nextPartId;
        squareCollider = this.gameObject.GetComponent<SquareCollider>();
        acceleration.y = -gravity;
        Integrator.AddToList(this);
        inverseMass = (1.0f / mass);
    }

    public void Integrate(double dt)
    {
        //Vector3 pos3 = (velocity * (float)dt);
        //transform.position += pos3;

        Vector3 pos3 = (velocity * (float)dt);
        transform.DOMove(transform.position + pos3, 0.1f);

        Vector2 resultingAcc = acceleration;

        if (!shouldIgnoreForces)//accumulate forces here
        {
            resultingAcc += accumulatedForces * inverseMass;
        }

        velocity += (resultingAcc * (float)dt);
        float damping = (float)Math.Pow(dampingConstant, dt);
        velocity *= damping;

        accumulatedForces = new Vector2(0.0f, 0.0f);
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
