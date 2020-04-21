﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject currentlyEquipped;
    private Ray ray;
    private Transform highlighted;
    private Material normalMaterial;

    public List<InventoryItem> inventory;
    public Transform[] inventoryTransforms;

    [Space]
    [Space]
    [Space]
    public Material highlightedMaterial;
    public Transform playerHand;

    [Header("Update with selectableObjects' Tags")]
    public string[] selectableTags;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Unhighlight objects

        if(highlighted != null)
        {
            highlighted.GetComponent<MeshRenderer>().material = normalMaterial;
            highlighted = null;
        }


        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 2, Color.red);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 2))
        {
            if (CanBeGrabbed(hit.transform.gameObject))
            {
                Highlight(hit.transform.gameObject);

                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform.gameObject.CompareTag("Lock"))
                    {
                        if (currentlyEquipped != null)
                        {
                            hit.transform.GetComponent<Lock>().OpenLock(currentlyEquipped.transform.gameObject);
                            currentlyEquipped = null;
                        }

                        else
                        {
                            Debug.Log("I need a key to open this...");
                        }
                    }

                    if (hit.transform.gameObject.CompareTag("Key"))
                    {
                        PickUp(hit.transform.gameObject);
                    }
                }

                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (currentlyEquipped != null)
                        {
                            Drop();
                        }
                    }
                }

            }
            
                //else
                //{
                //    if (Input.GetMouseButtonDown(1))
                //    {
                //        if (currentlyEquipped != null)
                //        {
                //            Drop();
                //        }
                //    }
                //}

            
        }

        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentlyEquipped != null)
                {
                    Drop();
                }
            }

            else if (Input.GetMouseButtonDown(1))
            {
                if (inventory.Count > 1)
                {
                    CycleInvPos();
                }
            }
        }
            
    }

    private void LateUpdate()
    {
        if(inventory.Count > 0)
        {
            currentlyEquipped = inventory[0].itemGO;
            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].itemGO.transform.position = inventory[i].slotTransform.position;
                inventory[i].itemGO.transform.SetParent(this.transform);
            }
        }

        else
        {
            currentlyEquipped = null;
        }
        
    }

    void PickUp(GameObject objToPickUp)
    {
            if(IsInvEmpty(inventory.Count))
            {
                inventory.Add(new InventoryItem(objToPickUp, inventoryTransforms[0]));
            }

            else
            {
                 inventory.Add(new InventoryItem(objToPickUp, inventoryTransforms[inventory.Count]));
            }

                currentlyEquipped = objToPickUp;
                currentlyEquipped.GetComponent<Rigidbody>().isKinematic = true;

    }

    void Drop()
    {

        currentlyEquipped.transform.SetParent(null);
        currentlyEquipped.GetComponent<Rigidbody>().isKinematic = false;
        inventory.Remove(inventory[0]);
        currentlyEquipped = null;

        if (inventory.Count > 1)
        {
            CycleInvPos();
        }

    }

    void Highlight(GameObject aimedObject)
    {
        normalMaterial = aimedObject.transform.GetComponent<MeshRenderer>().material;
        aimedObject.transform.GetComponent<MeshRenderer>().material = highlightedMaterial;
        highlighted = aimedObject.transform;
    }



    bool CanBeGrabbed(GameObject aimedObject)
    {
        for (int i = 0; i < selectableTags.Length; i++)
        {
            if (aimedObject.transform.CompareTag(selectableTags[i]))
            {
                return true;
            }
        }
        return false;
    }

    bool IsInvEmpty(int inventoryCount)
    {
        if(inventoryCount <= 0)
        {
            Debug.Log("Inventory's empty. Addind item...");
            return true;
        }
        else
        {
            return false;
        }
    }

    void CycleInvPos()
    {
        Debug.Log("Cycling through  : " + inventory.Count + " objets d'inventaire");

        Transform tempTransform = inventoryTransforms[0];
        for (int i = inventory.Count; i < inventory.Count; i++)
        {
            if(i > inventory.Count - 2)
            {
                Debug.Log("Ouais ouais");
                inventory[i].slotTransform = tempTransform;
            }
            else
            {
                inventory[i].slotTransform = inventoryTransforms[i + 1];
            }
        }
    }
}
