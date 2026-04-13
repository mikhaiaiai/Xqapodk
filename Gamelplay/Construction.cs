using UnityEngine;

[System.Serializable]
public class Construction : MonoBehaviour
{
    public ConstructionType type;
}
public enum ConstructionType
{
    cabin,
    statue,
    firepit
}