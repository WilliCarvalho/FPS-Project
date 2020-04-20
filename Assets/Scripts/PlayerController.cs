using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Config")]
    public float speed = 5.0f;
    public float sensibility = 3.0f;
    public bool enableMouse = true;

    private PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse Lock
        if(enableMouse == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Movement
        float _movX = Input.GetAxisRaw("Horizontal");
        float _movZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _movX;
        Vector3 _moveVertical = transform.forward * _movZ;

        //Place Movement
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * -speed;
        motor.Move(_velocity);

        //Rotation
        float _mouseY = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _mouseY, 0f) * sensibility;

        if(enableMouse == true)
        {
            motor.Rotate(_rotation);
        }

        //Rotation Camera
        float _mouseX = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_mouseX, 0f, 0f) * -sensibility;

        if(enableMouse == true)
        {
            motor.RotationCamera(_cameraRotation);
        }
    }
}
