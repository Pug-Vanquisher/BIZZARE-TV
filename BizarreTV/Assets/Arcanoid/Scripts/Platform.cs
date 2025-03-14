using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 100f)]
    public float speed = 20f;
    public Vector2 currspeed;
    [SerializeField]
    private float leftBorder = -7.9f;
    [SerializeField]
    private float rightBorder = 7.9f;

    bool isPlayable = false;

    void Update()
    {
        if (isPlayable)
        {
            //currspeed = speed * Vector2.right * Input.GetAxis("Horizontal");
            currspeed = Vector2.Lerp(currspeed, Input.GetAxis("Horizontal") * speed * Vector2.right, 0.01f);
            transform.Translate(currspeed * Time.deltaTime);
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftBorder, rightBorder), transform.position.y);
        }

    } 

    public void StopBePlayable()
    {
        isPlayable = false;
    }
    public void StartBePlayable()
    {
        isPlayable = true;
    }
}

