using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject currentlyEquipped;
    Ray ray;
    private Transform highlighted;

    public Material normalMaterial;
    public Material highlightedMaterial;

    //Update with new selectableObjectTags
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

        Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50))
        {
            for(int i = 0; i < selectableTags.Length; i++)
            {
                if(hit.transform.CompareTag(selectableTags[i]))
                {
                    normalMaterial = hit.transform.GetComponent<MeshRenderer>().material;
                    hit.transform.GetComponent<MeshRenderer>().material = highlightedMaterial;
                    highlighted = hit.transform;
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
            if(Input.GetMouseButtonDown(0))
            {
                if(currentlyEquipped != null)
                {
                    Drop();
                }
            }
        }
    }

    void PickUp(GameObject objToPickUp)
    {
        if(currentlyEquipped != null)
        {
            currentlyEquipped.SetActive(true);
            currentlyEquipped = null;
            Debug.Log("Got back the object");
        }

            currentlyEquipped = objToPickUp;
            objToPickUp.SetActive(false);
    }

    void Drop()
    {
        currentlyEquipped.gameObject.SetActive(true);
        currentlyEquipped = null;
    }

}
