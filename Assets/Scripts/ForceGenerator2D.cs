using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator2D : MonoBehaviour
{
    //Update the forces
    virtual public void UpdateForce()
    {
        Debug.LogError("Error: Base implementation of ForceGenerator's Update Force used instead of a derived classes");
    }
}
