using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectStats : MonoBehaviour
{
    [Header("More weight = Thrown further")]
    [Range(4, 20)]
    public float Weight = 4;

    [HideInInspector]
    public bool inInventory = false;
    [HideInInspector]
    public bool putOnTable = false;
}
