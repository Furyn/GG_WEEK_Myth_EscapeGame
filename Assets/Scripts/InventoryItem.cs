using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    //[HideInInspector]
    public GameObject itemGO;
    public Transform slotTransform;

    public InventoryItem(GameObject _itemGO, Transform _slotTransform)
    {
        itemGO = _itemGO;
        slotTransform = _slotTransform;
    }
}
