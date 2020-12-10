using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Range (0.0f, 1.0f)] [SerializeField] float timeScale = 0.0f;


    // Update is called once per frame
    void Update()
    {
        Integrator.Integrate(Time.deltaTime);
        Time.timeScale = timeScale;
    }
}
