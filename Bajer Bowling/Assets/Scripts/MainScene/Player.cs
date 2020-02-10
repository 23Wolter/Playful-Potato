using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float throwForce = default;
    [SerializeField] float inFrontOfChair = default;
    private string playerType;
    private int playerNumber;
    private GameObject team;
    private Team t;
    private Transform chair;
    private GameObject ball;
    private float ballOffset = 1f;
    private float ballDirection;
    private float indicatorOffset = 1f;
    private AIMovement ai;
    private bool hasShot = false;
    private bool canStand = false;
    private PlayerMovement playerMovement;

    void Throw()
    {
        if (HasBall() && !hasShot)
        {
            hasShot = true;
            ball.GetComponent<BallMovement>().ThrowBall(throwForce, ballDirection);
        }
    }

    void Start()
    {
        if (playerType == "AI") ai = gameObject.AddComponent<AIMovement>();

        if (t.GetTeamNumber() == 1) ballDirection = 1;
        else if (t.GetTeamNumber() == 2)
        {
            ballDirection = -1;
            inFrontOfChair *= -1;
        }

        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // if playertype is "AI" - the AI controls shooting (proper AI needs to be implemented)
        if (playerType == "AI")
        {
            if (HasBall() && !hasShot)
            {
                hasShot = true;
                StartCoroutine(ai.ShootBall(ball, ballDirection, throwForce));
            }
        }
        else
        {
            // CODE TO DO...
            // Player standing functionality is very messy, needs clean up 
            // Code needs a check if all players are sitting, before next player can shoot 
            // This could be implemented as a game rule, for instance the referee tackles players after some time 

            // if player 1 presses 'space' or 'A'
            if (playerType == "P1")
            {
                if (Input.GetButtonDown("Stand_1"))
                {
                    if (playerMovement.IsStanding())
                    {
                        if (Vector3.Distance(transform.position, GetChair().position) < 2)
                        {
                            playerMovement.Sit();
                        }
                    }
                    else
                    {
                        playerMovement.Stand();
                    }
                }
                else
                {
                    if (!playerMovement.IsStanding())
                    {
                        if (Input.GetButtonDown("Throw_1") || Input.GetButtonDown("P1_Throw"))
                        {
                            Throw();
                        }
                    }
                }
            }

            // if player 2 presses 'e' or 'A'
            if (playerType == "P2")
            {
                if (Input.GetButtonDown("Stand_2"))
                {
                    if (playerMovement.IsStanding())
                    {
                        if (Vector3.Distance(transform.position, GetChair().position) < 2)
                        {
                            playerMovement.Sit();
                        }
                    }
                    else
                    {
                        playerMovement.Stand();
                    }
                }
                else
                {
                    if (!playerMovement.IsStanding())
                    {
                        if (Input.GetButtonDown("Throw_2") || Input.GetButtonDown("P2_Throw"))
                        {
                            Throw();
                        }
                    }
                }
            }

            // if player 3 presses 'A'
            if ((Input.GetButtonDown("P3_Throw")) && playerType == "P3")
            {
                Throw();
            }

            // if player 4 presses 'A'
            if ((Input.GetButtonDown("P4_Throw")) && playerType == "P4")
            {
                Throw();
            }
        }
    }

    public string GetPlayerType()
    {
        return playerType;
    }

    public void SetPlayerType(string playerType)
    {
        this.playerType = playerType;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public void SetPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;
    }

    public GameObject GetTeam()
    {
        return team;
    }

    public void SetTeam(GameObject team)
    {
        this.team = team;
        this.t = team.GetComponent<Team>();
    }

    public GameObject GetBall()
    {
        return ball;
    }

    public void RemoveBall()
    {
        ball = null;
    }

    public void GiveBall(GameObject ball, GameObject turnIndicator)
    {
        this.ball = ball;
        ball.GetComponent<BallMovement>().SetPlayer(gameObject);
        ball.GetComponent<BallMovement>().StopBall();
        PositionBall();
        PositionIndicator(turnIndicator);
        hasShot = false;
    }

    // @returns: true if player has the ball
    public bool HasBall()
    {
        return ball;
    }

    // positions the ball according to which team the player is at 
    private void PositionBall()
    {
        float offset = ballOffset;
        // always position the ball in front of the goal 
        if (t.GetTeamNumber() == 2)
        {
            offset *= -1;
        }
        ball.transform.position = new Vector3(team.transform.position.x + offset, team.transform.position.y, team.transform.position.z);
    }

    private void PositionIndicator(GameObject indicator)
    {
        indicator.transform.position = new Vector3(transform.position.x, transform.position.y + indicatorOffset, transform.position.z);
        indicator.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        indicator.GetComponentInChildren<Text>().text = playerType;
    }

    public void SetChair(GameObject chair)
    {
        Transform c = chair.transform;
        this.chair = c;
    }

    public Transform GetChair()
    {
        return chair;
    }

    public void EndTurn()
    {
        RemoveBall();
        Goal goal = t.GetGoal().GetComponent<Goal>();

        bool score = false;

        if (goal.IsHit())
        {
            score = true;
            goal.ResetGoal();
        }

        GameObject.Find("GameManager").GetComponent<GameManager>().EndRound(gameObject, score);
    }

    private void SetCanStand(bool stand)
    {
        canStand = stand;
    }

    private bool GetCanStand()
    {
        return canStand;
    }

    override
    public string ToString()
    {
        return "Player " + playerNumber + " is " + playerType + " and has ball " + HasBall();
    }
}
