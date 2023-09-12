using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugLogUI : MonoBehaviour
{
    [SerializeField] Slider rotationSlider, zoomSlider, maxZoom, minZoom;
    [SerializeField] Text rotationText, zoomText, maxZoomText, minZoomText;
    [SerializeField] ModelViwerControler controler;

    public void SetUI(float rotation, float zoom, float maxZoomv, float minZoomv) 
    {
        rotationSlider.value = rotation;
        zoomSlider.value = zoom;
        rotationText.text = rotation.ToString();
        zoomText.text = zoom.ToString();


        maxZoom.value = maxZoomv;
        minZoom.value = minZoomv;

        minZoomText.text = minZoomv.ToString();
        maxZoomText.text = maxZoomv.ToString();

    }
    public void RotationValue() 
    {
        rotationText.text = rotationSlider.value.ToString();
        controler.RotateSencitivity(rotationSlider.value);
    }
    public void ZoomValue()
    {
        zoomText.text = zoomSlider.value.ToString();
        controler.ScaleSencitivity(zoomSlider.value);
    }
    public void MinZoomValue() 
    {
        minZoomText.text = minZoom.value.ToString();
        //zoomSlider.minValue = minZoom.value;
        controler.MinZoom(minZoom.value);
    }
    public void MaxZoomValue() 
    {
        maxZoomText.text = maxZoom.value.ToString();
        //zoomSlider.maxValue = maxZoom.value;
        controler.MaxZoom(maxZoom.value);
    }
}
