using System.Collections;
using UnityEngine;

public class pancakeMoveToPlate : MonoBehaviour
{
    public fryPancake fry;
    public PourDough pd;
    public FinalMove fm;
    public GameObject dropPancakeHere, plate;
    GameObject pancake;
    Collider2D[] cols;
    ContactPoint2D[] conts = new ContactPoint2D[10];
    Rigidbody2D rb;
    float zChange = 0;
    bool pancakeIsMoved = false;
    private void Update() {
        if (fry.curPancake != null) pancake = fry.curPancake;
    }

    public void MovePancakeToPlate() {
        if (pancake != null && !pancakeIsMoved)
        {
            pancakeIsMoved = true;
            pancake.layer = 15;
            fry.FinishCooking();
            StartCoroutine(MoveToGoal(pancake, dropPancakeHere.transform.position));
        }
        else Cook.ShowText("Нечего снять со сковороды", false);
    }

    IEnumerator MoveToGoal(GameObject whomoves, Vector3 goal) {
        if (whomoves.transform.position != goal)
        {
            whomoves.transform.position = Vector3.MoveTowards(whomoves.transform.position, goal, 2);
            yield return new WaitForSeconds(0.01f);
            yield return StartCoroutine(MoveToGoal(whomoves, goal));
        }
        else
        {
            pancake.layer = 0;
            pancake.transform.SetParent(plate.transform);
            pancake.transform.position += new Vector3(0, 0, zChange);
            zChange -= 0.01f;
            Invoke("turnOffCols", 1f);
            fry.curPancake = pd.currentPancake = null;
        }
    }

    void turnOffCols() {
        cols = pancake.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < 2; i++) if (cols[i].GetContacts(conts) == 0) cols[i].isTrigger = true;
        rb = pancake.GetComponent<Rigidbody2D>();
        Destroy(rb);
        pancake = null;
        if (pd.DishCanBeServed()) fm.Finalmove = true;
        pancakeIsMoved = false;
    }
}
