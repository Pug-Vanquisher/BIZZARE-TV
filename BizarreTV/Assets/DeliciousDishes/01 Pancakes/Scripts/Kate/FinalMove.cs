using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class FinalMove : MonoBehaviour
{
    public bool Finalmove = false;
    // bool final = false;
    public Transform FinalPos;
    public float moveSpeed = 2f;
    public CinemachineVirtualCamera vcam1;
    public GameObject saveBtn;

    // void Update()
    // {
    //     if (Finalmove)
    //     {
    //         if (!final)
    //         {
    //             StartCoroutine(MoveToFinalPos());
    //             //vcam1.Follow = transform;
    //             vcam1.Follow = FinalPos;
    //         }
    //         final = true;
    //     }
    // }

    private void OnMouseDown() {
        if (!PickUpAndFollowCoursor.IsHoldingObject() && !SliderControl.hasActiveSlider() && Finalmove && !EventSystem.current.IsPointerOverGameObject()) {
            StartCoroutine(MoveToFinalPos());
            vcam1.Follow = FinalPos;
            Finalmove = false;
        }
    }

    IEnumerator MoveToFinalPos()
    {
        while (Vector3.Distance(transform.position, FinalPos.position) > 0.3f)
        {
            transform.position = Vector3.Lerp(transform.position, FinalPos.position, Time.deltaTime * moveSpeed);
            yield return null;
        }

        transform.position = FinalPos.position;
        saveBtn.SetActive(true);
        
        yield break;
    }
}