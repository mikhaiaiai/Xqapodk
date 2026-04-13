using UnityEngine;
[System.Serializable]
public class LocalisationJSON
{
    public LocalisationItemValues[] localisationItems = new LocalisationItemValues[0];
    public RecipeLocalisationItemValues[] recipeLocalisationItems = new RecipeLocalisationItemValues[0];

}
[System.Serializable]
public class LocalisationItemValues
{
    public string KEY;
    public string en;
    public string ru;
}
[System.Serializable]
public class RecipeLocalisationItemValues
{
    public string RECIPE_KEY;
    public string name_en;
    public string name_ru;
    public string description_en;
    public string description_ru;
}