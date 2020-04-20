using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject currentlyEquipped;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50))
        {
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
    }

    void PickUp(GameObject objToPickUp)
    {
        currentlyEquipped = objToPickUp;
        objToPickUp.SetActive(false);
    }

}
