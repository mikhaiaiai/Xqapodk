using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Item item;

    public void PickUp()
    {

    }
    public void Drop(Vector3 pos)
    {
        GameManager.instance.instancesManager.InstantiateCollectable(item, pos);
    }
}
