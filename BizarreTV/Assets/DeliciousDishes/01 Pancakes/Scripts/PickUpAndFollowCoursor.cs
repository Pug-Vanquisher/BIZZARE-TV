using System.Collections;
using UnityEngine;

public class PickUpAndFollowCoursor : MonoBehaviour
{
    GameObject pickedObj;
    static PickUpAndFollowCoursor instance;
    Vector3 mousePos, objPos, tempPos;
    float zChange = -2;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        mousePos = Input.mousePosition;
    }

    public static void PickUp(GameObject obj, bool moveToCursorInstantly = true)
    {
        if (!IsHoldingObject())
        {
            if (instance.GetComponent<AudioSource>() != null)
            {
                if (instance.GetComponent<AudioSource>().enabled == true) instance.GetComponent<AudioSource>().Play();
                else instance.GetComponent<AudioSource>().enabled = true;
            }
            instance.pickedObj = obj;
            if (moveToCursorInstantly)
            {
                instance.objPos = instance.pickedObj.transform.position;
                instance.StartCoroutine("FollowMouse");
            }
        }
    }

    public static void Drop()
    {
        if (IsHoldingObject())
        {
            instance.StopAllCoroutines();
            instance.pickedObj.transform.position = new Vector3(instance.pickedObj.transform.position.x, instance.pickedObj.transform.position.y, instance.objPos.z);

            if (instance.pickedObj.CompareTag("Decoration"))
            {
                instance.pickedObj.transform.position = new Vector3(instance.pickedObj.transform.position.x, instance.pickedObj.transform.position.y, instance.zChange);
                instance.zChange -= 0.00001f;
            }
            instance.pickedObj = null;
        }
    }

    public static void PauseMove()
    {
        instance.StopCoroutine("FollowMouse");
    }

    public static void ResumeMove()
    {
        instance.StartCoroutine("FollowMouse");
    }

    private IEnumerator FollowMouse()
    {
        if (IsHoldingObject())
        {
            mousePos = Input.mousePosition;
            tempPos = Camera.main.ScreenToWorldPoint(mousePos);
            pickedObj.transform.position = new Vector3(tempPos.x, tempPos.y, -5);
        }
        yield return new WaitForEndOfFrame();
        yield return StartCoroutine("FollowMouse");
    }

    public static bool IsHoldingObject(GameObject go = null)
    {
        if (go == null) return instance.pickedObj != go;
        else return instance.pickedObj == go;
    }

}
