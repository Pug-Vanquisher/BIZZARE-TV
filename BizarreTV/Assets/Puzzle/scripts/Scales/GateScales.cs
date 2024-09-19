using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScales : WeightScale
{
    [SerializeField]
    private Transform gates;

    private Vector3 startPos;
    private Vector3 nextPos;
    [SerializeField]
    private Vector3 axesAble;

    [SerializeField]
    private float liftMultiplier=1f;

    private void Start()
    {
        startPos = gates.localPosition;
        axesAble.Normalize();
    }

    protected override void UpdateWeight()
    {
        base.UpdateWeight();

        nextPos = new Vector3(startPos.x + calculatedMass * liftMultiplier * axesAble.x, startPos.y + calculatedMass* liftMultiplier* axesAble.y, startPos.z + calculatedMass * liftMultiplier * axesAble.z);
        StopAllCoroutines();
        StartCoroutine("Translate");
    }

    [SerializeField]
    float translateTime = 0.1f;

    IEnumerator Translate()
    {
        Vector3 crntPos = gates.localPosition;
        float crntTime = 0f;
        while (crntTime < translateTime)
        {
            gates.localPosition = Vector3.Lerp(crntPos, nextPos, crntTime / translateTime);
            crntTime += Time.deltaTime;
            yield return null;
        }


        yield return null;
    }

}
