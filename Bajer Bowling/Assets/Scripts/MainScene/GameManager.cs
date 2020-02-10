using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController cam = default;
    [SerializeField] GameObject arena = default;

    /* USE THESE VARIABLES WHEN TESTING ONLY GAME SCENE */
    [SerializeField] enum PlayerType { AI, P1, P2, P3, P4 };
    [Header("Team 1")]
    [SerializeField] PlayerType P1 = default;
    [SerializeField] PlayerType P2 = default;
    [Header("Team 2")]
    [SerializeField] PlayerType P3 = default;
    [SerializeField] PlayerType P4 = default;
    /* */


    [SerializeField] GameObject turnIndicator = default;
    [SerializeField] GameObject ScoreBoard = default;
    [SerializeField] int maxScore = default;
    [Header("Only for development")]
    [SerializeField] string sceneToReset = default;

    private string[] playerTypes;
    private ArenaSetup arenaSetup;
    private GameObject[] players;
    private GameObject ball;
    private ScoreManager scoreManager;
    private int playerTurn = 1;

    void Start()
    {
        arenaSetup = arena.GetComponent<ArenaSetup>();
        scoreManager = ScoreBoard.GetComponent<ScoreManager>();
        turnIndicator = Instantiate(turnIndicator);
        playerTypes = PlayerInformation.GetPlayerTypes();
        SetupGame();
        StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerTurn++;
            if (playerTurn > 4)
            {
                playerTurn = 1;
            }
            StartRound(playerTurn);
        }
    }

    // sets up the arena with the selected playertypes
    void SetupGame()
    {
        /* USE THESE VARIABLES WHEN TESTING ONLY GAME SCENE */
        string p1 = P1.ToString();
        string p2 = P2.ToString();
        string p3 = P3.ToString();
        string p4 = P4.ToString();
        playerTypes = new string[] { p1, p2, p3, p4 };
        /* */

        arenaSetup.SetupArena(arena, playerTypes);

        GameObject[] teams = arenaSetup.GetTeams();
        for (int i = 0; i < teams.Length; i++)
        {
            scoreManager.SetTeamName((i + 1), teams[i].GetComponent<Team>().GetTeamName());
            scoreManager.SetTeamScore((i + 1), 0);
        }
    }

    // starts the game by getting the instantiated players and the ball
    void StartGame()
    {
        this.players = arenaSetup.GetPlayers();
        this.ball = arenaSetup.GetBall();

        StartRound(0);
        // StartCoroutine("Test");
    }

    // start the round of the player specified by the parameter, and assign the ball to that player
    // @params: the player which turn it is 
    void StartRound(int playerTurn)
    {
        int nextPlayer = 0;
        switch (playerTurn)
        {
            case 1:
                nextPlayer = 3;
                break;
            case 3:
                nextPlayer = 2;
                break;
            case 2:
                nextPlayer = 4;
                break;
            case 4:
                nextPlayer = 1;
                break;
            default:
                nextPlayer = 1;
                break;
        }

        Player p = this.players[nextPlayer - 1].GetComponent<Player>();
        GameObject team = p.GetTeam();
        // cam.PositionCamera(team); // is used to position the camera behind the given team
        cam.MoveCamera(team);
        p.GiveBall(ball, turnIndicator);
    }

    // ends the current round by checking if the team scored and checks if the game is over
    // otherwise, resets and starts a new round 
    // @params: the current player, if the player scored 
    public void EndRound(GameObject currentPlayer, bool score)
    {
        Player player = currentPlayer.GetComponent<Player>();
        Team team = player.GetTeam().GetComponent<Team>();

        if (score) scoreManager.IncrementTeamScore(team.GetTeamNumber());

        if (scoreManager.GetTeamScore(team.GetTeamNumber()) >= maxScore)
        {
            scoreManager.SetWinText(team);
            StartCoroutine("ResetGame");
        }
        else
        {
            StartRound(player.GetPlayerNumber());
        }
    }

    public IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(sceneToReset);
    }

    public GameObject[] GetPlayers()
    {
        return players;
    }
}
