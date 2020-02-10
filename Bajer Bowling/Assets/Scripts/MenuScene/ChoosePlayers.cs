using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoosePlayers : MonoBehaviour
{
    [SerializeField] GameObject[] players = default;
    [SerializeField] GameObject[] positions = default;
    [SerializeField] Text countdown_text = default;

    private bool[] player_isActive;
    private bool[] player_isSelected;
    private GameObject[] player_isReady;

    void Start()
    {
        countdown_text.text = "";
        player_isActive = new bool[] { false, false, false, false };
        player_isSelected = new bool[] { false, false, false, false };
        player_isReady = new GameObject[] { null, null, null, null };
    }

    void Update()
    {
        // if player 1 presses 'space' or 'A'
        if (Input.GetButtonDown("Throw_1") || Input.GetButtonDown("P1_Throw"))
        {
            // if not active, set active
            if (!player_isActive[0])
            {
                SetStartPos(0);
                players[0].SetActive(true);
                player_isActive[0] = true;
                StopCountDown();
            }
            // else toggle player select
            else
            {
                SelectPosition(0);
            }
        }

        // if player 2 presses 'e' or 'A'
        if (Input.GetButtonDown("Throw_2") || Input.GetButtonDown("P2_Throw"))
        {
            // if not active, set active
            if (!player_isActive[1])
            {
                SetStartPos(1);
                players[1].SetActive(true);
                player_isActive[1] = true;
                StopCountDown();
            }
            // else toggle player select
            else
            {
                SelectPosition(1);
            }
        }

        // if player 3 presses 'A'
        if (Input.GetButtonDown("P3_Throw"))
        {
            // if not active, set active
            if (!player_isActive[2])
            {
                SetStartPos(2);
                players[2].SetActive(true);
                player_isActive[2] = true;
                StopCountDown();
            }
            // else toggle player select
            else
            {
                SelectPosition(2);
            }
        }

        // if player 4 presses 'A'
        if (Input.GetButtonDown("P4_Throw"))
        {
            // if not active, set active
            if (!player_isActive[3])
            {
                SetStartPos(3);
                players[3].SetActive(true);
                player_isActive[3] = true;
                StopCountDown();
            }
            // else toggle player select
            else
            {
                SelectPosition(3);
            }
        }

        // get move input from player 1
        if (player_isActive[0] && !player_isSelected[0])
        {
            if (Input.GetAxisRaw("Horizontal_1") == 1 || Input.GetAxisRaw("P1_Horizontal") == 1)
            {
                SetPlayerPosition(0, "right");
            }
            else if (Input.GetAxisRaw("Horizontal_1") == -1 || Input.GetAxisRaw("P1_Horizontal") == -1)
            {
                SetPlayerPosition(0, "left");
            }
            else if (Input.GetAxisRaw("Vertical_1") == 1 || Input.GetAxisRaw("P1_Vertical") == -1)
            {
                SetPlayerPosition(0, "up");
            }
            else if (Input.GetAxisRaw("Vertical_1") == -1 || Input.GetAxisRaw("P1_Vertical") == 1)
            {
                SetPlayerPosition(0, "down");
            }
        }

        // get move input from player 2
        if (player_isActive[1] && !player_isSelected[1])
        {
            if (Input.GetAxisRaw("Horizontal_2") == 1 || Input.GetAxisRaw("P2_Horizontal") == 1)
            {
                SetPlayerPosition(1, "right");
            }
            else if (Input.GetAxisRaw("Horizontal_2") == -1 || Input.GetAxisRaw("P2_Horizontal") == -1)
            {
                SetPlayerPosition(1, "left");
            }
            else if (Input.GetAxisRaw("Vertical_2") == 1 || Input.GetAxisRaw("P2_Vertical") == -1)
            {
                SetPlayerPosition(1, "up");
            }
            else if (Input.GetAxisRaw("Vertical_2") == -1 || Input.GetAxisRaw("P2_Vertical") == 1)
            {
                SetPlayerPosition(1, "down");
            }
        }

        // get move input from player 3
        if (player_isActive[2] && !player_isSelected[2])
        {
            if (Input.GetAxisRaw("P3_Horizontal") == 1)
            {
                SetPlayerPosition(2, "right");
            }
            else if (Input.GetAxisRaw("P3_Horizontal") == -1)
            {
                SetPlayerPosition(2, "left");
            }
            else if (Input.GetAxisRaw("P3_Vertical") == -1)
            {
                SetPlayerPosition(2, "up");
            }
            else if (Input.GetAxisRaw("P3_Vertical") == 1)
            {
                SetPlayerPosition(2, "down");
            }
        }

        // get move input from player 4
        if (player_isActive[3] && !player_isSelected[3])
        {
            if (Input.GetAxisRaw("P4_Horizontal") == 1)
            {
                SetPlayerPosition(3, "right");
            }
            else if (Input.GetAxisRaw("P4_Horizontal") == -1)
            {
                SetPlayerPosition(3, "left");
            }
            else if (Input.GetAxisRaw("P4_Vertical") == -1)
            {
                SetPlayerPosition(3, "up");
            }
            else if (Input.GetAxisRaw("P4_Vertical") == 1)
            {
                SetPlayerPosition(3, "down");
            }
        }
    }

    private void SetPlayerPosition(int i, string direction)
    {
        Transform p = players[i].transform;

        if (direction == "right")
        {
            // if player 'i' is in position 1
            if (p.parent == positions[0].transform)
            {
                // set parent position 2
                p.SetParent(positions[1].transform);
                p.localPosition = new Vector3(0, 0, 0);
            }
            // if player 'i' is in position 3
            else if (p.parent == positions[2].transform)
            {
                // set parent position 4
                p.SetParent(positions[3].transform);
                p.localPosition = new Vector3(0, 0, 0);
            }
        }
        else if (direction == "left")
        {
            // if player 'i' is in position 2
            if (p.parent == positions[1].transform)
            {
                // set parent position 1
                p.SetParent(positions[0].transform);
                p.localPosition = new Vector3(0, 0, 0);
            }
            // if player 'i' is in position 4
            else if (p.parent == positions[3].transform)
            {
                // set parent position 3
                p.SetParent(positions[2].transform);
                p.localPosition = new Vector3(0, 0, 0);
            }
        }
        else if (direction == "down")
        {
            // if player 'i' is in position 1
            if (p.parent == positions[0].transform)
            {
                // set parent position 3
                p.SetParent(positions[2].transform);
                p.localPosition = new Vector3(0, 0, 0);
            }
            // if player 'i' is in position 2
            else if (p.parent == positions[1].transform)
            {
                // set parent position 4
                p.SetParent(positions[3].transform);
                p.localPosition = new Vector3(0, 0, 0);
            }
        }
        else if (direction == "up")
        {
            // if player 'i' is in position 3
            if (p.parent == positions[2].transform)
            {
                // set parent position 1
                p.SetParent(positions[0].transform);
                p.localPosition = new Vector3(0, 0, 0);
            }
            // if player 'i' is in position 4
            else if (p.parent == positions[3].transform)
            {
                // set parent position 2
                p.SetParent(positions[1].transform);
                p.localPosition = new Vector3(0, 0, 0);
            }
        }
    }

    private void SelectPosition(int i)
    {
        Color color;
        if (!player_isSelected[i])
        {
            color = Color.black;
            player_isReady[i] = players[i];
        }
        else
        {
            color = Color.white;
            player_isReady[i] = null;
        }
        players[i].GetComponent<Image>().color = color;
        player_isSelected[i] = !player_isSelected[i];

        ReadyToStartGame();
    }

    private void SetStartPos(int player)
    {
        Transform p = players[player].transform;

        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i].transform.childCount == 0)
            {
                p.SetParent(positions[i].transform);
                p.localPosition = new Vector3(0, 0, 0);
                break;
            }
        }
    }

    private void ReadyToStartGame()
    {
        int activePlayers = 0;
        int readyPlayers = 0;

        for (int i = 0; i < player_isActive.Length; i++)
        {
            if (player_isActive[i] == true) activePlayers++;
            if (player_isReady[i] == true) readyPlayers++;
        }

        if (activePlayers == readyPlayers)
        {
            StartCoroutine("StartCountDown");
        }
        else
        {
            StopCountDown();
        }
    }

    private void StopCountDown()
    {
        StopCoroutine("StartCountDown");
        countdown_text.text = "";
    }

    private IEnumerator StartCountDown()
    {
        int count = 10;
        while (count >= 0)
        {
            countdown_text.text = "Game begins in " + count;
            yield return new WaitForSeconds(1);
            count--;
        }

        SetupPlayers();
    }

    private void SetupPlayers()
    {
        GameObject player;
        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i].transform.childCount > 0)
            {
                player = positions[i].transform.GetChild(0).gameObject;
                PlayerInformation.SetPlayerType(i, player.name);
            }
        }

        SceneManager.LoadScene("MainScene");
    }
}
