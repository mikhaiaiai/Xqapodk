
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class CraftingRecipe : MonoBehaviour
{
    public string KEY;
    public string recipeName;
    public string description;

    public List<CraftingComponent> components;

    [SerializeField] private Image icon;
    public Sprite getSprite()
    {
        return icon.GetComponent<Image>().sprite;
    }
    [System.Serializable]
    public class CraftingComponent
    {
        public Ingredient type;
        public int amount;
    }
}