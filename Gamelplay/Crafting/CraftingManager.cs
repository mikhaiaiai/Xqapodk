using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CraftingManager : MonoBehaviour
{
    private const string filename = "CraftRecipes";

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private GameObject grid;

    [SerializeField] private List<GameObject> ingredients;

    private CraftingRecipe[] recipeObjects;

    public void Init()
    {
        recipeObjects = FindObjectsByType<CraftingRecipe>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        LoadRecipes();
    }
    // Editor only
    /*
    public void SaveRecipes()
    {
        var serRecipes = new CraftRecipesJSON();

        var serRecipesArr = new List<SerializableCraftRecipe>();

        foreach (var recipeObj in recipeObjects)
        {
            var newSerRecipe = new SerializableCraftRecipe();
            var newSerComponents = new List<SerializableCraftComponent>();
            foreach(var comp in recipeObj.components)
            {
                var newSerComponent = new SerializableCraftComponent();
                newSerComponent.ingredientType = (int)comp.type;
                newSerComponent.amount = comp.amount;
                newSerComponents.Add(newSerComponent);
            }
            newSerRecipe.KEY = recipeObj.KEY;
            newSerRecipe.components = newSerComponents.ToArray();

            serRecipesArr.Add(newSerRecipe);
        }
        
        serRecipes.recipes = serRecipesArr.ToArray();

        string json = JsonUtility.ToJson(serRecipes, true);
        SaveSystem.Save(filename, json);
    }
    */
    private void LoadRecipes()
    {
        string saveString = SaveSystem.Load(filename);
        if (saveString != null )
        {
            var data = JsonUtility.FromJson<CraftRecipesJSON>(saveString);
            foreach (var recipe in data.recipes)
            {
                foreach (var recipeObj in recipeObjects)
                {
                    if (recipe.KEY == recipeObj.KEY)
                    {
                        var newComps = new List<CraftingRecipe.CraftingComponent>();

                        foreach(var dataComp in recipe.components)
                        {
                            var newComp = new CraftingRecipe.CraftingComponent();
                            newComp.type = (Ingredient)dataComp.ingredientType;
                            newComp.amount = dataComp.amount;
                            newComps.Add(newComp);
                        }

                        recipeObj.components = newComps;
                    }
                }
            }
        }
    }

    public void ShowRecipe(CraftingRecipe recipe)
    {
        image.sprite = recipe.getSprite();
        recipeName.text = recipe.recipeName;
        description.text = recipe.description;

        var children = grid.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject.name != "Grid") Destroy(child.gameObject);
        }

        foreach(var comp in recipe.components)
        {
            foreach (var ing in ingredients)
            {
                if (ing.GetComponent<CraftingIngredient>().type == comp.type)
                {
                    var newIng = Instantiate(ing, grid.transform);
                    var obj = newIng.GetComponent<CraftingIngredient>();
                    obj.nameText.text =
                        GameManager.instance.localisationManager.GetLanguage() == Language.en ?
                        obj.nameText.GetComponent<LocalisationItem>().en :
                        obj.nameText.GetComponent<LocalisationItem>().ru;
                    if (comp.amount > 1)
                    {
                        obj.amount.text = comp.amount.ToString();
                        obj.amount.enabled = true;
                    }
                }
            }
        }
    }
}
