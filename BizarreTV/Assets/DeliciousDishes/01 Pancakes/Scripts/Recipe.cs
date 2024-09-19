using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Delicious Dishes/Recipe", order = 0)]
public class Recipe : ScriptableObject
{
    public List<CookingStep> recipeBySteps = new();

    [Serializable]
    public class ProductAndAmount {
        public GameObject product;
        [Tooltip("нижняя граница на максимум очков")]
        public float minimalAmount;
        [Tooltip("верхняя граница на 1/2 очков")]
        public float maximalAmount;

        public ProductAndAmount(GameObject Product, float MinimalAmount, float MaximalAmount) {
            product = Product;
            minimalAmount = MinimalAmount;
            maximalAmount = MaximalAmount;
        }
    }

    [Serializable]
    public class CookingStep {
        public string stepTitle;
        public List<ProductAndAmount> items;
    }
}
