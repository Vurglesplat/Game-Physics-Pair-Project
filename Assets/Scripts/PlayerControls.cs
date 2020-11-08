﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

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
        Debug.Log("Changing weapon");
    }

    void FireWeapon()
    {
        Debug.Log("firing weapon");
    }
}