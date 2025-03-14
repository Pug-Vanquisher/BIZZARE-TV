using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debugpenis : MonoBehaviour
{
    public static Dictionary<string, string> lines = new Dictionary<string, string>();
    public TMP_Text text;
    void Update()
    {
        text.text = "";
        foreach(KeyValuePair<string, string> pair in lines)
        {
            text.text += pair.Key + " : " + pair.Value;
        }
    }
    public static void line(string key, string value)
    {
        lines[key] = value;
    }
}
