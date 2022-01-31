using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalMobileController
{
    public class Player : MonoBehaviour
    {
        public float speed = 3.5f;
        public Camera MainCamera;
        private float X;
        private float Y;


        private float horizontalInput = 0f;
        private float verticalInput = 0f;
        private float movementSpeed = 2;
        private Rigidbody rb;
        [SerializeField] private FloatingJoyStick joystick;

        [SerializeField] private FloatingJoyStick Camerajoystick;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            Move();
            CameraView();
        }
        private void Move()
        {
            horizontalInput = joystick.GetHorizontalValue();
            verticalInput = joystick.GetVerticalValue();
            
            transform.Translate(Vector3.forward.normalized * horizontalInput * movementSpeed  * Time.deltaTime);
            transform.Translate(Vector3.right.normalized * - verticalInput * movementSpeed  * Time.deltaTime);
        }
        public void Jump()
        {
            rb.AddForce(Vector3.up * 150 * Time.deltaTime, ForceMode.Impulse);    
        }


        private void CameraView()
        {
            print("Camerajoystick.GetHorizontalValue(): " + Camerajoystick.GetHorizontalValue());

            MainCamera.transform.Rotate(new Vector3(-Camerajoystick.GetVerticalValue() * speed, Camerajoystick.GetHorizontalValue() * speed, 0));
            X = MainCamera.transform.rotation.eulerAngles.x;
            Y = MainCamera.transform.rotation.eulerAngles.y;
            
            if(X > 89)
            {
                
            }

            MainCamera.transform.rotation = Quaternion.Euler(X, Y, 0);

        }
    }
}
