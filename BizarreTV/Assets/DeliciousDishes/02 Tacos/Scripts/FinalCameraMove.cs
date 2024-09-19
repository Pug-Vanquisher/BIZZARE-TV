using System.Collections;
using Cinemachine;
using UnityEngine;

public class FinalCameraMove : MonoBehaviour
{
    
    public CinemachineVirtualCamera vcam1;
    public Transform FinalPos;
    public GameObject saveBtn;
    bool final = false;
    void FixedUpdate()
    {
        if (Cook.AllStepsFinished() && !final)
        {
            final = true;
            StartCoroutine(MoveToFinalPos());
        }
    }

    IEnumerator MoveToFinalPos()
    {
        yield return new WaitForSeconds(2);
        vcam1.Follow = FinalPos;
        yield return new WaitForSeconds(1);
        saveBtn.SetActive(true);
    }
}
