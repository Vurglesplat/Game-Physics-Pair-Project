using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    int score = 0;
    delegate void CustomEventDelegate(int x);

    public void HandleTargetHit()
    {
        Debug.Log("Score: " + ++score);

        this.gameObject.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 6.0f), this.gameObject.transform.position.z);
    }
}
