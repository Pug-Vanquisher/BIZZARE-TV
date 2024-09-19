using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upd : MonoBehaviour
{
    private float reset = 0;
    public float val;
    public float S = 0;
    public Text resText;

    private void OnEnable()
    {
        val = Statics.recorD;
    }

    void Start()
    {
        if (val > S)
        {
            S = val;
            resText.text = S.ToString();
        }
    }

    public void ButtonRes()
    {
        resText.text = reset.ToString();
    }
}
