using System;
using UnityEngine;

public class FPS_Camera : MonoBehaviour {

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerBody;
    [SerializeField] private int maxAngle;

    private float xRotation;

    public static FPS_Camera instance;

    private void Awake()
    {
        instance = this;
    }

    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    

    public void Rotate(float mouseX, float mouseY)
    {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxAngle, maxAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up*mouseX);
    }
}
