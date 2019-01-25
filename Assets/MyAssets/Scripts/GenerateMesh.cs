using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using MarchingCubesProject;

public class GenerateMesh {

    public Material m_material;
    
    Marching marching = null;

    public int width = 32;
    public int height = 32;
    public int length = 32;

    public Voxel[] voxels;

    public Mesh mesh;

    public GenerateMesh(int _width, int _height, int _length, Voxel[] _voxels)
    {
        width = _width;
        height = _height;
        length = _length;
        voxels = _voxels;
    }

    public void Generate()
    {
        List<Vector3> verts = new List<Vector3>();
        List<Color> colors = new List<Color>();
        List<Vector3> uvs = new List<Vector3>();
        List<int> indices = new List<int>();
        marching = new MarchingCubes();

        marching.GenerateWithColors(voxels, width, height, length, verts, indices, colors);

        mesh = new Mesh();
        mesh.SetVertices(verts);
        mesh.SetTriangles(indices, 0);
        mesh.colors = (colors.ToArray());
        //mesh.SetUVs(0,uvs);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    private Voxel getVoxFromVert(Vector3 vert)
    {
        Vector3 voxelPosition = new Vector3(vert.x, vert.y, vert.z);

        int x = (int)Mathf.Floor(vert.x+0.5f);
        int y = (int)Mathf.Floor(vert.y);
        int z = (int)Mathf.Floor(vert.z+0.5f);

        if(vert.x <= x+1 && vert.x >= x - 1 &&
            vert.y <= y+1 && vert.y >= y - 1 &&
            vert.z <= z+1 && vert.z >= z - 1)
        {
            return voxels[x + y * width + z * width * height];
        }
        return null;
    }
}
