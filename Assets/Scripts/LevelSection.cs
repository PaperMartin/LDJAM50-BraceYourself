using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSection : MonoBehaviour
{
    [SerializeField]
    private float MaxDistance = 32f;
    private PlaneController controller;

    private void OnEnable()
    {
        controller = FindObjectOfType<PlaneController>();
    }

    private void Update()
    {
        if (transform.position.z <= controller.transform.position.z - MaxDistance)
        {
            Destroy(gameObject);
        }
    }
}
