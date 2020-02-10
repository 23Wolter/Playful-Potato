using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float offsetX = default;
    [SerializeField] float offsetY = default;
    [SerializeField] Transform posTarget;
    [SerializeField] Transform lookTarget = default;
    [SerializeField] float smoothSpeed = 0.124f;
    [SerializeField] Vector3 offsetTarget = default;

    private bool moveCamera;
    private Transform centerPos;
    private Transform centerLook;
    private Vector3 offset;

    public void Awake()
    {
        moveCamera = false;
        centerPos = posTarget;
        centerLook = lookTarget;
        offset = offsetTarget;
    }

    public void LateUpdate()
    {
        if (moveCamera)
        {
            Vector3 desiredPosition = posTarget.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(lookTarget);
        }
    }

    public void PositionCamera(GameObject team)
    {
        float currentOffsetX = offsetX;
        Team t = team.GetComponent<Team>();
        // check if gameobject has component Team
        if (t)
        {
            float rotation = 0;
            if (t.GetTeamNumber() == 1)
            {
                rotation = 90f;
                currentOffsetX *= -1;
            }
            else if (t.GetTeamNumber() == 2)
            {
                rotation = -90f;
            }

            transform.position = new Vector3(team.transform.position.x + currentOffsetX, team.transform.position.y + offsetY, team.transform.position.z);
            transform.eulerAngles = new Vector3(30, rotation, 0);
        }
    }

    public void MoveCamera(GameObject team)
    {
        if (team != null)
        {
            posTarget = team.transform;
            offset.x *= -1;
        }
        else
        {
            posTarget = centerPos;
        }

        moveCamera = true;
    }
}
