using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public static bool CollisionTest(Particle2D first, Particle2D second, float dist)
    {
        if (Vector3.Distance(first.transform.position, second.transform.position) < dist)
        {
            return true;
        }

        return false;
    }
}
