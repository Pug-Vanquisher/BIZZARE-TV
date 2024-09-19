using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantMan : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private moleGenerate molgen;
    private void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0))
        {
                if (gameManager.score > 100)
                {
                    gameManager.score -= 100;
                }
            
        }       

    }
}
