using System;
using UnityEngine;

namespace Lissajous
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [Range(0, 100)] public float xSensitivity; 
        [Range(0, 100)] public float ySensitivity; 
        
        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                transform.RotateAround(target.position, target.up, Input.GetAxis("Mouse X") * Time.deltaTime * xSensitivity);
                transform.RotateAround(target.position, transform.right, -Input.GetAxis("Mouse Y") * Time.deltaTime * ySensitivity);
            }
        }
    }
}