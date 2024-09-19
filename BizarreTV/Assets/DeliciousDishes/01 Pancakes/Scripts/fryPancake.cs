using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class fryPancake : MonoBehaviour
{
    public Transform sliderSpawnPoint;
    public float cookingDelay = 0.2f, moveSpeed = 2f;
    public GameObject curPancake;
    public Temperature tem;
    float curColorLerpStep, minimalTemp, maximalTemp, medianTemp, pancakeAngle, panAngle = 0;
    SpriteRenderer doughSprite, pancakeSprite;
    Color startDoughColor, startPancakeColor;
    Vector2 mousePos;
    Vector3 homePos;
    bool penIsMoving, pancakeFlipped = false;
    Rigidbody2D rb, pancakeRb;
    Slider sl;
    Coroutine c;

    pancakeSides curSide1 = new(), curSide2 = new();

    class pancakeSides
    {
        public Color doughColor, pancakeColor;
        public float pancakeTemp;
        public pancakeSides()
        {
            doughColor = pancakeColor = Color.white;
            pancakeTemp = 0;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        homePos = transform.position;
        Invoke("SetAmounts", 0.1f);
    }

    void Update()
    {
        if (tem.TempNow > 60) cookingDelay = Mathf.Abs(1 - tem.KoefTime) / 10;
        else cookingDelay = 10;

        if (sl != null)
        {
            if (Input.GetMouseButtonDown(1) && penIsMoving)
            {
                StopCoroutine(c);
                StartCoroutine(MovePanToTheBurner(homePos));
                penIsMoving = false;
                rb.rotation = 0;
                rb.velocity = pancakeRb.velocity = new Vector2(0, 0);
                PickUpAndFollowCoursor.Drop();

                if (pancakeFlipped) sl.value = curSide2.pancakeTemp;
                else sl.value = curSide1.pancakeTemp;
                sl.gameObject.SetActive(true);
            }
        }
        if (curPancake != null)
        {
            if (curPancake.transform.position.y < 40) curPancake.transform.position = new Vector3(103.84f, 68.8f, -3);

            pancakeAngle = curPancake.transform.rotation.eulerAngles.z;
            if (pancakeAngle <= 270 && pancakeAngle > 90 && !pancakeFlipped) pancakeFlipped = true;
            else if ((pancakeAngle > 270 || pancakeAngle <= 90) && pancakeFlipped) pancakeFlipped = false;

            else if (!IsInvoking("SliderFill") && doughSprite != null && pancakeSprite != null)
            {
                if (pancakeFlipped)
                {
                    doughSprite.color = curSide2.doughColor;
                    pancakeSprite.color = curSide2.pancakeColor;
                }
                else
                {
                    doughSprite.color = curSide1.doughColor;
                    pancakeSprite.color = curSide1.pancakeColor;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (sl != null && penIsMoving && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            StopCoroutine("penRotate");
            StartCoroutine("penRotate", true);
        }
    }

    void SetAmounts()
    {
        minimalTemp = Cook.GetNeededAmount(gameObject)[0];
        maximalTemp = Cook.GetNeededAmount(gameObject)[1];
        medianTemp = (minimalTemp + maximalTemp) / 2;
        curColorLerpStep = 1 / medianTemp;
    }

    public void StartCooking()
    {
        if (sl == null) sl = SliderControl.createSlider(gameObject, sliderSpawnPoint.position, true, true, true);
        pancakeRb = curPancake.GetComponent<Rigidbody2D>();

        doughSprite = curPancake.GetComponentsInChildren<SpriteRenderer>()[0];
        pancakeSprite = curPancake.GetComponentsInChildren<SpriteRenderer>()[1];

        startDoughColor = doughSprite.color;
        startPancakeColor = pancakeSprite.color;

        if (curSide1.doughColor == curSide1.pancakeColor)
        {
            curSide1.doughColor = startDoughColor;
            curSide1.pancakeColor = startPancakeColor;
        }
        if (curSide2.doughColor == curSide2.pancakeColor)
        {
            curSide2.doughColor = startDoughColor;
            curSide2.pancakeColor = startPancakeColor;
        }

        curColorLerpStep = 1 / medianTemp;
        Invoke("SliderFill", 0.5f);

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3);
    }

    public void SliderFill()
    {
        if (sl != null && sl.value != sl.maxValue)
        {
            sl.value++;

            if (pancakeFlipped)
                ColorPancake(curSide2);
            else
                ColorPancake(curSide1);

            if (rb.IsTouching(pancakeRb.GetComponents<Collider2D>()[0]) || rb.IsTouching(pancakeRb.GetComponents<Collider2D>()[1])) Invoke("SliderFill", cookingDelay);
        }
    }

    void ColorPancake(pancakeSides side)
    {
        if (sl.value <= minimalTemp)
        {
            doughSprite.color = Color.Lerp(startDoughColor, new Color(1, 1, 1, 0), curColorLerpStep);
            if (sl.value == minimalTemp) curColorLerpStep = 1 / minimalTemp; // сброс шага, а то блин сразу будет горелый после этого
            curColorLerpStep += 1 / minimalTemp;
            side.doughColor = doughSprite.color;
        }
        else if (sl.value > maximalTemp)
        {
            pancakeSprite.color = Color.Lerp(startPancakeColor, new Color(0.4f, 0.2f, 0, 1), curColorLerpStep);
            curColorLerpStep += 1 / minimalTemp;
            side.pancakeColor = pancakeSprite.color;
        }

        side.pancakeTemp = sl.value;
    }

    private void OnMouseOver()
    {
        if (sl != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) && !penIsMoving) PickUpPen();

            else if (Input.GetMouseButtonUp(0) && penIsMoving)
            {
                StopCoroutine("penRotate");
                StartCoroutine("penRotate", false);
            }
        }
    }

    public void PickUpPen()
    {
        if (!PickUpAndFollowCoursor.IsHoldingObject())
        {
            PickUpAndFollowCoursor.PickUp(gameObject, false);
            PickUpAndFollowCoursor.PauseMove();

            CancelInvoke("SliderFill");
            sl.gameObject.SetActive(false);

            c = StartCoroutine(MovePen());
            penIsMoving = true;

            curPancake.layer = gameObject.layer = 15;
        }
    }

    private IEnumerator MovePen()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rb.velocity = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y) * moveSpeed;

        yield return new WaitForSeconds(0.2f);
        yield return c = StartCoroutine(MovePen());
    }

    IEnumerator MovePanToTheBurner(Vector2 goal)
    {
        if (Vector2.Distance(gameObject.transform.position, goal) > 0.1f)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(goal.x - transform.position.x, goal.y - transform.position.y) * moveSpeed;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3);
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(MovePanToTheBurner(goal));
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3);

            if (rb.IsTouching(pancakeRb.GetComponents<Collider2D>()[0]) || rb.IsTouching(pancakeRb.GetComponents<Collider2D>()[1]))
            {
                curPancake.layer = gameObject.layer = 0;
                StartCooking();
            }
        }
    }

    IEnumerator penRotate(bool down = true)
    {
        if (down && rb.rotation < 30 || !down && rb.rotation > 0)
        {
            rb.MoveRotation(panAngle);
            if (down && rb.rotation < 30) panAngle++;
            else if (!down && rb.rotation > 0) panAngle--;

            yield return new WaitForSeconds(down ? 0.025f : 0.015f);
            yield return penRotate(down);
        }
    }

    public void FinishCooking()
    {
        StopAllCoroutines();
        CancelInvoke();
        SliderControl.destroySlider();

        float totalScore = calculateScore(curSide1.pancakeTemp) + calculateScore(curSide2.pancakeTemp);
        if (totalScore < 0) Cook.TakeScoreManually(-totalScore);
        else Cook.AddScoreManually(totalScore);

        pancakeFlipped = false;
        curSide1 = new(); // если их приравнять, они и дальше будут равны
        curSide2 = new(); // и будет готовиться сразу две стороны блина
        doughSprite = pancakeSprite = new();
    }

    float calculateScore(float value)
    {
        float score = 0;
        if (value < minimalTemp || value >= medianTemp * 1.75f) score = -Cook.GetWrongMoveTakesValue();
        else if (value >= minimalTemp && value < maximalTemp) score = Cook.GetRightMoveGiveValue();
        else if (value >= maximalTemp && value < medianTemp * 1.75f) score = Cook.GetRightMoveGiveValue() * 0.5f;
        return score;
    }
}
