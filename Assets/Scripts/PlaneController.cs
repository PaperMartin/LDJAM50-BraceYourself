using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlaneController : MonoBehaviour
{
    [SerializeField]
    private float HorizontalSpeed = 2.5f;
    [SerializeField]
    private float ForwardSpeed = 2.5f;
    [SerializeField]
    private float Gravity = 5f;
    [SerializeField]
    private Vector2 HorizontalBounds = new Vector2(-5, 5);
    [SerializeField]
    private float PlaneRotationSpeed = 2f;
    [SerializeField]
    private Vector2 PlaneMinMaxAngle = new Vector2(-5f, 15f);
    [SerializeField]
    private float PlaneUpwardForce;
    [SerializeField]
    private float WindAcceleration = 5f;
    [SerializeField]
    private UnityEvent OnMove;
    [SerializeField]
    private UnityEvent OnCrash;

    public enum PlaneState
    {
        Flying,
        Crashed
    }
    public PlaneState CurrentState { get; private set; } = PlaneState.Flying;
    public float HorizontalMovement { get; private set; } = 0;
    public float PlaneRaisingVector { get; private set; } = 0;


    private Rigidbody rigidbody;
    private Vector3 Velocity = Vector3.zero;
    private float PlaneRaisingInput;

    private List<WindVolume> WindVolumes = new List<WindVolume>();
    private Vector2 CurrentWindSpeed = Vector2.zero;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void OnUpdateHorizontalInput(InputAction.CallbackContext context)
    {
        HorizontalMovement = context.ReadValue<float>();
    }

    public void OnUpdatePlaneRaisingInput(InputAction.CallbackContext context)
    {
        PlaneRaisingInput = context.ReadValue<float>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CurrentState == PlaneState.Flying)
        {
            UpdateMovement();
        }
    }

    private void UpdateMovement()
    {
        UpdateAngleFactor();
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.x = Mathf.Lerp(PlaneMinMaxAngle.x, PlaneMinMaxAngle.y, PlaneRaisingVector);
        Quaternion rot = Quaternion.Euler(eulerAngles);
        rigidbody.MoveRotation(rot);

        UpdateWindStrength();
        Velocity.x = HorizontalMovement * HorizontalSpeed * Time.deltaTime;
        Velocity.y = Time.deltaTime * Gravity;
        Velocity.y += (PlaneUpwardForce + CurrentWindSpeed.y) * Time.deltaTime * PlaneRaisingVector;
        Velocity.z = (ForwardSpeed + CurrentWindSpeed.x) * Time.deltaTime;
        Vector3 TargetPos = rigidbody.position + Velocity;
        TargetPos.x = Mathf.Clamp(TargetPos.x, HorizontalBounds.x, HorizontalBounds.y);
        rigidbody.MovePosition(TargetPos);
        OnMove.Invoke();
    }

    private void UpdateAngleFactor()
    {
        PlaneRaisingVector = Mathf.MoveTowards(PlaneRaisingVector, PlaneRaisingInput, PlaneRotationSpeed * Time.deltaTime);
    }

    private void UpdateWindStrength()
    {
        Vector2 WindStrength = Vector2.zero;
        for (int i = 0; i < WindVolumes.Count; i++)
        {
            WindStrength = Vector2.Max(WindStrength, WindVolumes[i].GetWindStrength());
        }
        CurrentWindSpeed = Vector2.MoveTowards(CurrentWindSpeed, WindStrength, WindAcceleration * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            WindVolume windZone = other.attachedRigidbody.GetComponent<WindVolume>();
            if (windZone != null)
            {
                WindVolumes.Add(windZone);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            WindVolume windZone = other.attachedRigidbody.GetComponent<WindVolume>();
            if (WindVolumes.Contains(windZone))
            {
                WindVolumes.Remove(windZone);
            }
        }
    }
}
