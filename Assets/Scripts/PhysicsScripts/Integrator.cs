using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Integrator
{
    public static List<Particle2D> particleList = new List<Particle2D> { };
    public static List<Particle2DContact> contactList = new List<Particle2DContact> { };
    public static List<SquareCollider> colliderList = new List<SquareCollider> { };
    public static List<Particle2DLink> particleLinkList = new List<Particle2DLink> { };
    const int NUM_OF_ITERATIONS = 10;

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

        foreach (SquareCollider currentCollider in colliderList)
        {
            currentCollider.CheckForCollision(ref colliderList);
        }

        ResolveContacts(dt);
        contactList.Clear();
    }

    public static void AddToList(Particle2D newPart)
    {
        particleList.Add(newPart);
    }

    public static void RemoveUnit(int unitIdToBeDeleted)
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
    static void ResolveContacts(double dt)
    {
        int mIterationsUsed = 0;
        while (mIterationsUsed < NUM_OF_ITERATIONS)
        {
            float max = 9999999.9f;
            int numContacts = contactList.Count;
            int maxIndex = numContacts;
            for (int i = 0; i < numContacts; i++)
            {
                float sepVel = contactList[i].CalculateSeparatingVelocity();
                if (sepVel < max && (sepVel < 0.0f || contactList[i].penetration > 0.0f))
                {
                    max = sepVel;
                    maxIndex = i;
                }
            }
            if (maxIndex == numContacts)
                break;

            contactList[maxIndex].Resolve(dt);

            for (int i = 0; i < numContacts; i++)
            {
                if (contactList[i].particle1 == contactList[maxIndex].particle1)
                {
                    contactList[i].penetration -= Vector2.Dot( contactList[maxIndex].move1, contactList[i].contactNorm);
                }
                else if (contactList[i].particle1 == contactList[maxIndex].particle2)
                {
                    contactList[i].penetration -= Vector2.Dot(contactList[maxIndex].move2, contactList[i].contactNorm);
                }

                if (contactList[i].particle2)
                {
                    if (contactList[i].particle2 == contactList[maxIndex].particle1)
                    {
                        contactList[i].penetration += Vector2.Dot(contactList[maxIndex].move1, contactList[i].contactNorm);
                    }
                    else if (contactList[i].particle2 == contactList[maxIndex].particle2)
                    {
                        contactList[i].penetration -= Vector2.Dot(contactList[maxIndex].move2, contactList[i].contactNorm);
                    }
                }
            }
            mIterationsUsed++;
        }
    }
}
