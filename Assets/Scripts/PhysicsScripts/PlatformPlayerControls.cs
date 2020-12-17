using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerControls : MonoBehaviour
{
    [SerializeField] Particle2D playerParticle;
    [SerializeField] float moveSpeed;
    [SerializeField] float swimmingUpStrength;
    [SerializeField] float jumpStrength;
    [SerializeField] JumpCollider jumpCollider;
    public bool isUnderwater = false;

    // Start is called before the first frame update
    void Start()
    {
        playerParticle = this.gameObject.GetComponent<Particle2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerParticle.accumulatedForces += new Vector2(moveSpeed, 0.0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerParticle.accumulatedForces += new Vector2(-moveSpeed, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (jumpCollider.jumpBase)
            {
                playerParticle.accumulatedForces += new Vector2(0.0f, jumpStrength);
                jumpCollider.jumpBase.particle.accumulatedForces += new Vector2(0.0f, -jumpStrength);

                //Particle2DContact newContact = new Particle2DContact { };
                //newContact.setNewVars(this.playerParticle, jumpCollider.jumpBase.particle, 0.9f, new Vector2(0.0f, 1.0f), jumpStrength, new Vector2(0f, 0f), new Vector2(0f, 0f));
                //Integrator.contactList.Add(newContact);
            }
            else if (isUnderwater)
            {
                playerParticle.accumulatedForces += new Vector2(0.0f, swimmingUpStrength);

            }
            
        }

        isUnderwater = false;
    }
}
