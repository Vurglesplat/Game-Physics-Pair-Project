using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Integrator
{
    public static List<Particle2D> particleList = new List<Particle2D> { };

    public static void Integrate(double dt)
    {
        foreach (Particle2D currentParticle in particleList)
            currentParticle.Integrate(dt);
    }

    public static void addToList(Particle2D newPart)
    {
        particleList.Add(newPart);
    }
}
