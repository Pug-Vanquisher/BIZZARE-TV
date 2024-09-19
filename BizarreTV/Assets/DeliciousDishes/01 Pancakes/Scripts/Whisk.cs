using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Whisk : MonoBehaviour
{
    public GameObject whatToStir;
    public Color whatToStirEndColor;
    public float moveSpeed = 2, minDistance = 1, addStep;
    public string sayWhenNotReady = "В тесте не хватает компонентов!";
    Vector2 mousePos, newPos, curPos;
    Slider sr;
    Rigidbody2D rb;
    Collider2D co;
    pickableObject po;
    bool whiskIsMoving;
    float neededAmount, totalAmount, amount, curColorLerpStep;
    List<SpriteRenderer> whatToStirSprites = new();
    Vector3 bowl = new(-26f, 1, -5);


    void Start()
    {
        Invoke("SetAmounts", 0.1f);
        rb = GetComponent<Rigidbody2D>();
        po = GetComponent<pickableObject>();
        co = GetComponent<Collider2D>();
        foreach (SpriteRenderer t in whatToStir.GetComponentsInChildren<SpriteRenderer>())
        {
            whatToStirSprites.Add(t);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && whiskIsMoving)
        {
            StopAllCoroutines();
            co.isTrigger = true;
            whiskIsMoving = false;
            rb.velocity = new Vector2(0, 0);
            SliderControl.destroySlider();
            Cook.AddScoreForObject(gameObject, amount, false);
            amount = 0;
            // PickUpAndFollowCoursor.ResumeMove();
            if (totalAmount == neededAmount)
            {
                GetComponent<pickableObject>().goalObject.transform.parent.GetComponent<PickOnClick>().isEnabled = true;
            }
        }
    }

    public void Stir()
    {
        if (Cook.PrevStepsFinished(gameObject))
        {
            PickUpAndFollowCoursor.PauseMove();
            StartCoroutine(MoveToGoal(bowl));
        }
        else Cook.ShowText(sayWhenNotReady);
    }

    private IEnumerator MoveWhisk()
    {
        curPos = transform.position;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.velocity = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y) * moveSpeed;

        yield return new WaitForSeconds(0.2f);

        newPos = transform.position;
        if (po.isTouchingGoal && Mathf.Abs(curPos.x - newPos.x) >= minDistance && totalAmount < neededAmount)
        {
            if (!GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>() != null)
            {
                if (GetComponent<AudioSource>().enabled == true) GetComponent<AudioSource>().Play();
                else GetComponent<AudioSource>().enabled = true;
            }
            amount += addStep;
            totalAmount += addStep;
            sr.value = totalAmount;
            foreach (SpriteRenderer sr in whatToStirSprites) sr.color = Color.Lerp(sr.color, whatToStirEndColor, curColorLerpStep);
            curColorLerpStep += 1 / (neededAmount / addStep);
        }


        yield return StartCoroutine("MoveWhisk");
    }

    IEnumerator MoveToGoal(Vector3 goal)
    {
        if (!whiskIsMoving)
        {
            if (transform.localPosition != goal)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, goal, 1f);
                yield return new WaitForSeconds(0.01f);
                StartCoroutine("MoveToGoal", goal);
            }
            else
            {
                co.isTrigger = false;
                sr = SliderControl.createSlider(gameObject, (po.sliderSpawnPoint == null) ? default : po.sliderSpawnPoint.transform.position, false, true, true);
                sr.value = totalAmount;
                StartCoroutine(MoveWhisk());
                whiskIsMoving = true;
            }
        }

    }

    void SetAmounts()
    {
        neededAmount = Cook.GetNeededAmount(gameObject)[0];
        curColorLerpStep = 1 / (neededAmount / addStep);
    }
}
