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
