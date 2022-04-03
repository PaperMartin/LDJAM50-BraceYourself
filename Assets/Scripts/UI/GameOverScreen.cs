using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    //[SerializeField]
    //private Button button;

    private void Start() {
        text.text = "Score : " + Mathf.Floor(PlaneGameManager.Instance.Score);;
        //button.Select();
    }
}
