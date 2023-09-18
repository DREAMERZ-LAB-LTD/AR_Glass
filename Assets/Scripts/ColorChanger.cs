using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] MeshRenderer glassMesh;
    [SerializeField] Material material;

    public void Onclick() 
    {
        glassMesh.material = material;
        GlassSelectionManager.instance.CurrentMat = material;
        GlassSelectionManager.instance.MeshName = glassMesh.name;
    }
}
