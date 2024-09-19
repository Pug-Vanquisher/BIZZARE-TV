using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;

public class boxWithPickableObjects : MonoBehaviour
{
    public GameObject[] spawnPrefabs;
    public GameObject goalObject;
    GameObject spawnedObject;
    pickableObject po;
    
    private IEnumerator OnMouseDown() {
        if (!PickUpAndFollowCoursor.IsHoldingObject() && !EventSystem.current.IsPointerOverGameObject()) {
            spawnedObject = Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)], Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);
            spawnedObject.transform.position = new Vector3(spawnedObject.transform.position.x, spawnedObject.transform.position.y, -0.1f);

            if (spawnedObject.TryGetComponent<pickableObject>(out po))
            {
                PickUpAndFollowCoursor.PickUp(po.gameObject);
                po.goalObject = goalObject;
                po.homeObject = gameObject;
                yield return new WaitForSeconds(.25f);
                po.isPickedUp = true;
            }
            else
            {
                po = spawnedObject.GetComponentInChildren<pickableObject>();
                if (po != null)
                {
                    PickUpAndFollowCoursor.PickUp(po.gameObject);
                    po.goalObject = goalObject;
                    po.homeObject = gameObject;
                    yield return new WaitForSeconds(.25f);
                    po.isPickedUp = true;
                }
            }
        }
    }
}
