using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    

    private void OnEnable() {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float score = Mathf.Floor(PlaneGameManager.Instance.Score);
        text.text = "Score : " + score;
    }
}
