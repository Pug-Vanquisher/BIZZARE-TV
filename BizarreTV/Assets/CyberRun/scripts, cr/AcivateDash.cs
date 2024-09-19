using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcivateDash : MonoBehaviour
{
    public GameObject player;
    public GameObject DashIcon;

    private void Start()
    {
        DashIcon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Dashing dashingScript = player.GetComponent<Dashing>();
            DashIcon.SetActive(true);
            if (dashingScript != null)
            {
                dashingScript.enabled = true;
            }
        }
    }
}