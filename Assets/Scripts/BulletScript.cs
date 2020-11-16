﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    GameObject theTarget = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!theTarget)
        {
            foreach (Particle2D otherPart in Integrator.particleList)
            {
                if (otherPart.gameObject.CompareTag("Target"))
                {
                    theTarget = otherPart.gameObject;
                }
            }
        }
        else if (Vector3.Distance(this.transform.position, theTarget.transform.position) < 1.0f)
        {
            TargetScript theTargetScript = theTarget.GetComponent<TargetScript>();

            if (!theTargetScript)
                Debug.LogError("Error: Bullet hit something tagged target that wasn't one");


            theTargetScript.HandleTargetHit();
        }
    }



}
