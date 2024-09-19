using UnityEngine;

public class RotateChildAndDieXD : MonoBehaviour
{
    Transform child;
    void Start()
    {
        child = transform.GetChild(0);
        child.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
    }

    private void Update()
    {
        if (child == null) Destroy(gameObject);
    }
}
