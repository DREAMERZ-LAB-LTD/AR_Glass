using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ModelViwerControler : MonoBehaviour
{
    Touch touch, stouch;

    [SerializeField] Vector2 firstTouchPosition = Vector2.zero;
    //[SerializeField] Vector2 secoendTouchPosition = Vector2.zero;

    [SerializeField] Vector2 currentFirstTouchPosition = Vector2.zero;
    [SerializeField] Vector2 currentSecoendTouchPosition = Vector2.zero;

    Vector3 objectEularAngle = Vector3.zero;

    [SerializeField] float threashHole = 10f;
    [SerializeField] float rotationSeneitivity = 10f;
    [SerializeField] float zoomSencitivity = 10f;

    [SerializeField] float minZoom = 1f;
    [SerializeField] float maxZoom = 10f;

    [SerializeField] float zoomcurrnetditance = 0f, lastdistance = 0f;


    [SerializeField] bool zooming = false;
    [SerializeField] bool canview = false;
    [SerializeField] bool rotateCamera = false;

    [SerializeField] Transform glass, cameraT;
    [SerializeField] Vector3 startGlassScale;

    [SerializeField] Text zoomDelta, scaleMagnatute;
    [SerializeField] Text zoomText, rotationText, threashHoleText;
    [SerializeField] Slider rs_slider, ss_slider;
    [SerializeField] Slider threashHoleSlider;
    [SerializeField] GameObject settingsPanal;
    
    // Start is called before the first frame update
    void Start()
    {
        startGlassScale = glass.transform.localScale;
        rs_slider.value = rotationSeneitivity;
        ss_slider.value = zoomSencitivity;
        
        threashHoleSlider.value = threashHole;

        threashHoleText.text = threashHole.ToString();
        zoomText.text = zoomSencitivity.ToString();
        rotationText.text = rotationSeneitivity.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canview)
            return;

        if (rotateCamera) 
        {

            Vector3 cameraPosition = glass.position - cameraT.forward * 10f;
            cameraT.position = cameraPosition;

            // Ensure the camera always looks at the target.
            cameraT.LookAt(glass);
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
        }
        if (Input.touchCount > 1)
        {
            stouch = Input.GetTouch(1);
            zooming = true;
        }
        else 
        {
            zooming = false;
        }

        #region Rotating

        //rotate
        if (touch.phase == TouchPhase.Began) 
        {
            firstTouchPosition = touch.position;

            objectEularAngle = glass.rotation.eulerAngles;

        }
        else if (touch.phase == TouchPhase.Moved) 
        {
            currentFirstTouchPosition = touch.position;
            
            if (!zooming) 
            {
                Vector3 delta = currentFirstTouchPosition - firstTouchPosition;

                if (rotateCamera)
                {
                    // Calculate the rotation angles based on drag distance.
                    float horizontalRotation = -delta.x * rotationSeneitivity * Time.deltaTime;
                    float verticalRotation = delta.y * rotationSeneitivity * Time.deltaTime;

                    // Rotate the pivot (this GameObject) around the target.
                    cameraT.RotateAround(glass.position, Vector3.right, verticalRotation);
                    cameraT.RotateAround(glass.position, Vector3.up, horizontalRotation);
                    cameraT.rotation = Quaternion.Euler(cameraT.eulerAngles.x, cameraT.eulerAngles.y, 0.0f);


                    firstTouchPosition = currentFirstTouchPosition;
                }
                else 
                {
                    // rotate gmae object 
                    Vector3 newRotation = objectEularAngle + new Vector3(delta.y, -delta.x, 0f) * (rotationSeneitivity/100f);
                    glass.localRotation = Quaternion.Euler(newRotation);
                }
            }

        }
        else if (touch.phase == TouchPhase.Stationary) 
        {
            //noting to do
        }
        else if (touch.phase == TouchPhase.Ended) 
        {
            //both reseting
            firstTouchPosition = Vector2.zero;
            currentSecoendTouchPosition = firstTouchPosition;

        }
        #endregion



        //zoom part
        if (stouch.phase == TouchPhase.Began)
        {
            
            lastdistance = Vector3.Distance(currentFirstTouchPosition, stouch.position);
            startGlassScale = glass.localScale;

        }
        else if (stouch.phase == TouchPhase.Moved)
        {
            currentSecoendTouchPosition = stouch.position;
            
            if (zooming)
            {
                zoomcurrnetditance = Vector2.Distance(currentFirstTouchPosition, currentSecoendTouchPosition);
                
                if (threashHole <= Mathf.Abs(zoomcurrnetditance - lastdistance))
                {
                    float sf = zoomcurrnetditance / lastdistance;
                
                    float tempScale = Mathf.Clamp(startGlassScale.x * sf * zoomSencitivity, minZoom, maxZoom);
                    
                    //glass.localScale = Vector3.one * tempScale;
                    glass.DOScale(Vector3.one * tempScale, .2f);

                    scaleMagnatute.text = glass.localScale.sqrMagnitude.ToString();
                    zoomDelta.text = glass.localScale.ToString();
                }
            }         
        }
        else if (stouch.phase == TouchPhase.Stationary)
        {

        }
        else if (stouch.phase == TouchPhase.Ended)
        {
            lastdistance = zoomcurrnetditance = 0f;
        }
        
    }

    public void CanViewTrue() 
    {
        canview = true;
    }
    public void CanViewFalse()
    {
        canview = false;
    }

    public void ResetScale() 
    {
        glass.localScale = startGlassScale;

    }

    public void RotateSencitivity() 
    {
        rotationSeneitivity = rs_slider.value;
        rotationText.text = rotationSeneitivity.ToString();
    }
    public void ScaleSencitivity() 
    {
        zoomSencitivity = ss_slider.value;
        zoomText.text = zoomSencitivity.ToString();
    }
    public void TouchThreashHole()
    {
        threashHole = threashHoleSlider.value;
        threashHoleText.text = threashHole.ToString();
    }
    public void CloseModelViewerTag()
    {
        glass.rotation = Quaternion.identity;
        glass.localScale = Vector3.one;
        
        for (int i = 0; i < glass.childCount; i++) 
        {
            glass.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void TryWithAR() 
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeActivation() 
    {
        settingsPanal.SetActive(!settingsPanal.activeInHierarchy);

    }
    public void SelectGlass(int index)
    {
        GlassSelectionManager.instance.SelectGlass(index);
    }
}
