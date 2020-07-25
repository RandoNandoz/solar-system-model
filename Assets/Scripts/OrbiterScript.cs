using UnityEngine;

public class OrbiterScript : MonoBehaviour
{
    private GameObject _orbitalObj;
    private Transform _myTransform;
    private Vector3 _objLocation;
    [SerializeField] private string objectName;
    
    
    [SerializeField] private float rotateSpeed = 40.0f;
    // Start is called before the first frame update
    void Start()
    {
        _orbitalObj = GameObject.Find(objectName);
        _myTransform = GetComponent<Transform>();
        if (_orbitalObj != null)
        {
            _objLocation = _orbitalObj.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _myTransform.RotateAround(_objLocation, Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
