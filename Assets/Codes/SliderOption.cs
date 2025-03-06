using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderOption : MonoBehaviour
{
    [SerializeField] private Slider slider; // スライダー

    // Start is called before the first frame update
    void Start()
    {
        // Statics.cameraRotateSpeed = 0.9f;
        slider.value = Statics.cameraRotateSpeed;
        slider.onValueChanged.AddListener(ChangeCRS);
        Debug.Log("cameraRotateSpeed: " + slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeCRS(float value)
    {
        Statics.cameraRotateSpeed = value;
    }
}
