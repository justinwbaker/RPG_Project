using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {

    public Material mat;

    public int width = 32;
    public int height = 32;
    public int length = 32;

    public float[] voxels;

    public static float seed = 29384756;

    GameObject meshObject;

    GenerateMesh meshGenerator;
    // Use this for initialization
    void Start () {

        voxels = new float[width * height * length];

        StartCoroutine("GenerateTerrain");
    }
    float ind = 0;
	// Update is called once per frame
	void Update () {
        ind++;
	}

    public void Generate()
    {
        for(int x=0; x< width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                float xCoord = ((float)x + transform.position.x + (float)seed) / 100f;
                float yCoord = ((float)z + transform.position.z + (float)seed) / 100f;
                float sample = Mathf.PerlinNoise(xCoord, yCoord) * height;
                for (int y = 0; y < sample; y++)
                {
                    int idx = x + y * width + z * width * height;
                    voxels[idx] = 1f;
                }
            }
        }
    }

    public IEnumerator GenerateTerrain()
    {

        if (meshObject == null)
        {
            Generate();
            meshObject = new GameObject("Mesh");
            meshObject.transform.parent = transform;
            meshObject.transform.localPosition = new Vector3(0, 0, 0);
            meshObject.AddComponent<MeshFilter>();
            meshObject.AddComponent<MeshRenderer>();
            meshObject.GetComponent<Renderer>().material = mat;
        }

        meshGenerator = new GenerateMesh(width, height, length, voxels);
        meshGenerator.Generate();

        meshObject.GetComponent<MeshFilter>().mesh = meshGenerator.mesh;
        Destroy(meshObject.GetComponent<MeshCollider>());
        meshObject.AddComponent<MeshCollider>();

        yield return null;
    }

    public void removeVoxel(int x, int y, int z)
    {
        int idx = x + y * width + z * width * height;
        voxels[idx] = 0f;
        StartCoroutine("GenerateTerrain");
    }

    public void addVoxel(int x, int y, int z)
    {
        int idx = x + y * width + z * width * height;
        voxels[idx] = 1f;
        StartCoroutine("GenerateTerrain");
    }
}
