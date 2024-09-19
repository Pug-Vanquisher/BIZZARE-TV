using System.Collections.Generic;
using UnityEngine;

public class addLiquid : MonoBehaviour
{
    public GameObject liquidObject;
    public GameObject liquidEndPoint;
    public Color liquidColor;
    public float liquidMoveSpeed = 0.01f, colorLerpStep = 0.01f;
    Vector3 liquidEndPointPos;
    List <SpriteRenderer> liquidSprites = new();
    float lerpStep;

    void Start()
    {
        liquidEndPointPos = liquidEndPoint.transform.position;
        foreach (SpriteRenderer t in liquidObject.GetComponentsInChildren<SpriteRenderer>()) {
            liquidSprites.Add(t);
        }
        lerpStep = colorLerpStep;
    }

    public void liquidUp() {
        if (!GetComponent<AudioSource>().isPlaying && GetComponent<AudioSource>() != null) {
            if (GetComponent<AudioSource>().enabled == true) GetComponent<AudioSource>().Play();
            else GetComponent<AudioSource>().enabled = true;
        }
        if (liquidObject.transform.position != liquidEndPointPos) liquidObject.transform.position = Vector3.MoveTowards(liquidObject.transform.position, liquidEndPointPos, liquidMoveSpeed);
        foreach (SpriteRenderer sr in liquidSprites) sr.color = Color.Lerp(sr.color, liquidColor, lerpStep);
        lerpStep += colorLerpStep;
    }

    public void resetStep() {
        lerpStep = colorLerpStep;
    }
}
