using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class GratableObject : MonoBehaviour
{
    public GameObject[] grateResult;
    public GameObject whereSpawnResult, wherePlaceResult;
    public float howManyResultsSpawn = 9, addStep = 3;
    float neededAmount, totalAmount, amount, spawnedObjectsNum;
    Transform maskLeft, maskRight;
    GameObject spawnedResult;
    Slider sr;
    Grater grater;
    pickableObject po;
    Rigidbody2D rb;
    BoxCollider2D coLeft, coRight;
    bool objectBeingGrated = false, objectIsMoving = false;
    Vector2 mousePos, newPos, curPos;

    void Start()
    {
        Invoke("SetAmounts", 0.1f);
        po = GetComponent<pickableObject>();
        rb = GetComponent<Rigidbody2D>();
        maskLeft = transform.GetChild(0);
        maskRight = transform.GetChild(1);
        coLeft = maskLeft.GetComponent<BoxCollider2D>();
        coRight = maskRight.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && objectBeingGrated)
        {
            StopAllCoroutines();
            
            coLeft.isTrigger = true;
            coRight.isTrigger = true;

            objectIsMoving = false;
            objectBeingGrated = false;

            rb.velocity = new Vector2(0, 0);

            SliderControl.destroySlider();

            Cook.AddScoreForObject(gameObject, amount, false);
            amount = 0;
        }
    }

    public void StartGrating()
    {
        if (po.goalObject.TryGetComponent<Grater>(out grater) && grater.isOnPlace && !objectBeingGrated)
        {
            PickUpAndFollowCoursor.PauseMove();

            sr = SliderControl.createSlider(gameObject, (po.sliderSpawnPoint == null) ? default : po.sliderSpawnPoint.transform.position, false, true, true);
            if (sr == null) return;
            
            sr.value = totalAmount;
            objectBeingGrated = true;
            StartCoroutine(MoveToGoal(grater.transform.position + new Vector3(10, 0, -2)));
        }
        else if (po.goalObject.TryGetComponent<Grater>(out grater) && !grater.isOnPlace) Cook.ShowText("Тереть следует на доске", false);
    }

    IEnumerator MoveToGoal(Vector3 goal)
    {
        if (!objectIsMoving)
        {
            if (transform.position != goal)
            {
                transform.position = Vector3.MoveTowards(transform.position, goal, 1f);
                yield return new WaitForSeconds(0.01f);
                StartCoroutine("MoveToGoal", goal);
            }
            else
            {
                coLeft.isTrigger = false;
                coRight.isTrigger = false;

                objectIsMoving = true;
                StartCoroutine(Grate());
            }
        }
    }

    private IEnumerator Grate()
    {
        curPos = transform.position;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.velocity = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y) * 2;

        yield return new WaitForSeconds(0.2f);

        newPos = transform.position;
        if (po.isTouchingGoal && (coLeft.IsTouching(po.goalObject.GetComponent<Collider2D>()) || coRight.IsTouching(po.goalObject.GetComponent<Collider2D>())) && Mathf.Abs(curPos.y - newPos.y) >= 3 && totalAmount < neededAmount)
        {
            if (!GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>() != null)
            {
                if (GetComponent<AudioSource>().enabled == true) GetComponent<AudioSource>().Play();
                else GetComponent<AudioSource>().enabled = true;
            }
            if (po.goalObject.transform.position.x < transform.position.x)
                maskLeft.position += new Vector3(addStep / 15, 0, 0);
            else
                maskRight.position -= new Vector3(addStep / 15, 0, 0);

            amount += addStep;
            totalAmount += addStep;
            sr.value = totalAmount;

            if (totalAmount % howManyResultsSpawn == 0 && spawnedObjectsNum < howManyResultsSpawn)
            {
                spawnedResult = Instantiate(grateResult[Random.Range(0, grateResult.Length)], whereSpawnResult.transform.position + new Vector3(Random.Range(-3, 3), Random.Range(-1, 1), 0), Quaternion.identity, null);
                spawnedResult.GetComponent<ComeToHome>().ComeTo = wherePlaceResult;
                spawnedObjectsNum++;
            }
        }

        StartCoroutine(Grate());
    }

    void SetAmounts()
    {
        neededAmount = Cook.GetNeededAmount(gameObject)[0];
    }
}
