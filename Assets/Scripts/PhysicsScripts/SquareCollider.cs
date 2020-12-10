using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCollider : MonoBehaviour
{
    Particle2D thisParticle;
    [SerializeField] float height = 0.0f;
    [SerializeField] float width = 0.0f;

    [HideInInspector] public float collisionZoneHeightUpperBound = 0.0f;
    [HideInInspector] public float collisionZoneHeightLowerBound = 0.0f;
    [HideInInspector] public float collisionZoneWidthUpperBound = 0.0f;
    [HideInInspector] public float collisionZoneWidthLowerBound = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        thisParticle = this.gameObject.GetComponent<Particle2D>();
        Integrator.colliderList.Add(this);
    }

    void FixedUpdate()
    {
        collisionZoneHeightUpperBound = this.transform.position.y + (height / 2.0f) ;
        collisionZoneHeightLowerBound = this.transform.position.y - (height / 2.0f) ;
        
        collisionZoneWidthUpperBound = this.transform.position.x + (width / 2.0f) ;
        collisionZoneWidthLowerBound = this.transform.position.x - (width / 2.0f) ;
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
                colDirection dir = colDirection.NONE;
                bool horiTest = false;
                bool vertTest = false;
                float length = 0.0f;

                if (other.transform.position.y > this.transform.position.y
                    && other.collisionZoneHeightLowerBound < collisionZoneHeightUpperBound) //collision with other above
                {
                    //Debug.Log("" + other.collisionZoneHeightLowerBound + '<' + collisionZoneHeightUpperBound);
                    length += collisionZoneHeightUpperBound - other.collisionZoneHeightLowerBound;
                    dir = colDirection.OTHER_ABOVE;
                    vertTest = true;
                }
                //else if (other.transform.position.y < this.transform.position.y
                //    && other.collisionZoneHeightUpperBound > collisionZoneHeightLowerBound) // collision with other below
                //{
                //    dir = colDirection.OTHER_BELOW;
                //    length += other.collisionZoneHeightUpperBound - collisionZoneHeightLowerBound;
                //    vertTest = true;
                //}

                if (other.transform.position.x < this.transform.position.x
                    && other.collisionZoneWidthUpperBound > collisionZoneWidthLowerBound) // collision with other left
                {
                    //dir = colDirection.OTHER_TO_THE_LEFT;
                    //length += other.collisionZoneWidthUpperBound - collisionZoneWidthLowerBound;
                    horiTest = true;
                }
                else if (other.transform.position.x > this.transform.position.x
                    && other.collisionZoneWidthLowerBound < collisionZoneWidthUpperBound) // collision with other right
                {
                    //dir = colDirection.OTHER_TO_THE_RIGHT;
                    //length += collisionZoneWidthUpperBound - other.collisionZoneWidthLowerBound;
                    horiTest = true;
                }

                if (dir != colDirection.NONE && horiTest && vertTest)
                {
                    //float length = Vector2.Distance(this.transform.position, other.transform.position);
                    //Debug.Log("length = " + length);
                    Debug.Log("contact between " + this.gameObject.name + " and " + other.gameObject.name);

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
                    newContact.setNewVars(this.thisParticle, null, 0.1f, normal, length, new Vector2(0f, 0f), new Vector2(0f, 0f));
                    Integrator.contactList.Add(newContact);
                }
            }
        }
    }
}
