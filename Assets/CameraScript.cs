using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float followDurationMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.SetTweensCapacity(3150, 50);
    }

    // Update is called once per frame
    void Update()
    {
        transform.DOMoveX(player.transform.position.x, Time.deltaTime * followDurationMultiplier);
        transform.DOMoveY(player.transform.position.y, Time.deltaTime * followDurationMultiplier);
    }
}
