using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    [SerializeField] GameObject knife;
    pickableObject po;
    float rotationAngle = 50f;
    float xOffset = 1f;
    float rotationSpeed = 70f;
    float moveSpeed = 2f;
    bool cut = false;
    private void Start() {
        po = GetComponent<pickableObject>();
    }
    private void Update() {
        if (Input.GetMouseButtonDown(0) && cut && po.isTouchingGoal && !EventSystem.current.IsPointerOverGameObject())
        {
            Destroy(po);
            StartCoroutine(SplitTomato(gameObject));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == knife) cut = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == knife) cut = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == knife) cut = false;
    }

    private IEnumerator SplitTomato(GameObject parent)
    {
        parent.GetComponent<SpriteRenderer>().enabled = false;
        parent.GetComponent<CircleCollider2D>().enabled = false;

        moveSpeed += Random.Range(-0.6f, 0.6f);

        Transform child1 = parent.transform.GetChild(0);
        Transform child2 = parent.transform.GetChild(1);

        // Enable children
        child1.gameObject.SetActive(true);
        child2.gameObject.SetActive(true);

        // Rotate children
        float currentAngle = 0f;
        Vector3 targetPos1 = child1.position - Vector3.right * xOffset;
        Vector3 targetPos2 = child2.position + Vector3.right * xOffset;
        while (currentAngle < rotationAngle)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            child1.RotateAround(parent.transform.position, Vector3.forward, rotationStep);
            child1.position = Vector3.MoveTowards(child1.position, targetPos1, moveSpeed * Time.deltaTime);
            child2.RotateAround(parent.transform.position, -Vector3.forward, rotationStep);
            child2.position = Vector3.MoveTowards(child2.position, targetPos2, moveSpeed * Time.deltaTime);
            currentAngle += rotationStep;
            yield return Time.deltaTime;
        }
        
        child1.gameObject.GetComponent<Collider2D>().enabled = true;
        child2.gameObject.GetComponent<Collider2D>().enabled = true;

        Cook.AddScoreForObject(gameObject, 1, false);
    }
}
