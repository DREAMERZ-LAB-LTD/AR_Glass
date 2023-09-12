using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DiscriptionUI : MonoBehaviour
{
    [SerializeField] float currentRotation = 0f;
    [SerializeField] float minRotation = 0f, maxRotation = 180f;

    [SerializeField] float currentScale = 200f;
    [SerializeField] float minScale = 70f, maxScale = 200f;
    
    [SerializeField] Transform button;
    [SerializeField] RectTransform decription;
    [SerializeField] float time = .5f;

    public void OnClick() 
    {
        button.DOLocalRotate(Vector3.forward * currentRotation, time, RotateMode.Fast).OnComplete(() => {
            if (currentRotation <= minRotation)
            {
                currentRotation = maxRotation;
            }
            else 
            {
                currentRotation = minRotation;
            }
        });
        decription.DOSizeDelta(new Vector2(decription.sizeDelta.x, currentScale), time, false).OnComplete(() => {
            
            if (currentScale <= minScale) 
            {
                currentScale = maxScale;
            }
            else {
                currentScale = minScale;
            }
        });
    }
}
