using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlassSelectionManager : MonoBehaviour
{
    [SerializeField] GameObject currentGlass;
    [SerializeField] List<GameObject> glasses;
    [SerializeField] Material currentmat;
    [SerializeField] string meshIndex;
    public Material CurrentMat { set { currentmat = value; } get { return currentmat; } }
    public string MeshName { set { meshIndex = value; } get { return meshIndex; } }

    public static GlassSelectionManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }      
    }


    public void SelectGlass(int index) 
    {
        if (index < glasses.Count) 
        {
            currentGlass = glasses[index];
            
        } 
    }

    public GameObject GetCurrentGlass() 
    {
        return currentGlass;
    }
}
