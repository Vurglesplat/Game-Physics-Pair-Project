using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    TextMeshProUGUI scoreDisplay = null;
    public static int score = 0;

    void Start()
    {
        scoreDisplay = this.gameObject.GetComponent<TextMeshProUGUI>();

        if (!scoreDisplay)
            Debug.LogError("no tmp found for score");
    }

    void Update()
    {
        scoreDisplay.text = "Score: " + score;
    }
}
