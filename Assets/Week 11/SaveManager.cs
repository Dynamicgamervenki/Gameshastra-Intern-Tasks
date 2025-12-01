using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instace;
    public Inventory inventory;
    private string filePath = Application.dataPath + "saveFile.json";

    private void Awake()
    {
        if (Instace != null && Instace != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instace = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    PlayerSaveData data;
    private void Start()
    {
          data = new PlayerSaveData();
         data.items = new List<Item>();
        // data.items.Add(new Item(ItemType.HeaalthPotion,2));
        // data.items.Add(new Item(ItemType.Bow,1));
        // data.health = 80;

        //string json = JsonUtility.ToJson(data);
        //Debug.LogError(json);

        // File.WriteAllText(Application.dataPath + "saveFile.json",json);

       // string json = File.ReadAllText(Application.dataPath + "saveFile.json");
      //  Debug.LogWarning(json);

      //  PlayerSaveData loadedData =  JsonUtility.FromJson<PlayerSaveData>(json);
       // Debug.LogWarning(loadedData.health + " " + loadedData.items.Count);
    }

    public List<Item> GetItemsFromSave()
    {
        if (!File.Exists(filePath)) return null;

        string json = File.ReadAllText(filePath);
        PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);
        return data.items;
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        data.items = inventory.GetItemsList();
        data.health = 100.0f;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }
}

[System.Serializable]
public class PlayerSaveData
{
    public float health;
    public List<Item> items;
}

