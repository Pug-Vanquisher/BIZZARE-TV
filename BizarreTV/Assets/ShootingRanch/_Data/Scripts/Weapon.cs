using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Image imageToChange;
    private Gener Values;

    public int FLAG = 0;
    public int Counter = 0;
    public int Kounter = 0;

    public float fireRate = 1f;
    public float range = 500f;
    public float force = 150f;
    // public ParticleSystem muzzleFlash;
    // public Transform bulletSpawn;

    public AudioClip shotSFX;
    public AudioSource _audioSource;
    public Camera _cam;

    public float plus = 1;
    public float timeStart = 60;
    public Text timerText;
    public float recStart = 0;
    public Text recText;
  
    public Sprite weap1;
    public Sprite weap2;
    public Sprite weap3;
    public Sprite weap4;
    public Sprite weap5;

    public GameObject WatchSmaLL;
    public GameObject WatchBiG;
    public GameObject WatchEnD;

    public GameObject WSmaLL;
    public GameObject WBiG;
    public GameObject WEnD;

    public Material materialSmaLL;
    public Material materialBiG;
    public Material materialEnD;

    public Renderer renderSmaLL;
    public Renderer renderBiG;
    public Renderer renderEnD;

     public Transform target;
    

    private void OnDestroy()
    {
        // Debug.Log("Записал");
        Statics.recorD = recStart;
    }

    void Start()
    {
        timerText.text = timeStart.ToString();
        recText.text = recStart.ToString();

        WSmaLL = Instantiate(WatchSmaLL);
        WBiG = Instantiate(WatchBiG);
        WEnD = Instantiate(WatchEnD);

        renderSmaLL = WSmaLL.GetComponent<Renderer>();
        materialSmaLL = renderSmaLL.material;
        renderBiG = WBiG.GetComponent<Renderer>();
        materialBiG = renderBiG.material;
        renderEnD = WEnD.GetComponent<Renderer>();
        materialEnD = renderEnD.material;

    }

    private IEnumerator MoveObject()
    {
        while (true)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-2f, 1.5f), Random.Range(5f, 7f), 9f);
            target.position = randomPosition;
            if (Kounter >= 7)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else {
                yield return new WaitForSeconds(1f);
            }
            
        }
    }

    IEnumerator Stopper()
    {
        yield return new WaitForSeconds(0.5f);
        Values.fLag = 0;
    }

    void  Update()
    {
        Values = GetComponent<Gener>();
        if (timeStart > 0)
        {
            timeStart -= Time.deltaTime;
            timerText.text = Mathf.Round(timeStart).ToString();
        }
        if (timeStart < 0)
        {
            plus = 0;
            timeStart = 0;
            StartCoroutine(MoveObject());
            
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            // StartCoroutine(ChangeImageSprite());
            // ChangeImageSprite();
        }
    }

//    IEnumerator ChangeImageSprite()
//     {
//         imageToChange.sprite = weap2;    
//         yield return new WaitForSeconds(0.1f);
//         imageToChange.sprite = weap3;    
//         yield return new WaitForSeconds(0.1f);
//         imageToChange.sprite = weap4;    
//         yield return new WaitForSeconds(0.1f);
//         imageToChange.sprite = weap5;    
//         yield return new WaitForSeconds(0.2f);
//         imageToChange.sprite = weap1;    
//     }
    
    void Shoot()
    {
        _audioSource.PlayOneShot(shotSFX);
        RaycastHit hit;
        //muzzleFlash.Play();
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, range))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            
            if(hit.rigidbody !=null)
            {
                Material material = renderer.material;
               
                hit.rigidbody.AddForce(-hit.normal*force);
                if (material.name.Equals(materialSmaLL.name))
                {
                    recStart += plus*2;
                    recText.text = recStart.ToString();
                    Counter += 1;

                }
                if (material.name.Equals(materialBiG.name))
                {
                    recStart += plus;
                    recText.text = recStart.ToString();
                    Counter += 1;
                }
                if (material.name.Equals(materialEnD.name))
                {
                    recStart += 5f;
                    recText.text = recStart.ToString();
                    Kounter += 1;
                }

                if (Counter >= 12 && timeStart > 0)
                {
                    Counter = 0;
                    StartCoroutine(Stopper());
                }
                if (Kounter >= 10)
                {
                    SceneManager.LoadScene("Meny");

                }
            }
        }
    }
}

