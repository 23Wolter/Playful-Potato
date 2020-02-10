using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private CameraController cam = default;
    private GameObject player;
    private Rigidbody rb;
    private bool isThrown;
    private bool isMoving;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StopBall();
    }

    public void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    void Update()
    {
        if (isThrown && rb.velocity.magnitude >= 0.2f)
        {
            isThrown = false;
            isMoving = true;
        }

        if (isMoving && rb.velocity.magnitude < 0.2f)
        {
            isMoving = false;
            player.GetComponent<Player>().EndTurn();
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    // adds force to the ball to throw it down the field
    // @params: speed to throw the ball, direction the ball moves in 
    public void ThrowBall(float speed, float direction)
    {
        Vector3 movement = new Vector3(direction, 0.0f, 0.0f);
        rb.AddForce(movement * speed);
        isThrown = true;
        cam.MoveCamera(null);
    }

    public void StopBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
