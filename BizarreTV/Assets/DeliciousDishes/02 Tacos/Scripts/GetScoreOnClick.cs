using UnityEngine.EventSystems;
using UnityEngine;

public class GetScoreOnClick : MonoBehaviour
{
    public bool isEnabled = false;
    public float amount = 1;
    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && isEnabled && Input.GetMouseButton(0)) Cook.AddScoreForObject(gameObject, amount);
    }
}
