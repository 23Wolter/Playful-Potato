using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text team1_name = default;
    [SerializeField] Text team2_name = default;
    [SerializeField] Text team1_score = default;
    [SerializeField] Text team2_score = default;
    [SerializeField] Text win_text = default;

    // sets the team name displayed in the UI
    // @params: the team number to set name of, the name of the team
    public void SetTeamName(int teamNumber, string name)
    {
        if (teamNumber == 1) team1_name.text = name;
        else if (teamNumber == 2) team2_name.text = name;
    }

    // gets the name displayed in the UI
    // @params: the team number to get name of
    // @returns: the name text of the team 
    public string GetTeamName(int teamNumber)
    {
        if (teamNumber == 1) return team1_name.text;
        else if (teamNumber == 2) return team2_name.text;
        else return null;
    }

    // sets the score displayed in the UI
    // @params: the team number to set score of, the score to set
    public void SetTeamScore(int teamNumber, int score)
    {
        if (teamNumber == 1) team1_score.text = score.ToString();
        else if (teamNumber == 2) team2_score.text = score.ToString();
    }

    // gets the score displayed in the UI
    // @params: the team number to get the score of
    // @returns: the score of the team as a string
    public int GetTeamScore(int teamNumber)
    {
        if (teamNumber == 1) return int.Parse(team1_score.text);
        else if (teamNumber == 2) return int.Parse(team2_score.text);
        else return 0;
    }

    // increments the score of the team specified by the parameter by 1
    // @params: the team number to increment score of 
    public void IncrementTeamScore(int teamNumber)
    {
        int score;
        if (teamNumber == 1)
        {
            score = int.Parse(team1_score.text);
            score++;
            team1_score.text = score.ToString();
        }
        else if (teamNumber == 2)
        {
            score = int.Parse(team2_score.text);
            score++;
            team2_score.text = score.ToString();
        }
    }

    public void SetWinText(Team team)
    {
        string teamName = team.GetTeamName();
        win_text.text = teamName + " wins!";
    }
}
