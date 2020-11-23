using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Integrator
{
    public static List<Particle2D> particleList = new List<Particle2D> { };
    public static List<Particle2DContact> contactList = new List<Particle2DContact> { };
    public static List<Particle2DLink> particleLinkList = new List<Particle2DLink> { };
        
    public static void Integrate(double dt)
    {
        foreach (Particle2D currentParticle in particleList)
        {
            currentParticle.Integrate(dt);
            ForceManager.ApplyForces(currentParticle, dt);
        }
        foreach (Particle2DLink currentLink in particleLinkList)
        {
            currentLink.Update();
        }
        foreach (Particle2DContact currentCon in contactList)
        {
            currentCon.Resolve(dt);
        }

    }

    public static void addToList(Particle2D newPart)
    {
        particleList.Add(newPart);
    }

    public static void removeUnit(int unitIdToBeDeleted)
    {
        foreach (Particle2D currentParticle in particleList)
            if(currentParticle.particleId == unitIdToBeDeleted)
            {
                Particle2D temp = currentParticle;
                particleList.Remove(currentParticle);
                temp.DestroySelf();
                break;
            }
    }
}
