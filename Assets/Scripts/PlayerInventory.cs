using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject currentlyEquipped;
    private Ray ray;
    private Transform highlighted;
    private Material normalMaterial;

    [HideInInspector]
    public List<InventoryItem> inventory;

    public Transform[] inventoryTransforms;

    [Space]
    [Space]
    [Space]
    public Material highlightedMaterial;
    public Transform playerHand;

    public GameObject playerInventory;

    public Animator playerAnimator;

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.Instance.isPaused)
        {
            //Unhighlight objects

            if (highlighted != null)
            {
                highlighted.GetComponent<MeshRenderer>().material = normalMaterial;
                highlighted = null;
            }


            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 2, Color.red);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, 2))
            {
                if (CanBeInteractedWith(hit.transform.gameObject))
                {
                    if (hit.transform.gameObject.GetComponent<PickableObjectStats>())
                    {
                        if (!hit.transform.gameObject.GetComponent<PickableObjectStats>().inInventory)
                        {
                            Highlight(hit.transform.gameObject);
                        }
                    }

                    else if(hit.transform.GetComponent<Lock>())
                    {
                        Highlight(hit.transform.gameObject);
                    }

                    else if (hit.transform.GetComponent<Banquet>())
                    {
                        Highlight(hit.transform.gameObject);
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject.GetComponent<Lock>())
                        {
                            if (currentlyEquipped != null)
                            {
                                if (hit.transform.GetComponent<Lock>().OpenLock(currentlyEquipped.transform.gameObject))
                                {
                                    Drop();
                                }
                            }

                            else
                            {
                                Debug.Log("I need a key to open this...");
                            }
                        }

                        else if (hit.transform.gameObject.GetComponent<PickableObjectStats>())
                        {
                            if(!hit.transform.gameObject.GetComponent<PickableObjectStats>().inInventory)
                            {
                                if (hit.transform.GetComponent<PickableObjectStats>().putOnTable)
                                {
                                    GameObject.FindGameObjectWithTag("Banquet").GetComponent<Banquet>().PullItemFrom(hit.transform.gameObject);
                                }

                                PickUp(hit.transform.gameObject);
                            }
                        }

                        else if(hit.transform.gameObject.GetComponent<Banquet>())
                        {
                            if(currentlyEquipped != null)
                            {
                                if (!currentlyEquipped.GetComponent<PickableObjectStats>().putOnTable)
                                {
                                    GameObject equippedObject = currentlyEquipped.gameObject;
                                    Drop();
                                    hit.transform.gameObject.GetComponent<Banquet>().PutItemOn(equippedObject);

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
            }


            if (Input.GetMouseButtonDown(1))
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
        if (inventory.Count > 0)
        {
            currentlyEquipped = inventory[0].itemGO;

            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].itemGO.transform.position = inventory[i].slotTransform.position;
                inventory[i].itemGO.transform.rotation = inventory[i].slotTransform.rotation;
                inventory[i].itemGO.transform.localScale = inventory[i].slotTransform.localScale;
            }
            
            Animate();
        }

        else
        {
            currentlyEquipped = null;
        }

    }

    public void PickUp(GameObject objToPickUp)
    {
        if(inventory.Count < inventoryTransforms.Length)
        {
            inventory.Add(new InventoryItem(objToPickUp, inventoryTransforms[inventory.Count]));

            playerAnimator.SetTrigger("TakeObject");
            currentlyEquipped = objToPickUp;
            currentlyEquipped.GetComponent<Rigidbody>().isKinematic = true;
            currentlyEquipped.GetComponent<PickableObjectStats>().putOnTable = false;
            currentlyEquipped.GetComponent<PickableObjectStats>().inInventory = true;
        }

        RearrangeInvPos();
    }

    public void Drop()
    {

        if (inventory.Count > 0)
        { 
            currentlyEquipped.transform.SetParent(null);

            if (!currentlyEquipped.GetComponent<PickableObjectStats>().putOnTable)
            {
                currentlyEquipped.GetComponent<Rigidbody>().isKinematic = false;
            }

            currentlyEquipped.GetComponent<Rigidbody>().velocity = (ray.direction * currentlyEquipped.GetComponent<PickableObjectStats>().Weight);
            currentlyEquipped.GetComponent<PickableObjectStats>().inInventory = false;
            currentlyEquipped.GetComponent<Collider>().isTrigger = false;
            currentlyEquipped.GetComponent<Transform>().localScale = currentlyEquipped.GetComponent<PickableObjectStats>().originalSize;
            inventory.Remove(inventory[0]);
            currentlyEquipped = null;

            playerAnimator.SetTrigger("ThrowObject");
            StartCoroutine(Wait());

            if (inventory.Count > 0)
            {
                RearrangeInvPos();
            }
        }
    }

    void Highlight(GameObject aimedObject)
    {
        normalMaterial = aimedObject.transform.GetComponent<MeshRenderer>().material;
        highlightedMaterial.mainTexture = aimedObject.GetComponent<MeshRenderer>().material.mainTexture;
        aimedObject.transform.GetComponent<MeshRenderer>().material = highlightedMaterial;
        highlighted = aimedObject.transform;
    }



    bool CanBeInteractedWith(GameObject aimedObject)
    {
            if (aimedObject.transform.GetComponent<PickableObjectStats>() || aimedObject.CompareTag("Banquet"))
            {
                return true;
            }
            else
            {
                return false;

            }
    }

    void RearrangeInvPos()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i].slotTransform = inventoryTransforms[i];
            inventory[i].itemGO.GetComponent<Collider>().isTrigger = true;
        }

        SetParents();
    }

    void SwitchInv()
    {
        GameObject tempGO = inventory[0].itemGO;

        for (int i = 0; i < inventory.Count - 1; i++)
        {
            inventory[i].itemGO = inventory[i + 1].itemGO;
        }

        inventory[inventory.Count - 1].itemGO = tempGO;

        playerAnimator.SetTrigger("ChangeObject");

        SetParents();
    }

    void SetParents()
    {
        if (inventory.Count > 0)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].itemGO.transform.SetParent(inventoryTransforms[i].gameObject.transform);
            }
        }
    }

    public void Animate()
    {
        if (inventory.Count > 0)
        {
            playerInventory.GetComponent<Animator>().Play("Floating items");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
