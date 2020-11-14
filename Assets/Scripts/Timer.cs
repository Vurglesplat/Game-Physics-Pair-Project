using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Integrator.Integrate(Time.deltaTime);
    }
}
