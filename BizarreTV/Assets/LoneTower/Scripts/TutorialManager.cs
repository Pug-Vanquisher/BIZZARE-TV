using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] PopUps;
    private int index;
    private NeedVal znach;

    private void Start()
    {
        znach = JsonUtility.FromJson<NeedVal>(File.ReadAllText("Assets/LoneTower/StreamingAssets/need.json"));
    }

    private void Update()
    {
        if (znach.Need == 0)
        {
            Time.timeScale = 0;
            for (int i = 0; i < PopUps.Length; i++)
            {
                if (i == index)
                {
                    PopUps[i].SetActive(true);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                PopUps[index].SetActive(false);
                index++;
                Debug.Log(index);
            }

            if (index == PopUps.Length)
            {
                Time.timeScale = 1;
                znach.Need = 1;
                File.WriteAllText(Application.streamingAssetsPath + "/need.json", JsonUtility.ToJson(znach));
            }
        }
        
    }

    public class NeedVal
    {
        public int Need;
    }

}
