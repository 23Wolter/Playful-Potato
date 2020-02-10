using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public IEnumerator ShootBall(GameObject ball, float ballDirection, float throwForce)
    {
        yield return new WaitForSeconds(4);
        ball.GetComponent<BallMovement>().ThrowBall(throwForce, ballDirection);

    }
}
