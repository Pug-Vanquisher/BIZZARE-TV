using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(2)) // Проверяем нажатие колесика мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем, пересек ли луч какой-либо объект
            if (Physics.Raycast(ray, out hit, 50f, LayerMask.GetMask("sizeble") | LayerMask.GetMask("dragAnSize")))
            {
                // Получаем компонент Transform объекта, на который указывает луч
                Transform hitTransform = hit.transform;


                //!!дублирываение проверки на возможность изменения размера, можно оставить только слои
                // Проверяем, принадлежит ли объект к слою, который вы хотите изменить
                // размер (при необходимости добавьте или измените слои)
                if (hitTransform.GetComponent<ResizeObject>())
                {
                    hitTransform.GetComponent<ResizeObject>().Resize();
                }
            }
        }
    }
}
