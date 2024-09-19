using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class FryMince : MonoBehaviour
{
    public GameObject mince, oil;
    public Transform sliderSpawnPoint;
    public Sprite[] sprites;
    public Temperature tem;
    public Transform finalMinceSprites;
    [HideInInspector]
    public bool isFrying = false, isStirred = true, enoughOil = false;
    float cookingDelay, minimalTemp, maximalTemp, medianTemp, curColorLerpStep;
    SpriteRenderer minceSprite, finalMinceSprite;
    Slider sl;
    int curSpriteNum = 0, prevSpriteNum = 0;
    void Start()
    {
        minceSprite = mince.GetComponent<SpriteRenderer>();
        Invoke("SetAmounts", 0.1f);
    }

    void Update()
    {
        if (tem.TempNow > 60) cookingDelay = Mathf.Abs(1 - tem.KoefTime) / 10;
        else cookingDelay = 10;
        if (!isFrying && mince.activeSelf && sl == null)
        {
            isFrying = true;
            StartCooking();
        }
        if (!enoughOil) if (Cook.IsThereEnoughProduct(oil)) enoughOil = true;
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) FinishCooking();
    }

    void SetAmounts()
    {
        minimalTemp = Cook.GetNeededAmount(gameObject)[0];
        maximalTemp = Cook.GetNeededAmount(gameObject)[1];
        medianTemp = (minimalTemp + maximalTemp) / 2;
        curColorLerpStep = 1 / medianTemp;
    }

    public void StartCooking()
    {
        if (sl == null) sl = SliderControl.createSlider(gameObject, sliderSpawnPoint.position, true, true, true);
        Invoke("SliderFill", 0.5f);
    }

    public void SliderFill()
    {
        if (sl != null && sl.value != sl.maxValue)
        {
            if (isStirred) sl.value++;

            curSpriteNum = Mathf.FloorToInt(sl.value / Mathf.Ceil(minimalTemp / (sprites.Length - 1)));

            if (curSpriteNum < sprites.Length)
            {
                if (prevSpriteNum < curSpriteNum)
                {
                    isStirred = false;
                    prevSpriteNum = curSpriteNum;
                }
                else if (isStirred) 
                {
                    minceSprite.sprite = sprites[curSpriteNum];
                    minceSprite.color = Color.white;
                    curColorLerpStep = 0;
                }
                else if (!isStirred)
                {
                    minceSprite.color = Color.Lerp(Color.white, new Color(0.4f, 0.2f, 0, 1), curColorLerpStep);
                    curColorLerpStep += 1 / medianTemp;
                }
            }
            else if (sl.value > medianTemp)
            {
                minceSprite.color = Color.Lerp(Color.white, new Color(0.4f, 0.2f, 0, 1), curColorLerpStep);
                curColorLerpStep += 1 / medianTemp;
            }
            
            Invoke("SliderFill", cookingDelay);
        }
    }

    public void FinishCooking()
    {
        StopAllCoroutines();
        CancelInvoke();
        Cook.AddScoreForObject(gameObject, sl.value < Cook.GetNeededAmount(gameObject)[0] ? 500 : sl.value, true);
        SliderControl.destroySlider();
        foreach (Transform finalMince in finalMinceSprites)
        {
            finalMinceSprite = finalMince.GetComponent<SpriteRenderer>();
            finalMinceSprite.sprite = minceSprite.sprite;
            finalMince.GetComponent<pickableObject>().initialColor = minceSprite.color;
            finalMinceSprite.color = minceSprite.color;
        }
    }
}
