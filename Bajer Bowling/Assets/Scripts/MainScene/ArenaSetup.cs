using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSetup : MonoBehaviour
{
    // different prefabs to be instantiated 
    [SerializeField] GameObject[] playerPrefabs = default;
    [SerializeField] GameObject teamPrefab = default;
    [SerializeField] GameObject[] chairPrefabs = default;
    [SerializeField] GameObject goalPrefab = default;
    [SerializeField] GameObject ballPrefab = default;

    [SerializeField] float arenaLength = default;
    [SerializeField] float chairDistance = default;

    private GameObject[] players = default;
    private GameObject[] teams = default;
    private GameObject[] chairs = default;
    private GameObject[] goals = default;
    private GameObject ball = default;

    // instantiates the chairs for both teams with the specified positions
    // @params: the chair prefab, the length from goal to goal, the distance between chairs
    void SetChairs(GameObject[] chairPrefabs, float arenaLength, float chairDistance)
    {
        float[] lengths = { -arenaLength, -arenaLength, arenaLength, arenaLength };
        float[] distances = { chairDistance, -chairDistance, -chairDistance, chairDistance };
        GameObject[] chairs = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            // chairs at index 0 and 1 are assigned to team 1
            // chairs at index 2 and 3 are assigned to team 2 
            GameObject chair = Instantiate(chairPrefabs[i]);
            chair.transform.position = new Vector3(lengths[i], 0, distances[i]);
            chairs[i] = chair;
        }

        this.chairs = chairs;
    }

    // instantiates the players based on which players are AI or Human, and which teams they should be assigned to
    // @params: an array of selected player prefabs, an array of the selected player types, the lenght and distance to position the players
    void SetPlayers(GameObject[] playerPrefabs, string[] playerTypes, float arenaLength, float chairDistance)
    {
        float[] lengths = { -arenaLength, -arenaLength, arenaLength, arenaLength };
        float[] distances = { chairDistance, -chairDistance, -chairDistance, chairDistance };
        GameObject[] players = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            GameObject player = Instantiate(playerPrefabs[i]);
            Player p = player.GetComponent<Player>();

            p.SetPlayerType(playerTypes[i]);
            p.SetPlayerNumber(i + 1);
            p.SetChair(chairs[i]);

            // set player position
            player.transform.position = new Vector3(lengths[i], 0.75f, distances[i]);
            players[i] = player;
        }

        // add players to instance variable 
        this.players = players;

        // assign the players to the 2 teams 
        // SetTeams(teamPrefab, players, arenaLength);
    }

    // instantiate the goals which the players need to hit 
    // each teams goals are positioned on the opposite side of them
    // @params: the goal prefab, the length of the arena 
    void SetGoals(GameObject goalPrefab, float arenaLength)
    {
        float[] lengths = { arenaLength, -arenaLength };
        GameObject[] goals = new GameObject[2];

        for (int i = 0; i < 2; i++)
        {
            GameObject goal = Instantiate(goalPrefab);
            Vector3 pos = new Vector3(lengths[i], 0.25f, 0);
            goal.transform.position = pos;
            goal.GetComponent<Goal>().SetDefaultPos(pos);
            goals[i] = goal;
        }

        this.goals = goals;
    }

    // instantiate the ball which the players need to throw
    // @params: the ball prefab, the length of the arena
    void SetBall(GameObject ballPrefab, float arenaLength)
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.position = new Vector3(arenaLength - 1, 0, 0);
        this.ball = ball;
    }

    // instantiate team prefab with the selected players 
    // @params: the team prefab (an empty gameobject), an array of instantiated players
    void SetTeams(GameObject teamPrefab, float arenaLength)
    {
        float[] lengths = { -arenaLength, arenaLength };
        GameObject[] teams = new GameObject[2];

        for (int i = 0; i < 2; i++)
        {
            // instantiate and set position of teams
            GameObject team = Instantiate(teamPrefab);
            team.transform.position = new Vector3(lengths[i], 0.25f, 0);
            teams[i] = team;
            Team tm = team.GetComponent<Team>();

            // default team names are "Team 1" and "Team 2"
            string teamName = "Team " + (i + 1);
            tm.SetTeamName(teamName);

            // team numbers are always 1 and 2 
            tm.SetTeamNumber((i + 1));

            // player 1 and 2 are assigned to team 1, player 3 and 4 are assigned to team 2
            GameObject[] teamPlayers = { players[i + (i % 2)], players[i + (i % 2) + 1] };
            tm.SetPlayers(teamPlayers);

            // the players also get a reference to the teams they are assigned to 
            foreach (GameObject player in teamPlayers) { player.GetComponent<Player>().SetTeam(team); }

            // team gets a reference to a goal
            tm.SetGoal(goals[i]);

            // goals also gets a reference to team
            goals[i].GetComponent<Goal>().SetTeam(team);
        }
        this.teams = teams;
    }

    // instantiates the arena and all the interactable elements in the arena
    // @params: the arena prefab, an array of selected playertypes 
    public void SetupArena(GameObject arena, string[] playerTypes)
    {
        // Instantiate(arena); // only necessary if we make arenas as prefabs
        SetChairs(chairPrefabs, arenaLength, chairDistance);
        SetPlayers(playerPrefabs, playerTypes, arenaLength, chairDistance);
        SetGoals(goalPrefab, arenaLength);
        SetBall(ballPrefab, arenaLength);
        SetTeams(teamPrefab, arenaLength);
    }

    // @return: all players
    public GameObject[] GetPlayers()
    {
        return players;
    }

    // @return: all teams
    public GameObject[] GetTeams()
    {
        return teams;
    }

    // @return: all chairs
    public GameObject[] GetChairs()
    {
        return chairs;
    }

    // @return: all goals
    public GameObject[] GetGoals()
    {
        return goals;
    }

    // @return: the ball
    public GameObject GetBall()
    {
        return ball;
    }
}
