
using System.Collections.Generic;

public class CraftRecipesJSON
{
    public SerializableCraftRecipe[] recipes;
}

[System.Serializable]
public class SerializableCraftRecipe
{
    public string KEY;
    public SerializableCraftComponent[] components;
}

[System.Serializable]
public class SerializableCraftComponent
{
    public int ingredientType;
    public int amount;
}
