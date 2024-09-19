using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComeToHome : MonoBehaviour
{
    public GameObject ComeTo;
    float moveSpeed = 20.0f;
    [SerializeField] GameObject package;
    [SerializeField] bool mince = false, pickOnDrag = false;
    [SerializeField] float notComeCloseFor = 0.0f;
    Coroutine c;
    bool turnedOnSomething = false;
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0) && c == null && !EventSystem.current.IsPointerOverGameObject() && pickOnDrag)
        {
            if (mince)
            {
                if (!ComeTo.activeSelf) Cook.ShowText("Сначала нужно найти сковородку.", false);
                else if (!ComeTo.GetComponent<FryMince>().enoughOil) Cook.ShowText("Недостаточно масла", false);
                else c = StartCoroutine(MoveObject());
            }
            else
            {
                if (!ComeTo.activeSelf) Cook.ShowText("Некуда положить", false);
                else c = StartCoroutine(MoveObject());
            }
        }
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0) && c == null && !EventSystem.current.IsPointerOverGameObject() && !pickOnDrag)
        {
            if (mince)
            {
                if (!ComeTo.activeSelf) Cook.ShowText("Сначала нужно найти сковородку.", false);
                else if (!ComeTo.GetComponent<FryMince>().enoughOil) Cook.ShowText("Недостаточно масла", false);
                else c = StartCoroutine(MoveObject());
            }
            else
            {
                if (!ComeTo.activeSelf) Cook.ShowText("Некуда положить", false);
                else c = StartCoroutine(MoveObject());
            }
        }
    }
    private IEnumerator MoveObject()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -4.1f);
        if (mince) Destroy(package);

        Vector3 startPos = transform.position;
        Vector3 endPosition = new(ComeTo.transform.position.x - notComeCloseFor, ComeTo.transform.position.y, -4.1f);

        float journeyLength = Vector3.Distance(startPos, endPosition);
        float startTime = Time.time;

        while (transform.position != endPosition)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, endPosition, fracJourney);
            yield return null;
        }

        for (int i = 0; i < ComeTo.transform.childCount; i++)
        {
            GameObject child = ComeTo.transform.GetChild(i).gameObject;
            if (!child.activeSelf && !turnedOnSomething)
            {
                child.SetActive(true);
                turnedOnSomething = true;
            }
            if (child.activeSelf && i == ComeTo.transform.childCount - 2)
            {
                if (ComeTo.TryGetComponent<PickOnClick>(out PickOnClick poc)) poc.isEnabled = true;
                if (ComeTo.TryGetComponent<GetScoreOnClick>(out GetScoreOnClick gsoc)) gsoc.isEnabled = true;
            }
        }
        Destroy(gameObject);
    }
}