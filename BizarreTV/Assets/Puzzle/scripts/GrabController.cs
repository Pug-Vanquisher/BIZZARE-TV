using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    private GameObject selectedObj;

    [SerializeField]
    private float offsetByGround;
    public AudioSource audioSource;
    [SerializeField]
    private Transform marker;

    private Vector3 delta;

    private void Update()
    {

        //луч засекающий предметы, которые можно перетаскивать
        RaycastHit dragableHit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out dragableHit, 50f, LayerMask.GetMask("dragable") | LayerMask.GetMask("dragAnSize"));

        //луч засекающий поверхности, на которые можно ставить предметы
        RaycastHit groundHit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out groundHit, 50f);

        //проверка щелчка лкм
        if (Input.GetMouseButtonDown(0))
        {
            //если пока ничего не перетаскивается
            if (selectedObj == null)
            {
                //и мышь на перетаскиваемом предмете, то захватываем его
                if (dragableHit.collider)
                {
                    dragableHit.rigidbody.isKinematic = true;
                    selectedObj = dragableHit.collider.gameObject;
                    selectedObj.transform.rotation = Quaternion.Euler(0, 0, 0); //позже заменить на начальный вектор поворота объекта, т.к. он может быть ненулевым
                    audioSource.Play();
                    marker.gameObject.SetActive(true);
                    delta = selectedObj.transform.position - groundHit.point;
                    //dragableHit.collider.enabled = false;
                }

            }
            //если что-то уже перетаскивается, то отпускаем его
            else
            {
                selectedObj.GetComponent<Rigidbody>().isKinematic = false;
               // selectedObj.GetComponent<Collider>().enabled = true;
                marker.gameObject.SetActive(false);
                audioSource.Play();
                selectedObj = null;
            }
        }

        //если захвачен предмет, перемещаем его относительно точки падения луча на поверхность
        if (selectedObj)
        {
            selectedObj.transform.position = new Vector3(groundHit.point.x + delta.x, groundHit.point.y + offsetByGround+ selectedObj.transform.localScale.y/2f, groundHit.point.z + delta.z);

            RaycastHit hit;
            Physics.Raycast(selectedObj.transform.position, Vector3.down, out hit, 50f);

            marker.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }



    }


}
