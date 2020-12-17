using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyancyForceGenerator : ForceGenerator2D
{
    [SerializeField] float waterHeight;
    [SerializeField] float waterResistance;
    [SerializeField] float widthMultiplier = 1.0f;
    [Range(0.95f, 1f)] [SerializeField] float waterDampening;


    // Start is called before the first frame update
    void Start()
    {
        ForceManager.AddForceGenerator(this);
    }

    override public void UpdateForce(Particle2D theObject, double dt)
    {
        if (!theObject)
            Debug.LogError("Tried to update force on an object that doesn't exit");

        if (theObject.transform.position.y < waterHeight) // the object is underwater
        {
            float diff = theObject.transform.position.y - waterHeight ;


            float magnitude = diff * waterResistance * theObject.inverseMass * Mathf.Pow(theObject.squareCollider.width, widthMultiplier);
            diff *= magnitude;

            theObject.accumulatedForces += new Vector2(0.0f, diff);
            theObject.velocity *= waterDampening;

            if(theObject.gameObject.CompareTag("Player"))
            {
                theObject.GetComponent<PlatformPlayerControls>().isUnderwater = true;
            }
        }
    }

}
