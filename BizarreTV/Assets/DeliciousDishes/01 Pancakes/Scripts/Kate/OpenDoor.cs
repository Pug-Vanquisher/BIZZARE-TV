using UnityEngine.EventSystems;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Hided;
    public GameObject Open;
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) ToggleDoorState();
    }

    private void ToggleDoorState()
    {
        if (GetComponent<AudioSource>() != null) {
            if (GetComponent<AudioSource>().enabled == true) GetComponent<AudioSource>().Play();
            else 
            {
                Hided.GetComponent<AudioSource>().enabled = true;
                Open.GetComponent<AudioSource>().enabled = true;
            }
        }
        Hided.SetActive(false);
        Open.SetActive(true);
    }
}
