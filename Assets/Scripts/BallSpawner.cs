using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject ball;
    
    private GameObject CurrentBall;

    public void SpawnBall()
    {
        CurrentBall = Instantiate(ball);
    }
}
