using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneRaisingBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private PlaneController controller;

    private void OnEnable() {
        controller = FindObjectOfType<PlaneController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = controller.CurrentPlaneRaisingCharge;
    }
}
