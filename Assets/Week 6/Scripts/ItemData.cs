using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static ItemData Instance { get; private set; }
    public List<SO_Items> items;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        items = new List<SO_Items>();
        SO_Items[] allObjects = Resources.LoadAll<SO_Items>("Data");
        foreach (SO_Items obj in allObjects)
        {
            items.Add(obj);
        }
    }


}
