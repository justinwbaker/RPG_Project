using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MarchingCubesProject
{
    public abstract class Marching : IMarching
    {

        public float Surface { get; set; }

        private float[] Cube { get; set; }
        private Voxel[] Vox { get; set; }

        /// <summary>
        /// Winding order of triangles use 2,1,0 or 0,1,2
        /// </summary>
        protected int[] WindingOrder { get; private set; }

        public Marching(float surface = 0.5f)
        {
            Surface = surface;
            Cube = new float[8];
            Vox = new Voxel[8];
            WindingOrder = new int[] { 0, 1, 2 };
        }

        public virtual void Generate(IList<Voxel> voxels, int width, int height, int depth, IList<Vector3> verts, IList<int> indices)
        {
            if (Surface > 0.0f)
            {
                WindingOrder[0] = 0;
                WindingOrder[1] = 1;
                WindingOrder[2] = 2;
            }
            else
            {
                WindingOrder[0] = 2;
                WindingOrder[1] = 1;
                WindingOrder[2] = 0;
            }

            int x, y, z, i;
            int ix, iy, iz;
            for (x = 0; x < width - 1; x++)
            {
                for (y = 0; y < height - 1; y++)
                {
                    for (z = 0; z < depth - 1; z++)
                    {
                        //Get the values in the 8 neighbours which make up a cube
                        for (i = 0; i < 8; i++)
                        {
                            ix = x + VertexOffset[i, 0];
                            iy = y + VertexOffset[i, 1];
                            iz = z + VertexOffset[i, 2];

                            Cube[i] = voxels[ix + iy * width + iz * width * height].isColldable?1:0;
                        }

                        //Perform algorithm
                        March(x, y, z, Cube, verts, indices);
                    }
                }
            }

        }

        public virtual void GenerateWithColors(IList<Voxel> voxels, int width, int height, int depth, IList<Vector3> verts, IList<int> indices, IList<Color> colors)
        {

            if (Surface > 0.0f)
            {
                WindingOrder[0] = 0;
                WindingOrder[1] = 1;
                WindingOrder[2] = 2;
            }
            else
            {
                WindingOrder[0] = 2;
                WindingOrder[1] = 1;
                WindingOrder[2] = 0;
            }

            int x, y, z, i;
            int ix, iy, iz;
            for (x = 0; x < width - 1; x++)
            {
                for (y = 0; y < height - 1; y++)
                {
                    for (z = 0; z < depth - 1; z++)
                    {
                        //Get the values in the 8 neighbours which make up a cube
                        for (i = 0; i < 8; i++)
                        {
                            ix = x + VertexOffset[i, 0];
                            iy = y + VertexOffset[i, 1];
                            iz = z + VertexOffset[i, 2];
                            Vox[i] = voxels[ix + iy * width + iz * width * height];
                        }
                        //Perform algorithm
                        MarchWithColors(x, y, z, Vox, verts, indices, colors);
                    }
                }
            }

        }

        /// <summary>
        /// MarchCube performs the Marching algorithm on a single cube
        /// </summary>
        protected abstract void March(float x, float y, float z, float[] cube, IList<Vector3> vertList, IList<int> indexList);
        protected abstract void MarchWithColors(float x, float y, float z, Voxel[] vox, IList<Vector3> vertList, IList<int> indexList, IList<Color> colors);

        /// <summary>
        /// GetOffset finds the approximate point of intersection of the surface
        /// between two points with the values v1 and v2
        /// </summary>
        protected virtual float GetOffset(float v1, float v2)
        {
            float delta = v2 - v1;
            return (delta == 0.0f) ? Surface : (Surface - v1) / delta;
        }

        /// <summary>
        /// VertexOffset lists the positions, relative to vertex0, 
        /// of each of the 8 vertices of a cube.
        /// vertexOffset[8][3]
        /// </summary>
        protected static readonly int[,] VertexOffset = new int[,]
	    {
	        {0, 0, 0},{1, 0, 0},{1, 1, 0},{0, 1, 0},
	        {0, 0, 1},{1, 0, 1},{1, 1, 1},{0, 1, 1}
	    };

    }

}
