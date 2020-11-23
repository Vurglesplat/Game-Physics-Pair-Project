using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator2D : MonoBehaviour
{
    //Update the forces
    virtual public void UpdateForce(Particle2D theObject, double dt)
    {
        Debug.LogError("Error: Base implementation of ForceGenerator's Update Force used instead of a derived classes");
    }
}
