using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Threefireball : MonoBehaviour
{
    [SerializeField] public bool SetActive;
    [SerializeField] private float bps;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform startPoint;
    //[SerializeField] private float bulletSpeed = 5f;

    [SerializeField] private Transform[] firstPoints;
    [SerializeField] private Transform[] secondPoints;

    private float TimeUntilFire;
    private int schet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeUntilFire += Time.deltaTime;
        if ((TimeUntilFire >= 1f / bps) && (SetActive == true))
        {
            TimeUntilFire = 0f;
            schet += 1;
            GameObject ball1 = Instantiate(fireballPrefab, startPoint.position, Quaternion.identity);
            GameObject ball2 = Instantiate(fireballPrefab, startPoint.position, Quaternion.identity);
            GameObject ball3 = Instantiate(fireballPrefab, startPoint.position, Quaternion.identity);
            if (schet % 2 == 1)
            {
                
            }
            else
            {

            }
            StartCoroutine(DestroyAfterSeconds(ball1));
            StartCoroutine(DestroyAfterSeconds(ball2));
            StartCoroutine(DestroyAfterSeconds(ball3));
        }
    }

    IEnumerator DestroyAfterSeconds(GameObject go)
    {
        yield return new WaitForSeconds(5f);
        Object.Destroy(go);
    }
}
