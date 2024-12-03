using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    RoundManager roundManager;

    List<bool> PowerUpList = new List<bool>();

    [Header("Barrierit")]
    [SerializeField]
    private GameObject UpperBarrier;
    [SerializeField]
    private GameObject LowerBarrier;
    public bool UpperBarrierEnabled = false;
    public bool LowerBarrierEnabled = false;

    [SerializeField]
    private BallSpawner BallSpawner;
    [SerializeField]
    private GameObject Ball;
    [Header("PowerUpPreFabs")]
    [SerializeField]
    private GameObject Bomb;

    private void Update()
    {
        UpperBarrier.SetActive(UpperBarrierEnabled);
        LowerBarrier.SetActive(LowerBarrierEnabled);
    }

    public void DisablePowerUps()
    {
        UpperBarrierEnabled = false;
        LowerBarrierEnabled = false;
        BallSpawner.ball = Ball;
        gameObject.transform.localScale = new Vector3(1,1,1);
    }

    public void SetPowerUpOn(string powerUp)
    {
        switch (powerUp)
        {
            case "LowerBarrier":
                LowerBarrierEnabled=true;
                break;
            case "UpperBarrier":
                UpperBarrierEnabled=true;
                break;
            case "Bomb":
                BallSpawner.ball = Bomb;
                break;
        }
    }
}
