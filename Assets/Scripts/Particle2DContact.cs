using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
    Particle2D particle1;
    Particle2D particle2;

    public float restitutionCoefficient;

    Vector2 vector2D;
    float penetration;
    Vector2 move;
}
