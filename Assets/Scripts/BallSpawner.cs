using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;
    
    public GameObject CurrentBall;


    public void SpawnBall()
    {
        CurrentBall = Instantiate(ball, transform
            );

    }
}
