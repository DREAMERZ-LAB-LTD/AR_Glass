using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlassSelectionManager : MonoBehaviour
{
    [SerializeField] GameObject currentGlass;
    [SerializeField] List<GameObject> glasses;

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
