using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class pickableObject : MonoBehaviour
{
    public GameObject goalObject, homeObject;
    [Header("Decoration")]
    public float decorRotationSpeed;
    public bool destroyWhenReturnHome = true, canBePlacedOnOtherDecorations = false;
    [HideInInspector]
    public bool isPickedUp = false, isTouchingGoal = false, isTouchingHome = false;
    [Header("Tool")]
    public UnityEvent doWhenToolIsTouchingGoal;
    Vector3 returnToPos;
    Animator a;
    Slider slider;
    SpriteRenderer sr;
    [Header("Spawning Tool")]
    public GameObject spawnedObjectsGoalObject;
    public GameObject spawnObject, spawnPoint, sliderSpawnPoint;
    public Color spawnedObjectColor;
    public bool needsRecharge, spawnsForAWhile = false;
    public bool rotateSpawnedObjects;
    public float spawnRate = 0.1f, spawnAfterDelay, spawnsForTime;
    [HideInInspector]
    public bool recharged = false;
    GameObject spawnedObject;
    float spawnedAmount, fullSpawnedAmount, passedTime;
    addLiquid al;
    [HideInInspector]
    public Color initialColor;

    void Start()
    {
        returnToPos = transform.position;
        TryGetComponent<SpriteRenderer>(out sr);
        initialColor = sr.color;
        TryGetComponent<Animator>(out a);
        TryGetComponent<addLiquid>(out al);
    }

    private void Update()
    {
        Invoke(gameObject.tag, 0f);
       
        if (sr != null) {
            if (isTouchingGoal || isTouchingHome || transform.position == returnToPos) sr.color = initialColor;
            else if (isPickedUp) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        }

        if (spawnsForAWhile && IsInvoking("spawnObjects")) {
            passedTime += Time.deltaTime;
            if (passedTime >= spawnsForTime + spawnAfterDelay) {
                CancelInvoke("spawnObjects");
                passedTime = 0;
            }
        }
    }

    void Ingredient() {
        if (TryGetComponent<Rigidbody2D>(out _) && isTouchingGoal) Cook.AddScoreForObject(gameObject);
        else if (isPickedUp && Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (isTouchingGoal || isTouchingHome)
            {
                PickUpAndFollowCoursor.Drop();
                isPickedUp = false;

                if (isTouchingGoal)
                {
                    Cook.AddScoreForObject(gameObject);
                }
                else if (isTouchingHome)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (isPickedUp && Input.GetMouseButtonUp(1)) PutBack();
        else if (isTouchingHome && !PickUpAndFollowCoursor.IsHoldingObject()) Destroy(gameObject, 0.2f);
    }

    void SpawnedIngredient() {
        if (isTouchingGoal) Destroy(gameObject, 0.1f);
        else Destroy(gameObject, 3f);
    }

    void Decoration() {
        if (isPickedUp && (isTouchingGoal || isTouchingHome) && Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            PickUpAndFollowCoursor.Drop();
            isPickedUp = false;
            if (isTouchingHome && destroyWhenReturnHome) Destroy(gameObject);
            // else if (isTouchingHome) transform.SetParent(homeObject.transform);
            // else if (isTouchingGoal) transform.SetParent(goalObject.transform);
        }
        else if (isPickedUp && Input.GetMouseButtonUp(1)) PutBack();
        else if (isPickedUp && Input.mouseScrollDelta != new Vector2(0, 0))
        {
            transform.Rotate(0, 0, Input.mouseScrollDelta.y * decorRotationSpeed);
        }
        else if (isTouchingHome && destroyWhenReturnHome && !PickUpAndFollowCoursor.IsHoldingObject()) Destroy(gameObject, 0.2f);
    }

    void Tool() {
        if (isPickedUp && isTouchingGoal && Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            doWhenToolIsTouchingGoal.Invoke();
        }
        else if (isPickedUp && Input.GetMouseButtonUp(1)) PutBack();
    }

    public void SpawningTool() {
        if (isPickedUp && Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
           if (isTouchingGoal && (!needsRecharge || (needsRecharge && recharged))) {
                if (!IsInvoking("spawnObjects")) {
                    if (spawnsForAWhile) InvokeRepeating("spawnObjects", spawnAfterDelay, spawnRate);
                    else spawnedObject = Instantiate(spawnObject, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, transform.position.z - 1), transform.rotation);

                    if (needsRecharge) {
                        recharged = false;
                        a.SetTrigger("empty");
                        a.ResetTrigger("fill");
                    }
                }

                if (spawnedObject != null) {
                    if (rotateSpawnedObjects) spawnedObject.transform.Rotate(new Vector3(0, 0, Random.Range(-270, 270)));

                    if (spawnedObject.TryGetComponent<pickableObject>(out pickableObject po)) {
                        po.goalObject = spawnedObjectsGoalObject;
                        po.homeObject = gameObject;
                    }
                }
            }
            else if (isTouchingHome && needsRecharge) {
                recharged = true;
                a.SetTrigger("fill");
            }
        }
        else if (isPickedUp && Input.GetMouseButtonUp(1)) PutBack();
        
        if (a != null && a.runtimeAnimatorController.name == "pouring") {
            if (isPickedUp && isTouchingGoal) {
                a.ResetTrigger("pourEnd");
                a.Play("pourStartPouring");
            }
            else if (isPickedUp || Input.GetMouseButtonUp(1)) a.SetTrigger("pourEnd");
        }
    }

    void ConstantlySpawningTool() {
        if (!isTouchingGoal && IsInvoking())
        {
            a.SetTrigger("pourEnd");
            CancelInvoke();
            al?.resetStep();
            if (slider != null) SliderControl.destroySlider();
            Cook.AddScoreForObject(gameObject, spawnedAmount);
            spawnedAmount = 0;
        }
        else if (isPickedUp && isTouchingGoal && Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (!IsInvoking("sliderFill")) {
                slider = SliderControl.createSlider(gameObject, (sliderSpawnPoint == null)? default : sliderSpawnPoint.transform.position, true, false);
                if (slider == null) return;
                a.Play("pourStartPouring");
                slider.value = fullSpawnedAmount;
                slider.GetComponentInChildren<TextMeshProUGUI>().text = fullSpawnedAmount.ToString();
                InvokeRepeating("sliderFill", 1f, 0.1f);
                InvokeRepeating("spawnObjects", spawnAfterDelay, spawnRate);
            }
            else {
                a.SetTrigger("pourEnd");
                CancelInvoke();
                al?.resetStep();
                if (slider != null) SliderControl.destroySlider();
                Cook.AddScoreForObject(gameObject, spawnedAmount);
                spawnedAmount = 0;
            }
        }
        else if (isPickedUp && Input.GetMouseButtonUp(1))
        {
            PutBack();
            if (IsInvoking()) {
                a.SetTrigger("pourEnd");
                CancelInvoke();

                al?.resetStep();
                
                if (slider != null) SliderControl.destroySlider();
                Cook.AddScoreForObject(gameObject, spawnedAmount);
            }
            spawnedAmount = 0;
        }
    }

    private IEnumerator OnMouseUp() {
        if (!isPickedUp && !PickUpAndFollowCoursor.IsHoldingObject() && !EventSystem.current.IsPointerOverGameObject())
        {
            PickUpAndFollowCoursor.PickUp(gameObject);
            if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
            yield return new WaitForSeconds(.1f);
            isPickedUp = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == goalObject)
            isTouchingGoal = true;
        else if (gameObject.CompareTag("Decoration") && other.gameObject.CompareTag("Decoration") && canBePlacedOnOtherDecorations)
            isTouchingGoal = true;
        else if (other.gameObject == homeObject)
            isTouchingHome = true;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject == goalObject)
            isTouchingGoal = true;
        else if (gameObject.CompareTag("Decoration") && other.gameObject.CompareTag("Decoration") && canBePlacedOnOtherDecorations)
            isTouchingGoal = true;
        else if (other.gameObject == homeObject)
            isTouchingHome = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == goalObject)
            isTouchingGoal = false;
        else if (gameObject.CompareTag("Decoration") && other.gameObject.CompareTag("Decoration") && canBePlacedOnOtherDecorations)
            isTouchingGoal = false;
        else if (other.gameObject == homeObject)
            isTouchingHome = false;
    }

    public void PutBack() {
        PickUpAndFollowCoursor.Drop();
        isPickedUp = false;
        StartCoroutine("MoveToGoal", returnToPos);
    }

    IEnumerator MoveToGoal(Vector3 goal) {
        if (transform.position != goal) {
            transform.position = Vector3.MoveTowards(transform.position, goal, 1);
            yield return new WaitForSeconds(0.01f);
            StartCoroutine("MoveToGoal", goal);
        }
    }

    void sliderFill() {
        spawnedAmount += 10;
        fullSpawnedAmount += 10;
        slider.value = fullSpawnedAmount;
        slider.GetComponentInChildren<TextMeshProUGUI>().text = fullSpawnedAmount.ToString();
    }

    void spawnObjects() { //временное?
        spawnedObject = Instantiate(spawnObject, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, transform.position.z - 1), new Quaternion());
        spawnedObject.GetComponent<SpriteRenderer>().color = spawnedObjectColor;
        if (spawnedObject.TryGetComponent<pickableObject>(out pickableObject po)) po.goalObject = spawnedObjectsGoalObject;
        if (rotateSpawnedObjects) spawnedObject.transform.Rotate(new Vector3(0, 0, Random.Range(-270, 270)));
        if (al != null) {
            if (spawnedObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer sr)) sr.color = GetComponent<addLiquid>().liquidColor;
            GetComponent<addLiquid>().liquidUp();
        }
    }

}
