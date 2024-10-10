using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject ball;
    
    private GameObject CurrentBall;

    // Update is called once per frame
    void Update()
    {
        if (CurrentBall == null)
        {
            CurrentBall = Instantiate(ball);
        }
    }
}
