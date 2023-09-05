using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSponer : MonoBehaviour
{
    [SerializeField] Transform content_parent;
    [SerializeField] int count = 0;
    [SerializeField] GameObject content;
    private void Start()
    {
        for (int i = 0; i < count; i++) 
        {
            Instantiate(content, transform.position, transform.rotation, content_parent);            
        }        
    }
}
