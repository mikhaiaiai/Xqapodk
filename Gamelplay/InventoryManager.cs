
using NUnit.Framework;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
}
[System.Serializable]
public class InventorySlot : MonoBehaviour
{
    public Item item;
    public int amount;
}
public enum Item
{
    none,
    axe,
    pickaxe,
    hammer,
}