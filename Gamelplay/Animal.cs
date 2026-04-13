using NUnit.Framework;
using UnityEngine;
using UnityEngine.Analytics;
[System.Serializable]
public class Animal : MonoBehaviour
{
    public AnimalType type;
}
public enum AnimalType
{
    pig,
    bear,
    wolf,
    bee,
    rabbit,
    deer,
    horse,
    butterfly
}
