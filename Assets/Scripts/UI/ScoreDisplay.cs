using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    
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
        float score = Mathf.Floor(controller.transform.position.z);
        text.text = "Score : " + score;
    }
}
