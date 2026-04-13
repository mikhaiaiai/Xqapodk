using NUnit.Framework;
using UnityEngine;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using System.IO;

public class LocalisationManager : MonoBehaviour
{
    private Language currentLanguage = Language.en;

    private const string filename = "Localisation";
    private LocalisationJSON data = null;

    private LocalisationItem[] loadedLocItems;
    private RecipeLocalisationItem[] loadedRecipeLocItems;

    public void Init()
    {
        loadedLocItems = FindObjectsByType<LocalisationItem>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        loadedRecipeLocItems = FindObjectsByType<RecipeLocalisationItem>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
        LoadLocalisationFile();
    }
    public Language GetLanguage()
    {
        return currentLanguage;
        
    }
   
    public void LoadLocalisationFile()
    {
        string saveString = SaveSystem.Load(filename);
        if (saveString != null)
        {
            data = JsonUtility.FromJson<LocalisationJSON>(saveString);
            UpdateLocalisationItems();
        }
    }
    private void UpdateLocalisationItems()
    {
        foreach (var locItem in loadedLocItems)
        {
            foreach (var dataItem in data.localisationItems)
            {
                if (locItem.KEY == dataItem.KEY)
                {
                    locItem.en = dataItem.en;
                    locItem.ru = dataItem.ru;
                    break;
                }
            }
        }

        foreach (var locRecipeItem in loadedRecipeLocItems)
        {
            foreach (var dataItem in data.recipeLocalisationItems)
            {
                if (locRecipeItem.RECIPE_KEY == dataItem.RECIPE_KEY)
                {
                    locRecipeItem.name_en = dataItem.name_en;
                    locRecipeItem.name_ru = dataItem.name_ru;
                    locRecipeItem.description_en = dataItem.description_en;
                    locRecipeItem.description_ru = dataItem.description_ru;
                    break;
                }
            }
        }
        UpdateTexts();
    }
    private void UpdateTexts()
    {
        string daystring = "DAY";
        foreach (var locItem in loadedLocItems)
        {
            /* ¬ каждом простом элементе локализации мен€ем текст
                    на соответствующий €зыку вариант*/
            locItem.gameObject.GetComponent<TextMeshProUGUI>().text =
                currentLanguage == Language.en ? locItem.en : locItem.ru;


            if (locItem.KEY == "DAY")
            {
                daystring = currentLanguage == Language.en ? locItem.en : locItem.ru;
            }
        }
        foreach (var locRecipeItem in loadedRecipeLocItems)
        {
            /* ¬ каждом локализуемом рецепте мен€ем данные компонента (им€ и описание)
                      на соответствующий €зыку вариант*/
            locRecipeItem.gameObject.GetComponent<CraftingRecipe>().recipeName =
                currentLanguage == Language.en ?
                locRecipeItem.name_en :
                locRecipeItem.name_ru;

            locRecipeItem.gameObject.GetComponent<CraftingRecipe>().description =
                currentLanguage == Language.en ?
                locRecipeItem.description_en :
                locRecipeItem.description_ru;
        }
        GameManager.instance.timeManager.SetDayString(daystring);
    }
    public void ChangeLanguage()
    {
        // ≈сли текущий €зык английский, то мен€етс€ на русский и наоборот
        currentLanguage = currentLanguage == Language.en ? Language.ru : Language.en;
        UpdateTexts();
    }
    public void ChangeLanguage(Language language)
    {
        currentLanguage = language;
        UpdateTexts();
    }

    /* Ёто все дл€ сохранени€ первичного файла в редакторе
    public void SaveLocalisationFile()
    {
        LocalisationJSON loc = new LocalisationJSON();

        foreach (var locItem in loadedLocItems)
        {
            if (UniqueCheck(loc.localisationItems, locItem.KEY))
            {
                var newLocItem = new LocalisationItemValues();
                newLocItem.KEY = locItem.KEY;
                newLocItem.en = locItem.en;
                newLocItem.ru = locItem.ru;

                loc.localisationItems = ArrayAdd(loc.localisationItems, newLocItem);
            }
        }

        foreach (var locRecipeItem in loadedRecipeLocItems)
        {
            if (UniqueCheck(loc.recipeLocalisationItems, locRecipeItem.RECIPE_KEY))
            {
                var newLocItem = new RecipeLocalisationItemValues();
                newLocItem.RECIPE_KEY = locRecipeItem.RECIPE_KEY;
                newLocItem.name_en = locRecipeItem.name_en;
                newLocItem.name_ru = locRecipeItem.name_ru;
                newLocItem.description_en = locRecipeItem.description_en;
                newLocItem.description_ru = locRecipeItem.description_ru;

                loc.recipeLocalisationItems = ArrayAdd(loc.recipeLocalisationItems, newLocItem);
            }
        }

        string json = JsonUtility.ToJson(loc, true);
        SaveSystem.Save(filename, json);
    }
    private bool UniqueCheck(LocalisationItemValues[] arr, string key)
    {
        foreach (var item in arr)
        {
            if (item.KEY == key) return false;
        }
        return true;
    }
    private bool UniqueCheck(RecipeLocalisationItemValues[] arr, string key)
    {
        foreach (var item in arr)
        {
            if (item.RECIPE_KEY == key) return false;
        }
        return true;
    }
    private LocalisationItemValues[] ArrayAdd(LocalisationItemValues[] arr, LocalisationItemValues item)
    {
        var newArr = arr.ToList();
        newArr.Add(item);
        return newArr.ToArray();
    }
    private RecipeLocalisationItemValues[] ArrayAdd(RecipeLocalisationItemValues[] arr, RecipeLocalisationItemValues item)
    {
        var newArr = arr.ToList();
        newArr.Add(item);
        return newArr.ToArray();
    }

    */
}
public enum Language
{
    en,
    ru
}
