using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public enum rounds
    {
        None,
        P1Build
        ,P2Build
        ,P1Move
        ,P2Move
        ,P1Shoot
        ,P2Shoot
    }

    public rounds CurrentRound;

    float MoveRoundTimer;
    public float MoveRoundInitialTime;
    int MoveRoundTimeSec;

    public CameraBoundaries camBounds;

    [SerializeField]
    TMP_Text Timer;

    [Header("Round P1build")]
    [SerializeField]
    private List<GameObject> P1build = new();
    [SerializeField]
    private GameObject CamPointP1B;

    [Header("Round P2build")]
    [SerializeField]
    private List<GameObject> P2build = new();
    [SerializeField]
    private GameObject CamPointP2B;

    [Header("Round P1Move")]
    [SerializeField]
    MovementController P1Movement;
    [SerializeField]
    PlayerController P1Player;
    [SerializeField]
    PlayerController P1jump;
    [SerializeField]
    private GameObject CamPointP1M;

    [Header("Round P2Move")]
    [SerializeField]
    MovementController P2Movement;
    [SerializeField]
    PlayerController P2Player;
    [SerializeField]
    PlayerController P2Pjump;
    [SerializeField]
    private GameObject CamPointP2M;

    [Header("Round P1Shoot")]
    [SerializeField]
    BallSpawner P1Shoot;
    [SerializeField]
    private GameObject CamPointP1S;

    [Header("Round P2Shoot")]
    [SerializeField]
    BallSpawner P2Shoot;
    [SerializeField]
    private GameObject CamPointP2S;

    private void Start()
    {
        ChangeRound();
        MoveRoundTimer = MoveRoundInitialTime;
    }

    public void ChangeRound()
    {
        CurrentRound++;
        switch (CurrentRound)
        {
            case rounds.P1Build:
                foreach (GameObject s in P1build)
                {
                    s.active = true;
                }
                camBounds.TargetObject = CamPointP1B;
                break;

            case rounds.P2Build:
                foreach (GameObject s in P1build)
                {
                    s.active = false;
                }
                camBounds.TargetObject = CamPointP2B;
                foreach (GameObject s in P2build)
                {
                    s.active = true;
                }
                break;
            case rounds.P1Move:
                foreach (GameObject s in P2build)
                {
                    s.active = false;
                }
                camBounds.TargetObject = CamPointP1M;
                P1Movement.enabled = true;
                P1Player.enabled = true;
                P1jump.enabled = true;
                break;
            case rounds.P2Move:
                P1Movement.enabled = false;
                P1jump.enabled = false;
                P1Player.enabled = false;
                P2Movement.enabled = true;
                P2Player.enabled = true;
                P2Pjump.enabled = true;
                camBounds.TargetObject = CamPointP2M;
                break;
            case rounds.P1Shoot:
                P2Movement.enabled = false;
                P2Player.enabled = false;
                P2Pjump.enabled = false;
                P1Shoot.SpawnBall();
                camBounds.TargetObject = CamPointP1S;
                break;
            case rounds.P2Shoot:
                P2Shoot.SpawnBall();
                camBounds.TargetObject = CamPointP1S;
                break;
        }
    }

    private void Update()
    {
        if (CurrentRound == rounds.P1Move)
        {
            TimerStart();
        }
        if (CurrentRound == rounds.P2Move)
        {
            TimerStart();
        }
    }

    private void TimerStart()
    {
        MoveRoundTimer -= Time.deltaTime;
        MoveRoundTimeSec = Mathf.FloorToInt(MoveRoundTimer % 60);
        Timer.text = MoveRoundTimeSec.ToString();
        if (MoveRoundTimer <= 0)
        {
            MoveRoundTimer = MoveRoundInitialTime;
            ChangeRound();
        }
    }
}
