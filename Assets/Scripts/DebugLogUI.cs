using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLogUI : MonoBehaviour
{
    [SerializeField] Text Fs_X, Fs_Y;
    [SerializeField] Text F_X, F_Y;
    [SerializeField] Text Ss_X, Ss_Y;
    [SerializeField] Text S_X, S_Y;
    [SerializeField] Text dostamce;
    public void setFX(float value) 
    {
        F_X.text = value.ToString();
    }
    public void setFY(float value)
    {
        F_Y.text = value.ToString();
    }
    public void setSX(float value)
    {
        S_X.text = value.ToString();
    }
    public void setSY(float value)
    {
        S_Y.text = value.ToString();
    }
    public void SetFsXY(Vector2 value) 
    {
        Fs_X.text = value.x.ToString();
        Fs_Y.text = value.y.ToString();
    }
    public void SetSsXY(Vector2 value) 
    {
        Ss_X.text = value.x.ToString();
        Ss_Y.text = value.y.ToString();
    }
    public void SetZoom(float value) 
    {
        dostamce.text = value.ToString();
    }
}
