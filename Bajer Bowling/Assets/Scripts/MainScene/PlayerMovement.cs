using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    private CameraController cam;
    private ArenaMovement arenaMov;
    private Player player;
    private bool stand;
    private Rigidbody rb;
    private float moveHorizontal;
    private float moveVertical;
    private Vector3 movement;

    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        arenaMov = GameObject.Find("GameManager").GetComponent<ArenaMovement>();
        stand = false;
    }

    void FixedUpdate()
    {
        if (stand)
        {
            if (player.GetPlayerType() == "P1")
            {
                moveHorizontal = Input.GetAxisRaw("Horizontal_1");
                moveVertical = Input.GetAxisRaw("Vertical_1");

                // Debug.Log("P1 wants to move: " + moveHorizontal + ", " + moveVertical);
            }

            if (player.GetPlayerType() == "P2")
            {
                moveHorizontal = Input.GetAxisRaw("Horizontal_2");
                moveVertical = Input.GetAxisRaw("Vertical_2");
            }

            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);
        }
    }

    public void Stand()
    {
        stand = true;
        cam.MoveCamera(null);
    }

    public bool IsStanding()
    {
        return stand;
    }

    public void Sit()
    {
        stand = false;

        float x = player.GetChair().position.x;
        float y = player.GetChair().position.y;
        float z = player.GetChair().position.z;
        transform.position = new Vector3(x, y + 0.75f, z);

        arenaMov.CheckPlayerMovement();
    }

    public void Reset()
    {
        Debug.Log("Reset player to chair");
    }
}
