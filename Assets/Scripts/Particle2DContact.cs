using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
    Particle2D particle1;
    Particle2D particle2;

    public float restitutionCoefficient;

    vector2D vector2D;
    float penetration;
    vector2D move;
}
