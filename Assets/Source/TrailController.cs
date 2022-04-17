using System.Collections.Generic;
using Lissajous;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public TrailRenderer TrailRenderer;
    public TrailRenderer XTrailRenderer;
    public TrailRenderer YTrailRenderer;
    public TrailRenderer ZTrailRenderer;

    public Vector3 reference;
    public Vector3 scale;
    
    [Range(0, 10)]public float speed;

    private float _time;

    private Dictionary<LissajousParamType, float> _currentValues = new(7);

    private Matrix4x4 Matrix =>  Matrix4x4.Translate(reference) * Matrix4x4.Scale(scale);

    private void Update()
    {
        _time += Time.deltaTime * speed;
        var point = GetLissajous(_time);
        var newPoint = Matrix.MultiplyPoint3x4(point);
        TrailRenderer.transform.position = newPoint;
        XTrailRenderer.transform.position = Matrix.MultiplyPoint3x4(new Vector3(-1, point.y, point.z));
        YTrailRenderer.transform.position = Matrix.MultiplyPoint3x4(new Vector3(point.x, -1, point.z));
        ZTrailRenderer.transform.position = Matrix.MultiplyPoint3x4(new Vector3(point.x, point.y, -1));
    }

    private Vector3 GetLissajous(float t)
    {
        var x = _currentValues[LissajousParamType.A] * Mathf.Sin(t);
        var y = _currentValues[LissajousParamType.B] * Mathf.Cos(_currentValues[LissajousParamType.Beta] * t + _currentValues[LissajousParamType.Phi] * Mathf.PI);
        var z = _currentValues[LissajousParamType.C] * Mathf.Cos(_currentValues[LissajousParamType.Delta] * t + _currentValues[LissajousParamType.Psi] * Mathf.PI);
        return new Vector3(x, y, z);
    }

    public void SetValue(LissajousParamType type, float value)
    {
        if (!_currentValues.ContainsKey(type))
            _currentValues.Add(type, value);
        else
            _currentValues[type] = value;
    }

    public void SetSpeed(float value)
    {
        speed = value;
        var trailRendererTime = 2 * Mathf.PI / value;
        TrailRenderer.time = trailRendererTime;
        XTrailRenderer.time = trailRendererTime;
        YTrailRenderer.time = trailRendererTime;
        ZTrailRenderer.time = trailRendererTime;
    }
}
