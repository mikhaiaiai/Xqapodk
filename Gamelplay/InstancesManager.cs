using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InstancesManager : MonoBehaviour
{
    [SerializeField] private Transform characterParent;
    [SerializeField] private Transform worldParent;

    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private List<GameObject> environmentObjectsPrefabs;
    [SerializeField] private List<GameObject> constructionsPrefabs;
    [SerializeField] private List<GameObject> animalsPrefabs;
    [SerializeField] private List<GameObject> collectablesPrefabs;

    public GameObject character;
    public List<GameObject> environmentObjects;
    public List<GameObject> constructions;
    public List<GameObject> animals;
    public List<GameObject> collectables;

    public void InstantiateCharacter(Vector3 position)
    {
        Destroy(character);
        character = Instantiate(characterPrefab, position, Quaternion.identity, characterParent);
    }
    public void InstantiateEnvironmentObject(EnvironmentObjectType type, Vector3 position)
    {
        foreach (var prefab in environmentObjectsPrefabs)
        {
            if (prefab.GetComponent<EnvironmentObject>().type == type)
            {
                var newObject = Instantiate(prefab, position, Quaternion.identity, worldParent);
                environmentObjects.Add(newObject);
                return;
            }
        }
    }
    public void InstantiateConstruction(ConstructionType type, Vector3 position)
    {
        foreach (var prefab in constructionsPrefabs)
        {
            if (prefab.GetComponent<Construction>().type == type)
            {
                var newObject = Instantiate(prefab, position, Quaternion.identity, worldParent);
                constructions.Add(newObject);
                return;
            }
        }
    }
    public void InstantiateAnimal (AnimalType type, Vector3 position)
    {
        foreach (var prefab in animalsPrefabs)
        {
            if (prefab.GetComponent<Animal>().type == type)
            {
                var newObject = Instantiate(prefab, position, Quaternion.identity, worldParent);
                animals.Add(newObject);
                return;
            }
        }
    }
    public void InstantiateCollectable(Item item, Vector3 position)
    {
        foreach (var prefab in collectablesPrefabs)
        {
            if (prefab.GetComponent<Collectable>().item == item)
            {
                var newObject = Instantiate(prefab, position, Quaternion.identity, worldParent);
                collectables.Add(newObject);
                return;
            }
        }
    }

    public void ClearWorld()
    {
        foreach(var envObject in environmentObjects)
        {
            Destroy(envObject);
        }
        environmentObjects.Clear();

        foreach (var construction in constructions)
        {
            Destroy(construction);
        }
        constructions.Clear();

        foreach (var animal in animals)
        {
            Destroy(animal);
        }
        animals.Clear();

        foreach (var collectable in collectables)
        {
            Destroy(collectable);
        }
        collectables.Clear();
    }
}