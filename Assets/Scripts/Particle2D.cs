using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    public float mass;
    [Range(0f, 1f)] [SerializeField] float dampingConstant;

    [HideInInspector] public Vector2 velocity;
    [HideInInspector] public Vector2 acceleration;
    [HideInInspector] public Vector2 accumulatedForces;
}
