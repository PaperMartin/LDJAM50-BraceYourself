using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindVolume : MonoBehaviour
{
    [SerializeField]
    private Vector2 WindStrength = new Vector2(5f,2.5f);

    public Vector2 GetWindStrength(){
        return WindStrength;
    }
}
