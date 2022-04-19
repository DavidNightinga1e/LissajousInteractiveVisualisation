using System;
using UnityEngine;

namespace Lissajous
{
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
    public class CanvasHider : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        
        private CanvasGroup _canvasGroup;
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            if (!camera)
                camera = Camera.main;
        }

        private void Update()
        {
            var dot = Vector3.Dot(camera.transform.position - _canvas.transform.position, _canvas.transform.forward);
            var isHidden = dot >= 0;
            _canvasGroup.alpha = isHidden ? 0f : 1f;
        }
    }
}