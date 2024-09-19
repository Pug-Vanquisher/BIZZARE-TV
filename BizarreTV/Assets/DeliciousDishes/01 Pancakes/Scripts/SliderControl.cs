using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    static SliderControl instance;
    public GameObject sliderPrefab;
    public GradientMode gradientMode;
    public Gradient sliderColors = new Gradient();
    GradientColorKey[] colorKeys;
    GradientAlphaKey[] a;
    GameObject createdSlider, canvas;
    Camera cam;
    Image img1;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        sliderColors.mode = gradientMode;

        a = new GradientAlphaKey[2];
        a[0] = new GradientAlphaKey(1f, 0);
        a[1] = new GradientAlphaKey(1f, 1f);

        cam = Camera.main;
    }

    void Update()
    {
        if (instance.createdSlider != null)
        {
            img1 = instance.createdSlider.GetComponentsInChildren<Image>()[1];
            img1.color = instance.sliderColors.Evaluate(img1.fillAmount);
        }
    }

    public static Slider createSlider(GameObject go, Vector3 spawnPoint = default, bool canBeOverdone = true, bool horizontal = true, bool noText = false)
    {
        if (instance.createdSlider != null) 
        {
            Cook.ShowText("Сначала завершите предыдущее дело", false);
            return null;
        }
        instance.createdSlider = Instantiate(instance.sliderPrefab, instance.canvas.transform);
        if (!horizontal)
        {
            instance.createdSlider.transform.Rotate(new Vector3(0, 0, 90));
            instance.createdSlider.GetComponentInChildren<TextMeshProUGUI>().transform.Rotate(new Vector3(0, 0, -90));
        }
        if (spawnPoint != default) instance.createdSlider.transform.position = instance.cam.WorldToScreenPoint(spawnPoint);
        if (noText) instance.createdSlider.GetComponentInChildren<TextMeshProUGUI>().text = "";

        float minimalAmount = Cook.GetNeededAmount(go)[0];
        float maximalAmount = Cook.GetNeededAmount(go)[1];
        float medianAmount = (minimalAmount + maximalAmount) / 2;

        instance.createdSlider.GetComponent<Slider>().maxValue = canBeOverdone ? medianAmount * 2 : minimalAmount;

        if (minimalAmount == 0f)
        {
            instance.colorKeys = new GradientColorKey[1];
            instance.colorKeys[0] = new GradientColorKey(Color.red, 0);
        }
        else if (instance.gradientMode == GradientMode.Fixed && canBeOverdone)
        {
            instance.colorKeys = new GradientColorKey[5];
            instance.colorKeys[0] = new GradientColorKey(Color.red, 0);
            instance.colorKeys[1] = new GradientColorKey(Color.red, minimalAmount / (medianAmount * 2));
            instance.colorKeys[2] = new GradientColorKey(Color.green, maximalAmount / (medianAmount * 2));
            instance.colorKeys[3] = new GradientColorKey(Color.yellow, medianAmount * 1.75f / (medianAmount * 2));
            instance.colorKeys[4] = new GradientColorKey(Color.red, 1);
        }
        else if (instance.gradientMode == GradientMode.Blend && canBeOverdone)
        {
            instance.colorKeys = new GradientColorKey[4];
            instance.colorKeys[0] = new GradientColorKey(Color.red, 0);
            instance.colorKeys[1] = new GradientColorKey(Color.green, medianAmount / (medianAmount * 2));
            instance.colorKeys[2] = new GradientColorKey(Color.yellow, medianAmount * 1.75f / (medianAmount * 2));
            instance.colorKeys[3] = new GradientColorKey(Color.red, 1);
        }
        else if (!canBeOverdone)
        {
            instance.colorKeys = new GradientColorKey[3];
            instance.colorKeys[0] = new GradientColorKey(Color.red, 0.5f);
            instance.colorKeys[2] = new GradientColorKey(Color.yellow, 0.99f);
            instance.colorKeys[1] = new GradientColorKey(Color.green, 1f);
        }

        instance.sliderColors.SetKeys(instance.colorKeys, instance.a);

        instance.createdSlider.transform.SetAsFirstSibling();
        return instance.createdSlider.GetComponent<Slider>();
    }

    public static void destroySlider()
    {
        if (instance.createdSlider != null) Destroy(instance.createdSlider);
    }

    public static bool hasActiveSlider()
    {
        return instance.createdSlider != null;
    }
}
