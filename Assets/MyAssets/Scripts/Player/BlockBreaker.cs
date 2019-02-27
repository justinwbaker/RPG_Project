using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreaker : MonoBehaviour {

    public float range = 10f;

    Transform cam;
    public Transform cube;

	// Use this for initialization
	void Start () {
        cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SelectionCubeUpdate();
        addCubeUpdate();
        RemoveCubeUpdate();
    }

    private void SelectionCubeUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            Debug.DrawRay(cam.position, cam.forward * hit.distance, Color.yellow);
            if (hit.transform.parent.gameObject.GetComponent<GenerateMap>())
            {
                GenerateMap MapGenerator = hit.transform.parent.gameObject.GetComponent<GenerateMap>();
                Vector3 voxelPosition = new Vector3(Mathf.Round(hit.point.x),
                                                    Mathf.Floor(hit.point.y),
                                                    Mathf.Round(hit.point.z));

                cube.gameObject.SetActive(true);
                cube.position = voxelPosition;
            }
            else
            {
                cube.gameObject.SetActive(false);
            }
        }
        else
        {
            cube.gameObject.SetActive(false);
        }
    }

    public void RemoveCubeUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(cam.position, cam.forward, out hit, range))
            {
                Debug.DrawRay(cam.position, cam.forward * hit.distance, Color.yellow);
                Debug.Log(hit.transform.parent.gameObject.name);
                if (hit.transform.parent.gameObject.GetComponent<GenerateMap>())
                {
                    GenerateMap MapGenerator = hit.transform.parent.gameObject.GetComponent<GenerateMap>();
                    Vector3 voxelPosition = new Vector3(hit.point.x - hit.transform.position.x,
                                                        hit.point.y - hit.transform.position.y,
                                                        hit.point.z - hit.transform.position.z);
                    MapGenerator.removeVoxel((int)Mathf.Round(voxelPosition.x), (int)Mathf.Floor(voxelPosition.y), (int)Mathf.Round(voxelPosition.z));
                }
            }
        }
    }

    public void addCubeUpdate()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(cam.position, cam.forward, out hit, range))
            {
                Debug.DrawRay(cam.position, cam.forward * hit.distance, Color.yellow);
                if (hit.transform.parent.gameObject.GetComponent<GenerateMap>())
                {
                    GenerateMap MapGenerator = hit.transform.parent.gameObject.GetComponent<GenerateMap>();
                    Vector3 voxelPosition = new Vector3(hit.point.x - hit.transform.position.x,
                                                        hit.point.y - hit.transform.position.y,
                                                        hit.point.z - hit.transform.position.z);
                    MapGenerator.addVoxel((int)Mathf.Round(voxelPosition.x), (int)Mathf.Ceil(voxelPosition.y), (int)Mathf.Round(voxelPosition.z));
                }
            }
        }
    }
}
