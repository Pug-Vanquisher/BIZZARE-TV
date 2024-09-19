using UnityEngine;

public class Clean : MonoBehaviour
{
    public GameObject objectToCollideWith10;
    public GameObject objectToCollideWith1;
    public GameObject objectToCollideWith5;
    public GameObject objectToCollideWith6;
    public GameObject objectToCollideWith7;
    public GameObject Cleaner;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == objectToCollideWith10)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject == objectToCollideWith1)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject == objectToCollideWith5)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject == objectToCollideWith6)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject == objectToCollideWith7)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject == Cleaner)
        {
            Destroy(gameObject);
        }
    }
}