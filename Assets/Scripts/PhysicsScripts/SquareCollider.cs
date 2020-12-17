using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCollider : MonoBehaviour
{
    [SerializeField] float height = 0.0f;
    public  float width = 0.0f;
    
    [HideInInspector] public Particle2D particle;
    [HideInInspector] public float collisionTop = 0.0f;
    [HideInInspector] public float collisionBottom = 0.0f;
    [HideInInspector] public float collisionRight = 0.0f;
    [HideInInspector] public float collisionLeft = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        particle = this.gameObject.GetComponent<Particle2D>();
        Integrator.colliderList.Add(this);
    }

    void FixedUpdate()
    {
        collisionTop = this.transform.position.y + (height / 2.0f) ;
        collisionBottom = this.transform.position.y - (height / 2.0f) ;
        
        collisionRight = this.transform.position.x + (width / 2.0f) ;
        collisionLeft = this.transform.position.x - (width / 2.0f) ;
    }

    enum colDirection
    {
        OTHER_ABOVE,
        OTHER_BELOW,
        OTHER_TO_THE_RIGHT,
        OTHER_TO_THE_LEFT,
        NONE
    }

    public void CheckForCollision(ref List<SquareCollider> colliderList)
    {
        foreach (SquareCollider other in colliderList)
        {
            if (this.gameObject != other.gameObject)
            {
                float length = 0.0f;

                float topPen = this.collisionTop - other.collisionBottom;
                float botPen = other.collisionTop - this.collisionBottom;
                float leftPen = other.collisionRight - this.collisionLeft;
                float rightPen = this.collisionRight - other.collisionLeft;

                //if (dir != colDirection.NONE && horiTest && vertTest)
                if ( topPen > 0.0f &&
                     botPen > 0.0f &&
                     leftPen > 0.0f &&
                     rightPen > 0.0f &&
                     !particle.particlesInContactWith.Contains(other.particle.particleId))
                {
                    //float length = Vector2.Distance(this.transform.position, other.transform.position);
                    //Debug.Log("length = " + length);
                    
                    colDirection dir = colDirection.OTHER_ABOVE;
                    length = topPen;

                    if (botPen < topPen)
                    {
                        dir = colDirection.OTHER_BELOW;
                        length = botPen;
                    }
                    if (leftPen < length)
                    {
                        dir = colDirection.OTHER_TO_THE_LEFT;
                        length = leftPen;
                    }
                    if (rightPen < length)
                    {
                        dir = colDirection.OTHER_TO_THE_RIGHT;
                        length = rightPen;
                    }
                    

                    if (this.gameObject.CompareTag("Player"))
                    {
                        if (topPen < botPen && topPen < leftPen && topPen < rightPen)
                            Debug.Log("Collision from above");
                        else if (topPen > botPen && botPen < leftPen && botPen < rightPen)
                            Debug.Log("Collision from below");
                        else if (rightPen < botPen && rightPen < leftPen && topPen > rightPen)
                            Debug.Log("Collision from Right");
                        else
                            Debug.Log("Collision from left");
                    }

                    //if (Mathf.Abs(theDiff.x) > Mathf.Abs(theDiff.y))
                    //{
                    //    Debug.Log("Collision Vertical because " + Mathf.Abs(theDiff.x) + '>' + Mathf.Abs(theDiff.y));
                    //}
                    //else
                    //    Debug.Log("Collision Horizontal because " + Mathf.Abs(theDiff.x) + '<' + Mathf.Abs(theDiff.y));

                    Vector2 normal = new Vector2(0.0f,0.0f);
                    switch(dir)
                    {
                        case colDirection.OTHER_ABOVE :
                            {
                                normal = new Vector2(0.0f,-1.0f);
                                break;
                            }
                        case colDirection.OTHER_BELOW :
                            {
                                normal = new Vector2(0.0f,1.0f);
                                break;
                            }
                        case colDirection.OTHER_TO_THE_LEFT :
                            {
                                normal = new Vector2(1.0f,0.0f);
                                break;
                            }
                        case colDirection.OTHER_TO_THE_RIGHT :
                            {
                                normal = new Vector2(-1.0f,0.0f);
                                break;
                            }
                    }

                    Particle2DContact newContact = new Particle2DContact { };
                    newContact.setNewVars(this.particle, other.particle, 0.9f, normal, length, new Vector2(0f, 0f), new Vector2(0f, 0f));
                    Integrator.contactList.Add(newContact);

                    this.particle.particlesInContactWith.Add(other.particle.particleId);
                    other.particle.particlesInContactWith.Add(this.particle.particleId);
                }
            }
        }
    }
}
