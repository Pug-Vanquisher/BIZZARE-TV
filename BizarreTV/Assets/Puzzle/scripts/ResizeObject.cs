using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObject : MonoBehaviour
{

    private int currentSizeIndex = 0; // Индекс текущего размера
    [SerializeField]
    private int sceneSizeStageInd;//начальное состояние размера на сцене
    [SerializeField]
    private int startSizeStageInd;//состояние на начало игры

    private Vector3 smallestSize; // Наименьший размер объекта
    [SerializeField]
    private float[] sizes; // Массив коэффициентов из возможных размеров объекта

    [SerializeField]
    private float[] weights;


    [SerializeField]
    private float resizeTime;

    public bool changeWeight;

    private Rigidbody rb;

    public AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Сохраняем изначальный размер объекта
        smallestSize = transform.localScale / sizes[sceneSizeStageInd];// Инициализируем массив размеров, начиная с изначального размера
        currentSizeIndex = startSizeStageInd;
        transform.localScale = smallestSize * sizes[startSizeStageInd];


        for (int i = 0; i < sizes.Length; i++)
        {
            if (sizes[i] == 0)
            {
                sizes[i] = 1;
            }
        }

        if (changeWeight)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] == 0)
                {
                    weights[i] = 1;
                }
            }
            rb.mass = weights[currentSizeIndex];
        }

        if (resizeTime == 0)
        {
            resizeTime = 0.05f;
        }



    }

    public void Resize()
    {
        // Увеличиваем индекс размера (циклически)
        currentSizeIndex = (currentSizeIndex + 1) % sizes.Length;

        if (changeWeight)
        {
            //меняем вес
            rb.mass = weights[currentSizeIndex];
        }
        // Меняем размер объекта
        StopAllCoroutines();
        StartCoroutine("SlightlyyResize");
        audioSource.Play();
        //transform.localScale = smallestSize * sizes[currentSizeIndex];
    }


    IEnumerator SlightlyyResize()
    {
        Vector3 crntLocalScale = transform.localScale;
        float crntTime = 0f;
        while (crntTime < resizeTime)
        {
            transform.localScale = Vector3.Lerp(crntLocalScale, smallestSize * sizes[currentSizeIndex], crntTime / resizeTime);
            crntTime += Time.deltaTime;
            yield return null;
        }


        yield return null;
    }

    public float GetWeight()
    {
        return rb.mass;
    }

}