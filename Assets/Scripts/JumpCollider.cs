using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollider : MonoBehaviour
{

    public float jumpHeightCheckRange;
    public float widthCheckReduction;
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
            if (other != thisCollider && // don't want to be able to jump off of ourselves
                thisCollider.collisionRight - widthCheckReduction > other.collisionLeft &&
                thisCollider.collisionLeft + widthCheckReduction < other.collisionRight &&
                thisCollider.collisionBottom - jumpHeightCheckRange < other.collisionTop)
            {
                jumpBase = other;
            }
        }

        }
}
