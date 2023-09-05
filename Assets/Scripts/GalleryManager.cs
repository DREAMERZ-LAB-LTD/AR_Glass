using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryManager : MonoBehaviour
{
    [SerializeField] GameObject mainCamera, modelViewer;
    [SerializeField] GameObject galleypanal, modelPanal;

    [SerializeField] List<GameObject> glassList;
    [SerializeField] GameObject corentGlass;
    public void ChoiceGlass(int index) 
    {
        if(corentGlass != null)
        {
            corentGlass.SetActive(false);
        }
        
        galleypanal.SetActive(false);
        mainCamera.SetActive(false);
        
        modelViewer.SetActive(true);
        glassList[index].SetActive(true);
    }
}
