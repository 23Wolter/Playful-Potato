using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameObject team;
    private bool hit;
    private Vector3 defaultPos;
    private Rigidbody rb;

    public void Start()
    {
        hit = false;
        rb = GetComponent<Rigidbody>();
    }

    public void SetTeam(GameObject team)
    {
        this.team = team;
    }

    public GameObject GetTeam()
    {
        return team;
    }

    public void SetHit(bool hit)
    {
        this.hit = hit;
    }

    public bool IsHit()
    {
        return hit;
    }

    public void SetDefaultPos(Vector3 pos)
    {
        defaultPos = pos;
    }

    public Vector3 GetDefaultPos()
    {
        return defaultPos;
    }

    // reset the goal to start position 
    public void ResetGoal()
    {
        gameObject.transform.position = defaultPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        hit = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Arena" && !hit) hit = true;
    }
}
