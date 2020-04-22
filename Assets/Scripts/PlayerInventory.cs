using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private GameObject currentlyEquipped;
    private Ray ray;
    private Transform highlighted;
    private Material normalMaterial;

    [HideInInspector]
    public List<InventoryItem> inventory;

    [HideInInspector]
    public Transform[] inventoryTransforms;

    [Space]
    [Space]
    [Space]
    public Material highlightedMaterial;
    public Transform playerHand;

    public GameObject playerInventory;

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
                            if(hit.transform.GetComponent<Lock>().OpenLock(currentlyEquipped.transform.gameObject))
                            {
                                Drop();
                            }
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
                    SwitchInv();
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
                SetParents();
                Animate();
            }
        }

        else
        {
            currentlyEquipped = null;
        }
        
    }

    void PickUp(GameObject objToPickUp)
    {
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

        if (inventory.Count > 0)
        {
            RearrangeInvPos();
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

    //bool IsInvEmpty(int inventoryCount)
    //{
    //    if(inventoryCount <= 0)
    //    {
    //        Debug.Log("Inventory's empty. Addind item...");
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    void RearrangeInvPos()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log("Cycling...");

            inventory[i].slotTransform = inventoryTransforms[i];
        }

        SetParents();
    }

    void SwitchInv()
    {
        GameObject tempGO = inventory[0].itemGO;

        for(int i = 0; i < inventory.Count - 1; i++)
        {
            inventory[i].itemGO = inventory[i + 1].itemGO;
        }

        inventory[inventory.Count - 1].itemGO = tempGO;

        SetParents();
    }

    void SetParents()
    {
        if(inventory.Count > 0)
        {
            for(int i = 0; i < inventory.Count; i++)
            {
                inventory[i].itemGO.transform.SetParent(inventoryTransforms[i].gameObject.transform);
            }
        }
    }

    void Animate()
    {
        playerInventory.GetComponent<Animator>().Play("Floating items");
    }
}
