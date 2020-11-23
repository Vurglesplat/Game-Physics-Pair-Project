using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ForceManager
{
    static List<ForceGenerator2D> forceGenList = new List<ForceGenerator2D> { };

    public static void AddForceGenerator(ForceGenerator2D newForceGen)
    {
        forceGenList.Add(newForceGen);
    }

    public static void DeleteForceGenerator(ForceGenerator2D forceGenToDelete)
    {

    }

    public static void ApplyForces(Particle2D currentPart, double dt)
    {
        foreach(ForceGenerator2D currentForceGen in forceGenList)
        {
            currentForceGen.UpdateForce(currentPart, dt);
        }
    }
}
