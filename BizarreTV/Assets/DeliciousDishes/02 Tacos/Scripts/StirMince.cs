using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirMince : MonoBehaviour
{
    public Transform moveThere;
    public FryMince fryMince;
    float moveSpeed = 2, minDistance = 4;
    Vector2 mousePos, newPos, curPos;
    Rigidbody2D rb;
    Collider2D co;
    pickableObject po;
    bool spatulaIsMoving;
    int stirCounter = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        po = GetComponent<pickableObject>();
        co = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && spatulaIsMoving)
        {
            StopAllCoroutines();
            co.isTrigger = true;
            spatulaIsMoving = false;
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void Stir()
    {
        if (fryMince.isFrying)
        {
            PickUpAndFollowCoursor.PauseMove();
            StartCoroutine(MoveToGoal(moveThere.position));
        }
        else Cook.ShowText("Нечего перемешать", false);
    }

    IEnumerator MoveSpatula()
    {
        curPos = transform.position;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.velocity = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y) * moveSpeed;

        yield return new WaitForSeconds(0.2f);

        newPos = transform.position;
        if (po.isTouchingGoal && Mathf.Abs(curPos.x - newPos.x) >= minDistance && stirCounter < 4)
        {
            if (!GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>() != null)
            {
                if (GetComponent<AudioSource>().enabled == true) GetComponent<AudioSource>().Play();
                else GetComponent<AudioSource>().enabled = true;
            }
            stirCounter++;
            if (stirCounter == 4)
            {
                stirCounter = 0;
                fryMince.isStirred = true;
            }
        }

        yield return StartCoroutine("MoveSpatula");
    }

    IEnumerator MoveToGoal(Vector3 goal)
    {
        if (!spatulaIsMoving)
        {
            if (transform.position != goal)
            {
                transform.position = Vector3.MoveTowards(transform.position, goal, 1f);
                yield return new WaitForSeconds(0.01f);
                StartCoroutine("MoveToGoal", goal);
            }
            else
            {
                co.isTrigger = false;
                StartCoroutine(MoveSpatula());
                spatulaIsMoving = true;
            }
        }
    }
}
