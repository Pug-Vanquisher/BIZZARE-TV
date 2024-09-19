using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScales : WeightScale
{
    [SerializeField]
    private float neededWeight = 4f;

    private bool isChecking;
    private bool isEnoughWeight;

    private void Start()
    {
        isChecking = false;
        isEnoughWeight = false;
    }

    private void Update()
    {
        if (!isChecking)
        {
            if (!isEnoughWeight)
            {
                if (calculatedMass > (neededWeight - 0.5f))
                {
                    isChecking = true;
                    StartCoroutine(CheckingWeight(true));

                }
            }
            else if (isEnoughWeight)
            {
                if (calculatedMass < (neededWeight - 0.5f))
                {
                    isChecking = true;
                    StartCoroutine(CheckingWeight(false));
                }
            }
        }

    }

    IEnumerator CheckingWeight(bool isGreater)
    {
        yield return new WaitForSeconds(2f);
        if (isGreater)
        {
            if (calculatedMass > (neededWeight - 0.5f))
            {
                isEnoughWeight = true;
                Debug.Log("Удар " + calculatedMass);
            }
        }
        else
        {
            if (calculatedMass < (neededWeight - 0.5f))
            {
                isEnoughWeight = false;
                Debug.Log("Теперь недостаточно веса " + calculatedMass);
            }
        }

        isChecking = false;
    }

    protected override void UpdateWeight()
    {
        base.UpdateWeight();
    }

}
