using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ModelViwerControler : MonoBehaviour
{
    Touch touch, stouch;
    [SerializeField] Vector2 firstTouchPosition = Vector2.zero;
    [SerializeField] Vector2 secoendTouchPosition = Vector2.zero;
    [SerializeField] Vector2 currentFirstTouchPosition = Vector2.zero;
    [SerializeField] Vector2 currentSecoendTouchPosition = Vector2.zero;
    
    [SerializeField] float rotationDamper = 10f;
    [SerializeField] float zoomDamper = 10f;
    
    [SerializeField] float minzoom = 1f;
    [SerializeField] float maxzoom = 10f;
    [SerializeField] float zoomVariable = 0;
    [SerializeField] DebugLogUI logUI;
    [SerializeField] bool zoomOperation = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
        }
        if (Input.touchCount > 1)
        {
            stouch = Input.GetTouch(1);
            zoomOperation = true;
        }
        else 
        {
            zoomOperation = false;
        }

        //move
        if (touch.phase == TouchPhase.Began) 
        {
            firstTouchPosition = touch.position;
            logUI.SetFsXY(firstTouchPosition);
           
        }
        else if (touch.phase == TouchPhase.Moved) 
        {
            currentFirstTouchPosition = touch.position;

            logUI.setFX(currentFirstTouchPosition.x);
            logUI.setFY(currentFirstTouchPosition.y);
            
        }
        else if (touch.phase == TouchPhase.Stationary) 
        {

        }
        else if (touch.phase == TouchPhase.Ended) 
        {
            firstTouchPosition = Vector2.zero;
            currentSecoendTouchPosition = firstTouchPosition;
            logUI.SetFsXY(firstTouchPosition);

        }

        //zoom part
        if (stouch.phase == TouchPhase.Began)
        {
            secoendTouchPosition = stouch.position;
            logUI.SetSsXY(secoendTouchPosition);


        }
        else if (stouch.phase == TouchPhase.Moved)
        {
            currentSecoendTouchPosition = stouch.position;
            logUI.setSX(currentSecoendTouchPosition.x);
            logUI.setSY(currentSecoendTouchPosition.y);
        }
        else if (stouch.phase == TouchPhase.Stationary)
        {

        }
        else if (stouch.phase == TouchPhase.Ended)
        {
            secoendTouchPosition = Vector2.zero;
            currentSecoendTouchPosition = secoendTouchPosition;
            logUI.SetSsXY(secoendTouchPosition);            
        }
        //operations
        if (zoomOperation)
        {
            zoomVariable = Vector2.Distance(currentFirstTouchPosition, currentSecoendTouchPosition);
            
            logUI.SetZoom(zoomVariable);
        }
        else 
        {

        }
        
    }
}
