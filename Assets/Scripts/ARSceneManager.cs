using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
public class ARSceneManager : MonoBehaviour
{
    [SerializeField] ARFaceManager faceManager;
    [SerializeField] GameObject issue;
    private void OnEnable()
    {
        GameObject glass = GlassSelectionManager.instance.GetCurrentGlass();
        
        MeshRenderer[] meshRenderers = glass.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer item in meshRenderers) 
        {
            if (item.name == GlassSelectionManager.instance.MeshName) 
            {
                item.material = GlassSelectionManager.instance.CurrentMat;
            }
        }

        if (glass != null) 
        {
            faceManager.facePrefab = glass;
        }
        else
        {
            issue.SetActive(true);
        }
    }
    public void LoadUIScene() 
    {
        SceneManager.LoadScene(0);
    }
}
