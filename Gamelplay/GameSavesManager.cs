using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSavesManager : MonoBehaviour
{
    private const string filename = "GameSave";
    private Coroutine timer30s;
    private IEnumerator Timer30s()
    {
        yield return new WaitForSeconds(30);
        SaveGame();
        timer30s = StartCoroutine(Timer30s());
    }
    public void SaveGame()
    {
        var newSave = new GameSaveJSON();

        newSave.day = GameManager.instance.timeManager.GetDay();
        newSave.time = GameManager.instance.timeManager.GetTime();

        var newCharacter = new SerializableCharacter();
        newCharacter.position = GameManager.instance.instancesManager.character.transform.position;
            // tools
        newSave.character = newCharacter;

        var newInventory = new List<SerializableInventorySlot>();
        foreach (var slot in GameManager.instance.player.inventoryManager.inventorySlots)
        {
            var newSlot = new SerializableInventorySlot();
            if (slot != null)
            {
                newSlot.itemType = ((int)slot.item);
                newSlot.amount = slot.amount;
            }
            newInventory.Add(newSlot);
        }
        newSave.inventory = newInventory.ToArray();


        var newEnvObjList = new List<SerializableEnvObject>();
        foreach (var envObj in GameManager.instance.instancesManager.environmentObjects)
        {
            var newObj = new SerializableEnvObject();
            newObj.type = (int)envObj.GetComponent<EnvironmentObject>().type;
            newObj.position = new SerializableVector3(envObj.transform.position);
        }
        newSave.environmentObjects = newEnvObjList.ToArray();


        var newConstrList = new List<SerializableConstruction>();
        foreach (var constrObj in GameManager.instance.instancesManager.constructions)
        {
            var newObj = new SerializableConstruction();
            newObj.type = ((int)constrObj.GetComponent<Construction>().type);
            newObj.position = new SerializableVector3(constrObj.transform.position);
        }
        newSave.constructions = newConstrList.ToArray();


        var newAnimalsList = new List<SeriazableAnimal>();
        foreach (var animal in GameManager.instance.instancesManager.animals)
        {
            var newObj = new SeriazableAnimal();
            newObj.type = ((int)animal.GetComponent<Animal>().type);
            newObj.position = new SerializableVector3(animal.transform.position);
        }
        newSave.animals = newAnimalsList.ToArray();

        string json = JsonUtility.ToJson(newSave, true);
        SaveSystem.Save(filename, json);
    }
    public void LoadSave()
    {
        string saveString = SaveSystem.Load(filename);
        if (saveString != null)
        {
            var data = JsonUtility.FromJson<GameSaveJSON>(saveString);

            GameManager.instance.timeManager.SetDayTime(data.day, data.time);


            GameManager.instance.instancesManager.ClearWorld();


            GameManager.instance.instancesManager.InstantiateCharacter(data.character.position);
            // equipment


            List<InventorySlot> slots = new List<InventorySlot>();
            foreach (var slot in data.inventory)
            {
                var newSlot = new InventorySlot();
                newSlot.item = (Item)slot.itemType ;
                newSlot.amount = slot.amount;
                slots.Add(newSlot);
            }
            GameManager.instance.player.inventoryManager.inventorySlots = slots.ToArray();


            foreach (var envObj in data.environmentObjects)
            {
                GameManager.instance.instancesManager.InstantiateEnvironmentObject((EnvironmentObjectType)envObj.type, new Vector3(envObj.position.x, envObj.position.y, envObj.position.z));
            }

            foreach (var constr in data.constructions)
            {
                GameManager.instance.instancesManager.InstantiateConstruction((ConstructionType)constr.type, new Vector3(constr.position.x, constr.position.y, constr.position.z));
            }

            foreach (var animal in data.animals)
            {
                GameManager.instance.instancesManager.InstantiateAnimal((AnimalType)animal.type, new Vector3(animal.position.x, animal.position.y, animal.position.z));
            }

        }
        else
        {
            GameManager.instance.instancesManager.InstantiateCharacter(Vector3.zero);
        }

        timer30s = StartCoroutine(Timer30s());
    }
}
