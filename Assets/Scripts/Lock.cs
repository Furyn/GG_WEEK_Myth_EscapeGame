using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{

    public bool OpenLock(GameObject currentItem)
    {
        if(currentItem.CompareTag("Key"))
        {
            Debug.Log("Opened the lock");
            currentItem.SetActive(false);
            this.gameObject.SetActive(false);
            return true;
        }
        else
        {
            Debug.Log("I need a key to open this...");
            return false;
        }
    }
}
