using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    void Update()
    {
        for(int i = 0; i < Integrator.particleList.Count; i++)
        {
            for (int j = i+1; j < Integrator.particleList.Count; j++)
            {
                if (CollisionDetector.CollisionTest(Integrator.particleList[i], Integrator.particleList[j], 1.3f))
                {
                    //make sure to remove in this order so you don't shift j around after deleting i
                    Integrator.removeUnit(Integrator.particleList[j].particleId);
                    Integrator.removeUnit(Integrator.particleList[i].particleId);
                }
            }
        }


    }
}
