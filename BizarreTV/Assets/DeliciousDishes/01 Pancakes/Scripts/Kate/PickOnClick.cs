using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
using Cinemachine;

public class PickOnClick : MonoBehaviour
{
    public GameObject Doplganger; 
    float moveSpeed = 20.0f;
    CinemachineVirtualCamera vcam1;
    public bool isEnabled = true, pickOnDrag = false;
    private bool isMoving = false;

    private void Start() {
        vcam1 = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnMouseOver()
    {
        if (!isMoving && isEnabled && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && pickOnDrag)
        {
            Destroy(GetComponent<Collider2D>());
            StartCoroutine(MoveObject());
            Doplganger.SetActive(true);
        }
    }

    private void OnMouseDown()
    {
        if (!isMoving && isEnabled && Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && !pickOnDrag)
        {
            Destroy(GetComponent<Collider2D>());
            StartCoroutine(MoveObject());
            Doplganger.SetActive(true);
        }
    }

    private IEnumerator MoveObject()
    {
        isMoving = true;
        transform.SetParent(null);
        Vector3 startPos = transform.position;
        Vector3 endPosition = transform.position;
        endPosition.y -= 50f;
        startPos.z -= 1.1f;

        Vector3 peak = transform.position;
        peak.y += 5f;

        bool pik = false;
        float startTime = Time.time;
        float journeyDuration = Mathf.Abs((peak.y - startPos.y) + (peak.y - endPosition.y)) / moveSpeed;

        while (Time.time - startTime < journeyDuration)
        {
            float t = (Time.time - startTime) / journeyDuration;
            float curveValue = 20 * Mathf.Pow(t, 2) + 1;
            if (!pik)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed * curveValue * Time.deltaTime, startPos.z);
                if (transform.position.y > peak.y) pik = true;
            }
            else 
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * curveValue * Time.deltaTime, startPos.z);
            }

            yield return null;
        }

        isMoving = false;
        Destroy(gameObject);
    }
}
