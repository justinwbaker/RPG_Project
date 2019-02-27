using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeble_Object : MonoBehaviour {

    public List<Transform> positions;
    public List<GameObject> objects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void placeObject(GameObject obj)
    {
        if(objects.Count < positions.Count)
        {
            obj.transform.parent = null;
            obj.transform.localPosition = positions[objects.Count].position;
            obj.transform.localRotation = positions[objects.Count].rotation;
            Destroy(obj.GetComponent<Rigidbody>());
        }
    }
}
