using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaMovement : MonoBehaviour
{
    private CameraController cam;
    private GameManager manager;
    private GameObject[] players;

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        manager = GetComponent<GameManager>();
    }

    // checks if all players are sitting down, if they are then the camera is reset to the ball holding player
    public void CheckPlayerMovement()
    {
        GameObject[] players = manager.GetPlayers();
        GameObject team = null;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<PlayerMovement>().IsStanding())
            {
                Debug.Log("one player is still standing");
                return;
            }
            Debug.Log("player is sitting");

            Player player = players[i].GetComponent<Player>();
            if (player.HasBall())
            {
                team = player.GetTeam();
            }
        }

        cam.MoveCamera(team);
    }
}
