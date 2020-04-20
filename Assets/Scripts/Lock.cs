using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool isUnlocked;

    public void OpenLock(GameObject currentItem)
    {
        if(currentItem.CompareTag("Key"))
        {
            Debug.Log("Opened the lock");
            isUnlocked = !isUnlocked;
            currentItem.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("I need a key to open this...");
        }
    }
}
