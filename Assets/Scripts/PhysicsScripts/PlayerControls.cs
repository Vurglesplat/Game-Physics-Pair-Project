using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] GameObject bulletOnePrefab = null;
    [SerializeField] float bulletOneForce = 10.0f;
    [SerializeField] GameObject bulletTwoPrefab = null;
    [SerializeField] float bulletTwoForce = 5.0f;
    [SerializeField] GameObject bulletThreePrefab = null;
    [SerializeField] float bulletThreeForce = 5.0f;

    enum WEAPON_TYPES
    {
        PISTOL_SHOT,
        SPRING_SHOT,
        ROD_SHOT
    };

    WEAPON_TYPES currentWeaponType = WEAPON_TYPES.PISTOL_SHOT;

    [Range(1f, 10f)] [SerializeField] float RotationSpeed;
    
    readonly KeyCode rotateCounterClockwise = KeyCode.Alpha1;
    readonly KeyCode rotateClockwise = KeyCode.Alpha2;
    readonly KeyCode changeWeapon = KeyCode.W;
    readonly KeyCode fireWeapon = KeyCode.Return; //the primary enter key on most keyboards

    private void FixedUpdate()
    {
        ;
    }

    private void Update()
    {
        if (Input.GetKey(rotateCounterClockwise))
        {
            // rotate counter-clockwise
            RotatePlayer(RotationSpeed * 0.1f);
        }
        else if (Input.GetKey(rotateClockwise))
        {
            // rotate clockwise
            RotatePlayer(RotationSpeed * -0.1f);
        }

        if (Input.GetKeyDown(changeWeapon))
        {
            ChangeWeapon();
        }

        if (Input.GetKeyDown(fireWeapon))
        {
            FireWeapon();
        }
    }

    void RotatePlayer(float rot)
    {
        Transform arrow = this.gameObject.transform;

        arrow.Rotate(0.0f, 0.0f, rot, Space.Self);
    }

    void ChangeWeapon()
    {
        if (currentWeaponType == WEAPON_TYPES.ROD_SHOT)
            currentWeaponType = WEAPON_TYPES.PISTOL_SHOT;
        else
            ++currentWeaponType;
    }

    void FireWeapon()
    {
        switch(currentWeaponType)
        {
            case WEAPON_TYPES.PISTOL_SHOT:
                {
                    Transform currentTransform = this.gameObject.transform;
                    GameObject theBullet = Instantiate(bulletOnePrefab, currentTransform.position, bulletOnePrefab.transform.rotation);
                    theBullet.transform.Rotate(0.0f, currentTransform.eulerAngles.z, 0.0f);

                    Particle2D theBulletsParticle2D = theBullet.GetComponent<Particle2D>();

                    if (!theBulletsParticle2D)
                        Debug.LogError("Didn't get a script from the bullet");

                    theBulletsParticle2D.velocity = bulletOneForce * theBullet.transform.right;
                    break;
                }
            case WEAPON_TYPES.SPRING_SHOT:
                {
                    Transform currentTransform = this.gameObject.transform;
                    GameObject theBullet = Instantiate(bulletTwoPrefab, currentTransform.position, bulletTwoPrefab.transform.rotation);

                    SpringForceGenerator springScript = theBullet.GetComponent<SpringForceGenerator>();
                    if (!springScript)
                        Debug.LogError("Didn't get a spring from the bullet");
                    
                    theBullet.transform.Rotate(0.0f, 0.0f, currentTransform.eulerAngles.z);

                    springScript.part1.velocity = bulletTwoForce * theBullet.transform.right;
                    springScript.part2.velocity = bulletTwoForce * theBullet.transform.right;
                    break;
                }
            case WEAPON_TYPES.ROD_SHOT:
                {
                    Transform currentTransform = this.gameObject.transform;
                    GameObject theBullet = Instantiate(bulletThreePrefab, currentTransform.position, bulletTwoPrefab.transform.rotation);

                    Particle2DLink linkScript = theBullet.GetComponent<Particle2DLink>();
                    if (!linkScript)
                        Debug.LogError("Didn't get a rod link from the bullet");

                    theBullet.transform.Rotate(0.0f, 0.0f, currentTransform.eulerAngles.z);

                    linkScript.particle1.velocity = bulletThreeForce * theBullet.transform.right;
                    linkScript.particle2.velocity = bulletThreeForce * theBullet.transform.right;
                    break;
                }
        }

   

    }
}
