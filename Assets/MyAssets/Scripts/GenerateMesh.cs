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

    public float[] voxels;

    public Mesh mesh;

    public GenerateMesh(int _width, int _height, int _length, float[] _voxels)
    {
        width = _width;
        height = _height;
        length = _length;
        voxels = _voxels;
    }

    public void Generate()
    {
        List<Vector3> verts = new List<Vector3>();
        List<int> indices = new List<int>();
        marching = new MarchingCubes();

        marching.Generate(voxels, width, height, length, verts, indices);

        mesh = new Mesh();
        mesh.SetVertices(verts);
        mesh.SetTriangles(indices, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}
