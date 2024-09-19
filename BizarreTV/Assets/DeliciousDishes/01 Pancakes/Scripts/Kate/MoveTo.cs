using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
using Cinemachine;

public class MoveTo : MonoBehaviour
{
    public GameObject Shadow;
    public CinemachineVirtualCamera vcam1;
    public GameObject ObjectToMove;
    Vector3 StartPosition;
    // bool Over = false;
    public float moveSpeed = 20f;
    void Start()
    {
        StartPosition = Shadow.transform.position;
    }
    // void Update()
    // {
        // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        
        // if (hit.collider != null && hit.collider == this.GetComponent<Collider2D>()) Over = true;
        // else Over = false;
        // Vector3 targetPosition = Over ? transform.position : StartPosition;
        // Vector3 direction = (targetPosition - Shadow.transform.position).normalized;
        // Shadow.transform.position += direction * moveSpeed * Time.deltaTime; 
    // }
    private void OnMouseDown()
    {
        // чтобы не переходил при клике на кнопки ui
        if (!EventSystem.current.IsPointerOverGameObject()) vcam1.m_Follow = ObjectToMove.transform;
    }

    private void OnMouseOver() {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            StopAllCoroutines();
            StartCoroutine("Move", true);
        }

    }

    private void OnMouseExit() {
        StopAllCoroutines();
        StartCoroutine("Move", false);
    }

    private IEnumerator Move(bool enter) {
        if (enter) {
            Shadow.transform.position = Vector3.MoveTowards(Shadow.transform.position, transform.position, moveSpeed);
            if (Shadow.transform.position != transform.position) {
                yield return new WaitForSeconds(0.3f);
                yield return StartCoroutine("Move", true);
            }
        }
        else {
            Shadow.transform.position = Vector3.MoveTowards(Shadow.transform.position, StartPosition, moveSpeed);
            if (Shadow.transform.position != StartPosition) {
                yield return new WaitForSeconds(0.01f);
                yield return StartCoroutine("Move", false);
            }
        }
    }
}