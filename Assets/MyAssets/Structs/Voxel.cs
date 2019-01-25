using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Voxel", menuName = "Ouroboros/Voxels/voxel")]
public class Voxel : ScriptableObject {

    public bool isColldable;
    public bool isLiquid;
    public bool connects;

    public List<string> exceptions;

    public Color color;

}
