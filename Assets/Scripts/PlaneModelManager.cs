using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlaneController))]
public class PlaneModelManager : MonoBehaviour
{
    PlaneController controller;
    [SerializeField]
    private float HorizontalRotation;
    [SerializeField]
    private float RotationChangeSpeed = 2f;
    [SerializeField]
    private Transform ModelPivot;

    private float CurrentHorizontalMovementFactor = 0;

    private void OnEnable() {
        controller = GetComponent<PlaneController>();
    }

    public void OnMove(){
        UpdateHorizontalMovementFactor();
        float horizontalRot = -CurrentHorizontalMovementFactor * HorizontalRotation;
        Vector3 angle = ModelPivot.eulerAngles;
        angle.z = horizontalRot;
        ModelPivot.eulerAngles = angle;
    }

    private void UpdateHorizontalMovementFactor(){
        CurrentHorizontalMovementFactor = Mathf.MoveTowards(CurrentHorizontalMovementFactor,controller.HorizontalMovement,RotationChangeSpeed * Time.deltaTime);
    }
}
