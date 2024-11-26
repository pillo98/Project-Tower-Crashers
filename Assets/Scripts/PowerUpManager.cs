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

    void Start()
    {
        PowerUpList.Add(UpperBarrierEnabled);
        PowerUpList.Add(LowerBarrierEnabled);

    }

    private void Update()
    {
        UpperBarrier.SetActive(UpperBarrierEnabled);
        LowerBarrier.SetActive(LowerBarrierEnabled);
    }

    public void DisablePowerUps()
    {
        for (int i = 0; i >= PowerUpList.Count; i++)
        {
            PowerUpList[i] = false;
        }
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
        }
    }
}
