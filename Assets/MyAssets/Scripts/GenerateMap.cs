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
        Generate();

        meshGenerator = new GenerateMesh(width, height, length, voxels);
        meshGenerator.Generate();

        GameObject go = new GameObject("Mesh");
        go.transform.parent = transform;
        go.AddComponent<MeshFilter>();
        go.AddComponent<MeshRenderer>();
        go.GetComponent<Renderer>().material = mat;
        go.GetComponent<MeshFilter>().mesh = meshGenerator.mesh;
        go.transform.localPosition = new Vector3(0, 0, 0);
        go.AddComponent<MeshCollider>();
        yield return null;
    }
}
