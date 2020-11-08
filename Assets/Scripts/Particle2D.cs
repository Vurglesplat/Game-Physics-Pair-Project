using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    public float mass;
    Vector2D velocity;
    Vector2D acceleration;
    Vector2D accumulatedForces;
    float dampingConstant;
}
