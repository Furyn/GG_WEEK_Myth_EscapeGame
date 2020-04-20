using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private GameObject currentlyEquipped;
    private Ray ray;
    private Transform highlighted;
    private Material normalMaterial;

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
            for(int i = 0; i < selectableTags.Length; i++)
            {
                if(hit.transform.CompareTag(selectableTags[i]))
                {
                    normalMaterial = hit.transform.GetComponent<MeshRenderer>().material;
                    hit.transform.GetComponent<MeshRenderer>().material = highlightedMaterial;
                    highlighted = hit.transform;
                }

                else
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        if (currentlyEquipped != null)
                        {
                            Drop();
                        }
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.gameObject.CompareTag("Lock"))
                {
                    if(currentlyEquipped != null)
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
        }

        else
        {
            if(Input.GetMouseButtonDown(1))
            {
                if(currentlyEquipped != null)
                {
                    Drop();
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (currentlyEquipped != null)
        {
            currentlyEquipped.transform.position = playerHand.position;
            currentlyEquipped.transform.rotation = playerHand.rotation;
            currentlyEquipped.transform.SetParent(this.transform);
        }
    }

    void PickUp(GameObject objToPickUp)
    {
        if(currentlyEquipped != null)
        {
            Drop();
            Debug.Log("Got back the object");
        }

            objToPickUp.transform.position = playerHand.position;
            currentlyEquipped = objToPickUp;
            currentlyEquipped.GetComponent<Rigidbody>().isKinematic = true;
            
    }

    void Drop()
    {
        currentlyEquipped.transform.SetParent(null);
        currentlyEquipped.GetComponent<Rigidbody>().isKinematic = false;
        currentlyEquipped = null;
    }

}
