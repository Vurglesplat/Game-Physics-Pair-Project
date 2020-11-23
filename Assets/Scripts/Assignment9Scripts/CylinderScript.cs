using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderScript : MonoBehaviour
{
    [SerializeField] float upperLimitForYVel;
    [SerializeField] float lowerLimitForYVel;
    [SerializeField] float upperLimitForXVel;
    [SerializeField] float lowerLimitForXVel;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Particle2D>().velocity = 
            new Vector2(Random.Range(lowerLimitForXVel, upperLimitForXVel), Random.Range(lowerLimitForYVel, upperLimitForYVel)) ;
    }

}
