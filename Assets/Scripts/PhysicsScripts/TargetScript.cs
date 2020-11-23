 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField] GameObject targetPrefab;
    bool isDead = false;

    public void HandleTargetHit()
    {
        if(!isDead)
        {
            isDead = true;
            ++ScoreScript.score;
            Instantiate(targetPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 6.0f), this.gameObject.transform.position.z), Quaternion.identity);

            Integrator.removeUnit(this.gameObject.GetComponent<Particle2D>().partId);
        }
    }
}
