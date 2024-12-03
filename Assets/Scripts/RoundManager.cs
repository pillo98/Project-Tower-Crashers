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
    [SerializeField]
    GameObject timer;

    [SerializeField]
    GameObject P1WinScreen;
    [SerializeField]
    GameObject P2WinScreen;

    [Header("Round P1build")]
    [SerializeField]
    private List<GameObject> P1build = new();
    [SerializeField]
    private GameObject CamPointP1B;
    [SerializeField]
    PowerUpManager powerUpManagerP1;
    [SerializeField]
    private PlayerHealth P1HP;

    [Header("Round P2build")]
    [SerializeField]
    private List<GameObject> P2build = new();
    [SerializeField]
    private GameObject CamPointP2B;
    [SerializeField]
    PowerUpManager powerUpManagerP2;
    [SerializeField]
    private PlayerHealth P2HP;

    [Header("Round P1Move")]
    [SerializeField]
    MovementController P1Movement;
    [SerializeField]
    PowerUpSpawner p1PowerUpSpawner;
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
    PowerUpSpawner p2PowerUpSpawner;
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
    [SerializeField]
    Sound RoundChange;
    [SerializeField]
    AudioSource audioSource;

    private void Start()
    {
        ChangeRound();
        MoveRoundTimer = MoveRoundInitialTime;
        Timer.enabled = false;
    }

    public void ChangeRound()
    {
        CurrentRound++;
        audioSource.PlayOneShot(RoundChange.clip);
        switch (CurrentRound)
        {
            case rounds.P1Build:
                foreach (GameObject s in P1build)
                {
                    s.SetActive(true);
                }
                camBounds.TargetObject = CamPointP1B;
                break;

            case rounds.P2Build:
                foreach (GameObject s in P1build)
                {
                    s.SetActive(false);
                }
                camBounds.TargetObject = CamPointP2B;
                foreach (GameObject s in P2build)
                {
                    s.SetActive(true);
                }
                break;
            case rounds.P1Move:
                powerUpManagerP1.DisablePowerUps();
                powerUpManagerP2.DisablePowerUps();
                Timer.enabled = true;
                timer.SetActive(true);
                foreach (GameObject s in P2build)
                {
                    s.SetActive(false);
                }
                camBounds.TargetObject = CamPointP1M;
                P1Movement.enabled = true;
                P1Player.canMove = true;
                P1jump.enabled = true;
                p1PowerUpSpawner.SpawnItem();
                break;
            case rounds.P2Move:
                P1Movement.enabled = false;
                P1jump.enabled = false;
                P1Player.canMove = false;
                P2Movement.enabled = true;
                P2Player.canMove = true;
                P2Pjump.enabled = true;
                camBounds.TargetObject = CamPointP2M;
                p2PowerUpSpawner.SpawnItem();
                break;
            case rounds.P1Shoot:
                Timer.enabled = false;
                timer.SetActive(false);
                P2Movement.enabled = false;
                P2Player.canMove = false;
                P2Pjump.enabled = false;
                P1Shoot.SpawnBall();
                camBounds.TargetObject = CamPointP1S;
                break;
            case rounds.P2Shoot:
                P2Shoot.SpawnBall();
                camBounds.TargetObject = CamPointP2S;
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
        if (CurrentRound == rounds.P1Shoot)
        {
            if (P1Shoot.CurrentBall == null)
            {
                ChangeRound();
            }
        }
        if (CurrentRound == rounds.P2Shoot)
        {
            if (P2Shoot.CurrentBall == null)
            {
                CurrentRound = rounds.P2Build;
                ChangeRound();
            }
        }
        CheckForWinner();

    }

    private void TimerStart()
    {
        Timer.IsActive();
        MoveRoundTimer -= Time.deltaTime;
        MoveRoundTimeSec = Mathf.FloorToInt(MoveRoundTimer % 60);
        Timer.text = MoveRoundTimeSec.ToString();
        if (MoveRoundTimer <= 0)
        {
            MoveRoundTimer = MoveRoundInitialTime;
            ChangeRound();
        }
    }
    
    void CheckForWinner()
    {
        if (P1HP.health <= 0)
        {
            P2WinScreen.SetActive(true);
            CurrentRound = rounds.None;
            Timer.enabled = false;
            timer.SetActive(false);
            P2Movement.enabled = false;
            P2Player.enabled = false;
            P2Pjump.enabled = false;
            P1Movement.enabled = false;
            P1jump.enabled = false;
            P1Player.enabled = false;
        }
        if (P2HP.health <= 0)
        {
            P1WinScreen.SetActive(true);
            CurrentRound = rounds.None;
            Timer.enabled = false;
            timer.SetActive(false);
            P2Movement.enabled = false;
            P2Player.enabled = false;
            P2Pjump.enabled = false;
            P1Movement.enabled = false;
            P1jump.enabled = false;
            P1Player.enabled = false;
        }


    }
}
