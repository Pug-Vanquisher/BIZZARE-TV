using UnityEngine.EventSystems;
using UnityEngine;

public class PourDough : MonoBehaviour
{
    public GameObject pancakePrefab, pancakeSpawnPosition, dough, doughEndPointGameobject;
    public int pancakesMinAmount = 1, pancakesMaxAmount = 10;
    public GameObject currentPancake;
    public fryPancake fry;
    pickableObject po;
    float doughMoveStep, curDoughMoveStep = 0;
    int curPancakesAmount = 0;
    Vector3 doughEndPoint;
    void Start()
    {
        po = GetComponent<pickableObject>();
        doughEndPoint = doughEndPointGameobject.transform.localPosition;
        doughMoveStep = (dough.transform.localPosition.y - doughEndPoint.y) / pancakesMaxAmount;
    }

    void Update()
    {
        if (po.isPickedUp && Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) {
            if (currentPancake == null && curPancakesAmount < pancakesMaxAmount)
            {
                if (po.isTouchingHome && !po.recharged && dough.transform.position != doughEndPoint) {
                    Invoke("TakeDough", 0);
                }
                else if (po.isTouchingGoal && po.recharged)
                {
                    PickUpAndFollowCoursor.PauseMove();
                    Invoke("CreatePancake", po.spawnAfterDelay + 0.25f);
                }
                po.SpawningTool();
            }
            else if (po.isTouchingHome) {
                if (currentPancake != null) Cook.ShowText("Сначала допеките блинчик", false);
                else if (curPancakesAmount >= pancakesMaxAmount) Cook.ShowText("Тесто кончилось", false);
            }
        }
    }

    public void TakeDough() {
        curDoughMoveStep = 0;
        InvokeRepeating("LiquidDown", 0, 0.05f);
    }

    void CreatePancake() {
        if (GetComponent<AudioSource>() != null) {
            if (GetComponent<AudioSource>().enabled == true) GetComponent<AudioSource>().Play();
            else GetComponent<AudioSource>().enabled = true;
        }
        currentPancake = Instantiate(pancakePrefab, new Vector3(pancakeSpawnPosition.transform.position.x, pancakeSpawnPosition.transform.position.y, pancakePrefab.transform.position.z), new Quaternion());
        currentPancake.GetComponentsInChildren<SpriteRenderer>()[1].transform.Rotate(new Vector3(0, 0, Random.Range(-270, 270)));
        curPancakesAmount++;
        fry.curPancake = currentPancake;
        Invoke("startCook", 0.7f);
    }

    void LiquidDown() {
        if (curDoughMoveStep < doughMoveStep)
        {
            dough.transform.localPosition = Vector3.MoveTowards(dough.transform.localPosition, doughEndPoint, 0.1f);
            curDoughMoveStep += 0.1f;
        }
        else CancelInvoke("liquidDown");
    }

    void startCook() {
        fry.StartCooking();
        po.PutBack();
    }

    public bool DishCanBeServed() {
        return curPancakesAmount >= pancakesMinAmount;
    }
}
