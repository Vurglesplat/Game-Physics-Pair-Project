using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCylinderSpawner : MonoBehaviour
{

    [SerializeField] [Range(0.1f, 2.0f)] float delayBetweenSpawns;

    [SerializeField] GameObject circlePrefab;
    void Start()
    {
        StartCoroutine(MoveTank());

    }
    IEnumerator MoveTank()
    {
        Instantiate(circlePrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-4.0f, 6.0f), this.gameObject.transform.position.z), circlePrefab.transform.rotation);

        yield return new WaitForSeconds(delayBetweenSpawns);
        StartCoroutine(MoveTank());
    }
}