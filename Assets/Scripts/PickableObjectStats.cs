using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectStats : MonoBehaviour
{
    [Header("More weight = Thrown further")]
    [Range(4, 20)]
    public float Weight = 4;

    public bool inInventory = false;
    public bool putOnTable = false;
}
