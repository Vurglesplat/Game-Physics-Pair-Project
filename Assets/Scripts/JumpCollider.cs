using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollider : MonoBehaviour
{

    public float jumpHeightCheckRange;
    public SquareCollider thisCollider;
    public SquareCollider jumpBase;

    // Update is called once per frame
    void Awake()
    {
        Integrator.jumpCollider = this;
    }

    public void CheckForGround(ref List<SquareCollider> colliderList)
    {
        foreach (SquareCollider other in colliderList)
        {
            if (thisCollider.collisionRight > other.collisionLeft &&
                thisCollider.collisionLeft < other.collisionRight &&
                thisCollider.collisionBottom - jumpHeightCheckRange < other.collisionTop &&
                other != thisCollider)
            {
                jumpBase = other;
            }
        }

        }
}
