using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GolfMinigameController : MonoBehaviour
{

    public static Action ballRolledAway;
    public static Action ballRolledAtHole;


    private Vector3 golfBallSpawnPoint;
    [SerializeField]
    private GameObject golfBall;

    [SerializeField]
    private GameObject[] newBalls;

    private int crntBallInd;

    private void OnEnable()
    {
        ballRolledAtHole += AddBall;
        ballRolledAway += RespawnGolfBall;
    }

    private void OnDisable()
    {
        ballRolledAtHole -= AddBall;
        ballRolledAway -= RespawnGolfBall;
    }

    private void Start()
    {
        golfBallSpawnPoint = golfBall.transform.localPosition;
    }


    private void AddBall()
    {
        if (crntBallInd < newBalls.Length)
        {
            newBalls[crntBallInd].SetActive(true);
            crntBallInd++;
            RespawnGolfBall();
        }
    }


    public void RespawnGolfBall()
    {
        StopAllCoroutines();
        StartCoroutine("RespawnBallDelay");
     
    }

    IEnumerator RespawnBallDelay()
    {
        yield return new WaitForSeconds(1f);

        golfBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        golfBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        golfBall.transform.localPosition = golfBallSpawnPoint;
    }




}
