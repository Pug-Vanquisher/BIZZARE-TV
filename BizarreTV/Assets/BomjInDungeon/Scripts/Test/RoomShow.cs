using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomShow : MonoBehaviour
{
    public int dangerous;
    public TMP_Text dngrs;

    void Start()
    {
        dngrs.text = dangerous.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
