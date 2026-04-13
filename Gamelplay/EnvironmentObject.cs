
using UnityEngine;

[System.Serializable]
public class EnvironmentObject : MonoBehaviour
{
    public EnvironmentObjectType type;
}
public enum EnvironmentObjectType
{
    rock,
    mossyStone
}
