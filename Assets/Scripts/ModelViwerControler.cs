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
    [SerializeField] bool startTouching = false;
    [SerializeField] bool rotateCamera = false;

    [SerializeField] Transform glass, cameraT;
    [SerializeField] Vector3 startGlassScale;

    [SerializeField] GameObject settingsPanal;
    [SerializeField] DebugLogUI dlui;

    [SerializeField] Text count, price;

    [SerializeField] Animator animatior;
    [SerializeField] float counter = 0f;

    [SerializeField] GameObject modelpanal;

    // Start is called before the first frame update
    void Start()
    {
        startGlassScale = glass.transform.localScale;
        dlui.SetUI(rotationSeneitivity, zoomSencitivity, maxZoom, minZoom);
    }

    // Update is called once per frame
    void Update()
    {
        if (!modelpanal.activeInHierarchy) 
        {
            counter = 0f;
            return;
        }


        if (counter <= 5f)
        {
            counter += Time.deltaTime;
        }        
        if (counter > 5f)
        {
            animatior.gameObject.SetActive(true);
        }
       
        if (!startTouching)
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
            animatior.gameObject.SetActive(false);
            counter = 0f;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            currentFirstTouchPosition = touch.position;

            if (!zooming)
            {
                Vector3 delta = currentFirstTouchPosition - firstTouchPosition;

                if (rotateCamera)
                {
                    // Calculate the currentRotation angles based on drag distance.
                    float horizontalRotation = -delta.x * rotationSeneitivity * Time.deltaTime;
                    float verticalRotation = delta.y * rotationSeneitivity * Time.deltaTime;

                    // Rotate the pivot (this GameObject) around the target.
                    cameraT.RotateAround(glass.position, Vector3.right, verticalRotation);
                    cameraT.RotateAround(glass.position, Vector3.up, horizontalRotation);
                    //cameraT.currentRotation = Quaternion.Euler(cameraT.eulerAngles.x, cameraT.eulerAngles.y, 0.0f);


                    firstTouchPosition = currentFirstTouchPosition;
                }
                else
                {
                    // rotate gmae object 
                    Vector3 newRotation = objectEularAngle + new Vector3(0f, -delta.x, 0f) * (rotationSeneitivity / 100f);
                    //Vector3 newRotation = objectEularAngle + new Vector3(delta.y, -delta.x, 0f) * (rotationSeneitivity/100f);
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
                float temp = (zoomcurrnetditance - lastdistance);
                
                if ( temp >= 0)
                {
                    //float tempScale = Mathf.Clamp(startGlassScale.x + (Time.deltaTime * zoomSencitivity), minZoom, maxZoom);
                    glass.DOScale(Vector3.one * Mathf.Clamp(glass.localScale.x + (Time.deltaTime * zoomSencitivity), minZoom, maxZoom), Time.deltaTime);
                }
                else 
                {
                    //float tempScale = Mathf.Clamp(startGlassScale.x - (Time.deltaTime * zoomSencitivity), minZoom, maxZoom);
                    glass.DOScale(Vector3.one * Mathf.Clamp(glass.localScale.x - (Time.deltaTime * zoomSencitivity), minZoom, maxZoom), Time.deltaTime);
                }

                lastdistance = zoomcurrnetditance;
                
                //if (threashHole <= Mathf.Abs(zoomcurrnetditance - lastdistance))
                //{
                //    //float sf = zoomcurrnetditance / lastdistance;

                //    //float tempScale = Mathf.Clamp(startGlassScale.x * sf * zoomSencitivity, minZoom, maxZoom);

                //    //glass.localScale = Vector3.one * tempScale;

                //}
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
        startTouching = true;
    }
    public void CanViewFalse()
    {
        startTouching = false;
    }

    public void ResetScale()
    {
        glass.localScale = Vector3.one;
        glass.rotation = Quaternion.identity;

    }

    public void RotateSencitivity(float value)
    {
        rotationSeneitivity = value;
        //rotationText.text = rotationSeneitivity.ToString();
    }
    public void ScaleSencitivity(float value)
    {
        zoomSencitivity = value;
        //zoomText.text = zoomSencitivity.ToString();
    }
    public void TouchThreashHole(float value)
    {
        threashHole = value;
        //threashHoleText.text = threashHole.ToString();
    }
    public void MaxZoom(float value)
    {
        maxZoom = value;
    }
    public void MinZoom(float value) 
    {
        minZoom = value;
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
        glass.GetChild(index).gameObject.SetActive(true);
        GlassSelectionManager.instance.SelectGlass(index);
    }

    private int count_V = 01;
    private int price_V = 350;
    public void ADDCount()
    {
        count_V++;
        count.text = count_V.ToString();
        //price
        UpdatePice();
    }
    public void RemoveCount() 
    {
        if (count_V <= 1)
            return;

        count_V--;
        count.text = count_V.ToString();
        UpdatePice();
    }
    public void UpdatePice() 
    {
        price_V = 350 * count_V;
        price.text = "$"+price_V.ToString();
    }
}
