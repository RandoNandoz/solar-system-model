using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private readonly float camSens = 0.10f;

    private Vector3
        _lastMouse = new Vector3(255, 255, 255);

    private readonly float mainSpeed = 50.0f; 
    private readonly float maxShift = 100.0f; 
    private readonly float shiftAdd = 25.0f;
    private float _totalRun = 1.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        _lastMouse = Input.mousePosition - _lastMouse;
        _lastMouse = new Vector3(-_lastMouse.y * camSens, _lastMouse.x * camSens, 0);
        var transform2 = transform;
        var eulerAngles = transform2.eulerAngles;
        _lastMouse = new Vector3(eulerAngles.x + _lastMouse.x, eulerAngles.y + _lastMouse.y, 0);
        eulerAngles = _lastMouse;
        transform2.eulerAngles = eulerAngles;
        _lastMouse = Input.mousePosition;
        
        var p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _totalRun += Time.deltaTime;
            p = p * (_totalRun * shiftAdd);
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            _totalRun = Mathf.Clamp(_totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }

        p = p * Time.deltaTime;
        var newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space))
        {
            Transform transform1;
            (transform1 = transform).Translate(p);
            var position = transform1.position;
            newPosition.x = position.x;
            newPosition.z = position.z;
            position = newPosition;
            transform1.position = position;
        }
        else
        {
            transform.Translate(p);
        }
    }

    private Vector3 GetBaseInput()
    {
        var pVelocity = new Vector3();
        if (Input.GetKey(KeyCode.W)) pVelocity += new Vector3(0, 0, 1);
        if (Input.GetKey(KeyCode.S)) pVelocity += new Vector3(0, 0, -1);
        if (Input.GetKey(KeyCode.A)) pVelocity += new Vector3(-1, 0, 0);
        if (Input.GetKey(KeyCode.D)) pVelocity += new Vector3(1, 0, 0);
        if (Input.GetKey(KeyCode.Escape)) Cursor.lockState = CursorLockMode.None;
        if (Input.GetKey(KeyCode.Tab)) Cursor.lockState = CursorLockMode.Confined;
        return pVelocity;
    }
}