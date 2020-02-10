using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    private string teamName;
    private int teamNumber;
    private GameObject[] players;
    private GameObject goal;

    public string GetTeamName()
    {
        return teamName;
    }

    public void SetTeamName(string name)
    {
        teamName = name;
    }

    public int GetTeamNumber()
    {
        return teamNumber;
    }

    public void SetTeamNumber(int teamNumber)
    {
        this.teamNumber = teamNumber;
    }

    public GameObject[] GetPlayers()
    {
        return players;
    }

    public void SetPlayers(GameObject[] players)
    {
        this.players = players;
    }

    public GameObject GetGoal()
    {
        return goal;
    }

    public void SetGoal(GameObject goal)
    {
        this.goal = goal;
    }

    override
    public string ToString()
    {
        return "Team " + GetTeamName() + " is ready with " + GetPlayers().Length + " players";
    }
}
