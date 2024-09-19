using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Cook : MonoBehaviour
{
    public Recipe Recipe;
    public float rightMoveGives, wrongMoveTakes;
    public GameObject canvas, scoreField;
    float score;
    static Cook instance;
    public List<CookingStep> recipeBySteps = new();
    public List<ProductAndAmount> extraProducts = new();

    [Serializable]
    public class ProductAndAmount : Recipe.ProductAndAmount {
        public float userPutAmount;

        public ProductAndAmount(GameObject Product = null, float MinimalAmount = 0, float MaximalAmount = 0, float UserPutAmount = 0) : base (Product, MinimalAmount, MaximalAmount) {
            userPutAmount = UserPutAmount;
        }
    }

    [Serializable]
    public class CookingStep {
        public string stepTitle;
        public List<ProductAndAmount> items;
        public bool finished;
        public CookingStep(string StepTitle, List<ProductAndAmount> Items, bool Finished) {
            stepTitle = StepTitle;
            items = Items;
            finished = Finished;
        }
    }

    public GameObject plusPointsPrefab, minusPointsPrefab, textPrefab;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        instance.score = 0;
        instance.scoreField.GetComponent<TextMeshProUGUI>().text = "0";
        foreach (var step in Recipe.recipeBySteps) {
            List<ProductAndAmount> paa = new List<ProductAndAmount>();
            foreach (var item in step.items) paa.Add(new ProductAndAmount(item.product, item.minimalAmount, item.maximalAmount));
            recipeBySteps.Add(new CookingStep(step.stepTitle, paa, false));
        }
    }

    public static void AddScoreForObject(GameObject go, float amount = 0, bool canBeOverdone = true, bool addInsteadOfReplacing = true) {
        // Debug.Log((from steps in instance.recipeBySteps where steps.items.Any(x => x.product.name.Contains("яйцо1")) select steps).FirstOrDefault().stepTitle);
        // Положить ингредиенты в миску
        // это тут как образец linq, когда-нибудь да пригодится

        ProductAndAmount paa = instance.FindNeededProductInfo(go); // нужно убедиться, что имена продуктов все полностью разные

        if (paa != null) {
            if (amount == 0 && go.CompareTag("Ingredient")) {
                if (paa.userPutAmount < paa.maximalAmount) AddScoreManually();
                else if (canBeOverdone) TakeScoreManually();
                paa.userPutAmount++;
            }
            else {
                if (paa.userPutAmount == 0 || (paa.userPutAmount > 0 && paa.userPutAmount < paa.minimalAmount))
                {
                    float medianAmount = (paa.minimalAmount + paa.maximalAmount) / 2;
                    if (addInsteadOfReplacing) paa.userPutAmount += amount;
                    else paa.userPutAmount = amount;

                    if (paa.userPutAmount >= paa.minimalAmount && paa.userPutAmount < paa.maximalAmount) AddScoreManually();
                    else if (paa.userPutAmount >= paa.maximalAmount  && paa.userPutAmount < medianAmount * 1.5f) AddScoreManually(instance.rightMoveGives * 0.5f);
                    else if (paa.userPutAmount >= medianAmount * 1.5f && paa.userPutAmount < medianAmount * 1.75f) AddScoreManually(instance.rightMoveGives * 0.25f);
                    else if (paa.userPutAmount >= medianAmount * 1.75f) AddScoreManually(instance.rightMoveGives * 0.1f);
                }
                else if (amount > 0 && canBeOverdone) TakeScoreManually();
            }
        }

        else {
            ProductAndAmount extraPaa = instance.extraProducts.Find(x => go.name.Contains(x.product.name));
            if (extraPaa == null) instance.extraProducts.Add(new ProductAndAmount(go, 0, 0, amount));
            else if (amount == 0) extraPaa.userPutAmount++;
            else extraPaa.userPutAmount += amount;
            TakeScoreManually();
        }

        if (go.tag == "Ingredient")
        {
            AudioSource audioSource = go.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                if (audioSource.enabled == true) audioSource.Play();
                else audioSource.enabled = true;
                Destroy(go, audioSource.clip.length);
            }
            else Destroy(go);
        }
        FinishSteps();
    }

    public static void AddScoreManually(float score = 0) {
        if (score == 0) {
            instance.score += instance.rightMoveGives;
            instance.ShowPoints(true);
        }
        else {
            instance.score += score;
            instance.ShowPoints(true, score);
        }

        instance.UpdateScore();
    }

    public static void TakeScoreManually(float score = 0) {
        if (score == 0) {
            instance.score -= instance.wrongMoveTakes;
            instance.ShowPoints(false);
        }
        else {
            instance.score -= score;
            instance.ShowPoints(false, score);
        }

        instance.UpdateScore();
    }

    public void UpdateScore() {
        instance.scoreField.GetComponent<TextMeshProUGUI>().text = instance.score.ToString();
    }

    public void ShowPoints(bool adding, float score = 0) {
        GameObject go;
        if (adding) {
            go = Instantiate(instance.plusPointsPrefab, Input.mousePosition, instance.transform.rotation, instance.canvas.transform);
            if (score == 0) go.GetComponentInChildren<TextMeshProUGUI>().text = "+" + instance.rightMoveGives;
            else go.GetComponentInChildren<TextMeshProUGUI>().text = "+" + score;
        }
        else {
            go = Instantiate(instance.minusPointsPrefab, Input.mousePosition, instance.transform.rotation, instance.canvas.transform);
            if (score == 0) go.GetComponentInChildren<TextMeshProUGUI>().text = "-" + instance.wrongMoveTakes;
            else go.GetComponentInChildren<TextMeshProUGUI>().text = "-" + score;
        }
        Destroy(go, 1f);
    }

    public static void ShowText(string text, bool atCursorPos = true) {
        GameObject go;
        if (atCursorPos) go = Instantiate(instance.textPrefab, Input.mousePosition, instance.transform.rotation, instance.canvas.transform);
        else go = Instantiate(instance.textPrefab, instance.canvas.transform.position, instance.transform.rotation, instance.canvas.transform);
        go.GetComponentInChildren<TextMeshProUGUI>().text = text;
        Destroy(go, 1f);
    }

    ProductAndAmount FindNeededProductInfo(GameObject go)
    {
        foreach (var step in instance.recipeBySteps) {
            if (step.items.Exists(x => go.name.Contains(x.product.name))) {
                return step.items.Find(x => go.name.Contains(x.product.name));
            }
        }
        return null;
    }

    public static float[] GetNeededAmount(GameObject go) {
        ProductAndAmount paa = instance.FindNeededProductInfo(go);
        float[] f = { (paa == null)? 0 : paa.minimalAmount, (paa == null)? 0 : paa.maximalAmount };
        return f;
    }

    public static bool IsThereEnoughProduct(GameObject product)
    {
        ProductAndAmount paa = instance.FindNeededProductInfo(product);
        return paa.userPutAmount >= paa.minimalAmount;
    }

    public static void FinishSteps() {
        foreach (var step in instance.recipeBySteps) {
            if (!step.finished) {
                int counter = 0;
                foreach (var item in step.items) if (item.userPutAmount >= item.minimalAmount) counter++;
                if (counter == step.items.Count) step.finished = true;
            }
        }
    }

    public static bool PrevStepsFinished(GameObject go) {
        int counter = 0;
        for (int i = 0; i < instance.recipeBySteps.Count; i++) {
            var step = instance.recipeBySteps[i];
            if (step.items.Exists(x => go.name.Contains(x.product.name))) {
                if (counter == i) return true;
                else return false;
            }
            else if (step.finished) counter++;
        }
        return false;
    }

    public static bool AllStepsFinished()
    {
        int counter = 0;
        foreach (CookingStep step in instance.recipeBySteps)
            if (step.finished) counter++;
        return counter == instance.recipeBySteps.Count;
    }

    public static float GetRightMoveGiveValue() {
        return instance.rightMoveGives;
    }

    public static float GetWrongMoveTakesValue() {
        return instance.wrongMoveTakes;
    }

    public static float GetScore() {
        return instance.score;
    }

    public static int ScoreToStars() {
        float maxScore = 0;

        foreach (var step in instance.recipeBySteps) {
            if (step.stepTitle != "Жарить на температуре до готовности")
            {
                foreach (var item in step.items)
                {
                    if (item.minimalAmount <= 10) maxScore += item.maximalAmount * instance.rightMoveGives;
                    else maxScore += instance.rightMoveGives;
                }
            }
            else maxScore += 10 * 100 * 2;
        }

        for (int stars = 0; stars < 6; stars++) {
            if (instance.score <= stars * (maxScore / 5)) {
                return stars;
            }
        }
        return 0;
    }
}
