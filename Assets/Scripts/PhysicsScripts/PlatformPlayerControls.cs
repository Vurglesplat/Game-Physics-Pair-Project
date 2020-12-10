using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerControls : MonoBehaviour
{
    [SerializeField] Particle2D playerParticle;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerParticle.accumulatedForces += new Vector2(0.0f, jumpHeight);
        }
    }
}
