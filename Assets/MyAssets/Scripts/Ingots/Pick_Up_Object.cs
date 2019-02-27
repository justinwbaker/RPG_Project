using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Up_Object : MonoBehaviour {

    public float range = 1f;

    public Transform L_Hand;
    public Transform R_Hand;

    private GameObject L_Hand_Obj;
    private GameObject R_Hand_Obj;

    private Transform cam;

    private Placeble_Object looking = null;
    private Vector3 lookingPoint;

	// Use this for initialization
	void Start () {
        cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        updateLookingPoint();
        looking = null;
        if (Input.GetButtonDown("Fire1"))
        {
            if (L_Hand_Obj == null)
            {
                GameObject cast = raycastObject();
                if (cast != null)
                {

                    L_Hand_Obj = cast;
                    PickUp(L_Hand_Obj, L_Hand);
                }
            }
            else
            {
                if (Input.GetButton("Throw"))
                {
                    Throw(L_Hand_Obj);
                    L_Hand_Obj = null;
                }
                else
                {
                    lookingObject();
                    if (looking != null)
                    {
                        looking.placeObject(L_Hand_Obj);
                        L_Hand_Obj = null;
                    }
                    else
                    {
                        if (Drop(L_Hand_Obj))
                        {
                            L_Hand_Obj = null;
                        }
                    }
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (R_Hand_Obj == null)
            {
                GameObject cast = raycastObject();
                if (cast != null)
                {
                    R_Hand_Obj = cast;
                    PickUp(R_Hand_Obj, R_Hand);
                }
            }
            else
            {

                if (Input.GetButton("Throw"))
                {
                    Throw(R_Hand_Obj);
                    R_Hand_Obj = null;
                }
                else
                {
                    lookingObject();
                    if (looking != null)
                    {
                        looking.placeObject(R_Hand_Obj);
                        R_Hand_Obj = null;
                    }
                    else
                    {
                        if (Drop(R_Hand_Obj))
                        {
                            R_Hand_Obj = null;
                        }
                    }
                }
            }
        }
    }

    private GameObject raycastObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            if (hit.transform.gameObject.tag == "PickUp")
            {
                return hit.transform.gameObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    private void lookingObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            if (hit.transform.gameObject.tag == "Placeable")
            {
                looking = hit.transform.gameObject.GetComponent<Placeble_Object>();
            }
            else
            {
                looking = null;
            }
        }
        else
        {
            looking = null;
        }
    }

    private void PickUp(GameObject obj, Transform hand)
    {
        obj.transform.parent = hand;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        Destroy(obj.GetComponent<Rigidbody>());
    }

    private bool Drop(GameObject handObj)
    {
        if (Vector3.Distance(transform.position, lookingPoint) < range)
        {
            handObj.transform.parent = null;
            handObj.transform.localPosition = lookingPoint;
            Rigidbody rb = handObj.AddComponent<Rigidbody>();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Throw(GameObject handObj)
    {
        handObj.transform.parent = null;
        Rigidbody rb = handObj.AddComponent<Rigidbody>();
        rb.velocity = handObj.transform.forward * 2f + handObj.transform.up * 2f;
    }

    private void updateLookingPoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            lookingPoint = hit.point;
        }
        else
        {
            lookingPoint = transform.forward * Mathf.Infinity;
        }
    }
}