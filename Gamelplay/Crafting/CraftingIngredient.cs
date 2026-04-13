using TMPro;
using UnityEngine;

public class CraftingIngredient : MonoBehaviour
{
    public Ingredient type;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI amount;
}
public enum Ingredient
{
    wood,
    rock,
    axe,
    rope,
    grass,
    metal,
    gold,
    skin,
    woodChest,
    shelter,
    woodDeck,
    fire,
    water,
    vegetable,
    corn,
    rawMeat,
    rawFish,
    wheat,
    potato,
    carrot,
    coconut,
    rice,
    tomato,
    herbs
}
