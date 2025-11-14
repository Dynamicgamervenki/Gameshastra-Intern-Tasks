using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static ItemData instance { get; private set; }
    public List<SO_Items> items;

    private void Awake()
    {
        instance = this;
    }



}
