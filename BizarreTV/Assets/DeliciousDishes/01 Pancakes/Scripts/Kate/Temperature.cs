using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
public class Temperature : MonoBehaviour
{
    float MaxTemp = 280f;
    public float TempNow = 23f;
    float MinTemp = 23f;
    float StartToHeat = 50f;
    float rotationSpeed = 100f;
    float SpeedOfTempChange = 0.1f;
    float currentAngle = 0f;
    public float KoefTime = 0f;
    public GameObject heat;
    public TMP_Text textMesh;
    float Transparency = 0f;
    SpriteRenderer heatSpriteRenderer;

    void Start()
    {
        heatSpriteRenderer = heat.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        float targetTemp = Mathf.Lerp(MinTemp, MaxTemp, (currentAngle % 360f) / 360f);
        TempNow = Mathf.Lerp(TempNow, targetTemp, Time.deltaTime * SpeedOfTempChange);
        KoefTime = TempNow < StartToHeat ? 0f : TempNow / MaxTemp;

        Transparency = TempNow < StartToHeat ? 0f : Mathf.InverseLerp(StartToHeat, MaxTemp, TempNow);
        Color color = heatSpriteRenderer.color;
        color.a = Transparency;
        heatSpriteRenderer.color = color;
        textMesh.text = (int)TempNow + "";
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0))
            {
                currentAngle = (currentAngle + rotationSpeed * Time.deltaTime) % 360f;
            }
            else if (Input.GetMouseButton(1))
            {
                currentAngle = (currentAngle - rotationSpeed * Time.deltaTime + 360f) % 360f;
            }

            Quaternion targetRotation = Quaternion.Euler(0, 0, currentAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}