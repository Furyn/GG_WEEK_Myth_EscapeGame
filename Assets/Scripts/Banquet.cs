using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banquet : MonoBehaviour
{
    [Header("Put the items the player has to find")]
    public List<GameObject> gameObjectsNeeded;
    [Space]
    [HideInInspector]
    public List<GameObject> gameObjectsOnTable;
    [Header("Works like inventory slots")]
    public Transform[] placesToTransform;
    public GameObject player;
    private int itemsOnTable = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void PutItemOn(GameObject selectedObject)
    {

        /*if(player.GetComponent<PlayerInventory>().inventory.Count > 0)
        {
            player.GetComponent<PlayerInventory>().Drop();
        }*/

        if(itemsOnTable < gameObjectsNeeded.Count)
        {
            selectedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            selectedObject.GetComponent<Rigidbody>().isKinematic = true;

            for (int i = 0; i < gameObjectsOnTable.Count; i++)
            {
                if (gameObjectsOnTable[i] == null)
                {
                    gameObjectsOnTable[i] = selectedObject;

                    selectedObject.transform.position = placesToTransform[i].position;
                    selectedObject.transform.rotation = placesToTransform[i].rotation;

                    itemsOnTable++;

                    break;
                }

            }



            Debug.Log("Items on table : " + itemsOnTable);
            if (itemsOnTable == gameObjectsNeeded.Count)
            {
                CheckComposition();
            }

            selectedObject.GetComponent<PickableObjectStats>().putOnTable = true;
        }           
    }

    public void PullItemFrom(GameObject highlightedObject)
    {

        for (int i = 0; i < gameObjectsOnTable.Count; i++)
        {
            if(highlightedObject.gameObject == gameObjectsOnTable[i])
            {
                gameObjectsOnTable[i].GetComponent<PickableObjectStats>().putOnTable = false;
                gameObjectsOnTable[i].GetComponent<Rigidbody>().isKinematic = false;
                gameObjectsOnTable[i] = null;
            }
        }

        itemsOnTable--;

    }

    void Victory()
    {

    }

    void Defeat()
    {

    }

    void CheckComposition()
    {
        //Debug.Log("All objects have been placed. Checking...");
        int good = 0;

        for (int i = 0; i < gameObjectsNeeded.Count; i++)
        {

            if(gameObjectsNeeded.Contains(gameObjectsOnTable[i]))
            {
                //Debug.Log(gameObjectsOnTable[i].name + " is good");
                good++;
            }

            else
            {
                gameObjectsOnTable[i].GetComponent<Rigidbody>().isKinematic = false;
                gameObjectsOnTable[i].GetComponent<Rigidbody>().velocity = new Vector3(4, 2, 0);
                PullItemFrom(gameObjectsOnTable[i]);
            }

            if (good == 4)
            {
                Debug.Log("Gagné!");
            }

            else
            {
                Debug.Log("Perdu!");
            }


        }
    }
}
