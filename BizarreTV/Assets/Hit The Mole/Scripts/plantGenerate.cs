using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantGenerate : MonoBehaviour
{
    [SerializeField] private List<Sprite> plant;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject poison;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] AudioSource audioSourceNeed;

    public float showDuration = 0.5f;
    public float timeGrow = 0f;
    public float timeAttack = 0f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    // Plant Parameters
    float timerGrown = 10.0f;
    float timerDeath = 5f;
    float timerAttack;
    private int growState = 0;
    private bool attack = false;
    private bool placed = false;
    private int plantIndex = 0;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = plant[0];
        spriteRenderer.enabled = false;
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = true;
        timerAttack = Random.Range(5f, 15f); 
        poison.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Update()
    {
        if (placed && growState == 3)
            return;

        if (placed && growState != 3)
        {
            timeGrow += Time.deltaTime;
            if (timeGrow >= timerGrown && growState != 3)
            {
                Grow();
                timeGrow = 0f;
            }
        }

        if (placed && !attack)
        {
            timeAttack += Time.deltaTime;
            if (timeAttack >= timerAttack)
            {
                attack = true;
                timeAttack = 0f;
                StartCoroutine(Attack());
            }
        }
    }

    public void PlacePlant()
    {
        placed = true;
        spriteRenderer.enabled = true;
    }

    private IEnumerator Attack()
    {
        float deathTime = 0f;
        float colorTimer = 0.5f;
        float colorTime = 0f;
        audioSourceNeed.clip = audioClips[0];
        audioSourceNeed.Play();
        while (deathTime < timerDeath)
        {
            colorTime += Time.deltaTime;
            deathTime += Time.deltaTime;
            if (colorTime >= colorTimer)
            {
                if(spriteRenderer.flipX == false)
                {
                    spriteRenderer.flipX = true;
                    yield return new WaitForSeconds(0.5f);
                    deathTime += 0.5f;
                }
                else
                {
                    spriteRenderer.flipX = false;
                    yield return new WaitForSeconds(0.5f);
                    deathTime += 0.5f;
                }
            }
            yield return null;
        }
        //yield return new WaitForSeconds(0.25f);
        attack = false;
        placed = false;
        growState = 0;
        audioSourceNeed.clip = audioClips[2];
        audioSourceNeed.Play();
        spriteRenderer.sprite = plant[0];
        spriteRenderer.enabled = false;
        timerAttack = Random.Range(5f, 30f);
        gameManager.SubtractScore(plantIndex, false);
    }

    private void Grow()
    {
        growState++;
        spriteRenderer.sprite = plant[growState];
    }

    //private IEnumerator GrownPlant(float setTime)
    //{
    //    while (grown)
    //    {
    //        Debug.Log(grown);
    //        float curTime1 = timerGrown;
    //        float curTime2 = setTime;
    //        while (curTime1 >= 0)
    //        {
    //            curTime1 -= Time.deltaTime;
    //        }
    //        Debug.Log(curTime1);
    //        yield return new WaitForSeconds(duration * 2);
    //        if (curTime1 <= 0)
    //        {
    //            attack = true;
    //        }
    //        Debug.Log(attack);
    //        while (attack && (curTime2 > 0))
    //        {
    //            plant.GetComponent<SpriteRenderer>().color = Color.black;
    //            curTime2 -= Time.deltaTime;
    //        }
    //        yield return new WaitForSeconds(duration * 2);
    //        Debug.Log(curTime2);
    //        if (attack && curTime2 <= 0)
    //        {
    //            Debug.Log("st");
    //            plant.SetActive(false);
    //            grown = false;
    //            attack = false;
    //            StopAllCoroutines();
    //            Activate(2);
    //            Debug.Log("fn");
    //        }
    //        else
    //        {
    //            GetComponent<SpriteRenderer>().color = Color.green;
    //            curTime2 = setTime;
    //            curTime1 = timerGrown;
    //            attack = false;
    //        }
    //        yield return null;
    //    }
    //}

    private IEnumerator poisonTake()
    {
        poison.GetComponent<SpriteRenderer>().enabled = true;
        audioSourceNeed.clip = audioClips[1];
        audioSourceNeed.Play();

        float elapsed = 0f;
        Quaternion start = poison.transform.rotation;
        Quaternion end = Quaternion.AngleAxis(60f, new Vector3(0f, 0f, 1f));
        while (elapsed < showDuration)
        {
            poison.transform.rotation = Quaternion.Lerp(start, end, elapsed / showDuration + 0.5f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        poison.GetComponent<SpriteRenderer>().enabled = false;
        poison.transform.rotation = start;
        yield return new WaitForSeconds(0.25f);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!gameManager.CheckIsMoleCurrent(plantIndex) && gameManager.score >= 100 && !placed)
            {
                gameManager.SubtractScore(plantIndex, true);
                StopAllCoroutines();
                PlacePlant();
                return;
            }

            if (placed && attack)
            {
                StopAllCoroutines();
                StartCoroutine(poisonTake());   
                attack = false;
                return;
            }
        }
    }

    public void SetIndex(int index)
    {
        plantIndex = index;
    }

    public int GetGrowState()
    {
        return growState;
    }

    public bool GetPlacedState()
    {
        return placed;
    }

    public void ChangeBoxCollider2DState()
    {
        boxCollider2D.enabled = !boxCollider2D.enabled;
    }

    public void ChangeBoxCollider2DState(bool state)
    {
        boxCollider2D.enabled = state;
    }
}
