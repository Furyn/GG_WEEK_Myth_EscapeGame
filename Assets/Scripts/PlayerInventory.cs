using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private GameObject currentlyEquipped;
    Ray ray;
    private Transform highlighted;
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
            highlighted.GetComponent<MeshRenderer>().material.color = Color.white;
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
                    hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }

            highlighted = hit.transform;

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

        if(highlighted == null)
        {

        }
    }

    void PickUp(GameObject objToPickUp)
    {
        currentlyEquipped = objToPickUp;
        objToPickUp.SetActive(false);
    }

}
