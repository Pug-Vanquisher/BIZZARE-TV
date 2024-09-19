using System.Collections;
using UnityEngine;

public class TransparencyOnMouseOver : MonoBehaviour
{
    SpriteRenderer sr;
    Color basicColor;
    RaycastHit2D[] hits;
    GameObject go;
    pickableObject po;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        basicColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = new Color(basicColor.r, basicColor.g, basicColor.b, 0.5f);
    }

    private void OnMouseExit()
    {
        sr.color = new Color(basicColor.r, basicColor.g, basicColor.b, 1f);
    }

    private IEnumerator OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hits.Length > 1)
            {
                if (hits[1].collider.gameObject.TryGetComponent<pickableObject>(out po))
                {
                    go = hits[1].collider.gameObject;
                    if (PickUpAndFollowCoursor.IsHoldingObject(go) && po.isTouchingGoal)
                    {
                        po.isPickedUp = false;
                        PickUpAndFollowCoursor.Drop();
                    }
                    else if (!PickUpAndFollowCoursor.IsHoldingObject())
                    {
                        PickUpAndFollowCoursor.PickUp(go);
                        yield return new WaitForSeconds(.1f);
                        po.isPickedUp = true;
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hits.Length > 1)
            {
                if (hits[1].collider.gameObject.TryGetComponent<pickableObject>(out po))
                {
                    go = hits[1].collider.gameObject;
                    if (PickUpAndFollowCoursor.IsHoldingObject(go) && po.isTouchingGoal)
                        po.PutBack();
                }
            }
        }
    }
}
