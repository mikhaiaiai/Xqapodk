using System;
using UnityEngine;

[System.Serializable]
public class GameSaveJSON 
{
    public int day;
    public float time;
    public SerializableCharacter character;
    public SerializableInventorySlot[] inventory;

    public SerializableEnvObject[] environmentObjects;
    public SerializableConstruction[] constructions;
    public SeriazableAnimal[] animals;
}

[System.Serializable]
public class SerializableCharacter
{
    public Vector3 position;
    public Equipment equippedTool;
    public Equipment equippedHat;
    public Equipment equippedBackpack;
    public Equipment equippedBoots;
    public Equipment equippedCoat;
    public Equipment equippedRing;
}
public enum Equipment
{
    none,
    axe,
    pickaxe,
    hammer,
    shovel,
    torch,
    wateringCan,
    fishingRod,
    jug,
    compass,
    rope,
    lure,
    trap,
    bat,
    spear,
    bow,
    hat,
    woodHelmet,
    winterHat,
    backpack,
    boots,
    winterBoots,
    woodArmor,
}

[System.Serializable]
public class SerializableInventorySlot
{
    public int itemType;
    public int amount;
}

[System.Serializable]
public class SerializableEnvObject
{
    public int type;
    public SerializableVector3 position;
}

[System.Serializable]
public class SerializableConstruction
{
    public int type;
    public SerializableVector3 position;
}

[System.Serializable]
public class SeriazableAnimal
{
    public int type;
    public SerializableVector3 position;
}

[System.Serializable]
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(Vector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }
}