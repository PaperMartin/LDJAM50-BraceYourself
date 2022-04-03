using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource EngineAudioSource;
    [SerializeField]
    private Vector2 EnginePitchMinMax;
    PlaneController controller;

    private void OnEnable() {
        controller = GetComponent<PlaneController>();
    }

    public void OnMove(){
        EngineAudioSource.pitch = Mathf.Lerp(EnginePitchMinMax.x,EnginePitchMinMax.y,controller.PlaneRaisingVector);
    }
}
