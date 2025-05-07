using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    #region Variables

    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    #endregion

    #region Unity callbacks

    private void Awake() => _offset = transform.position - target.position;

    private void LateUpdate()
    {


        if (Input.GetKey(KeyCode.Keypad6))
        {

            transform.position += new Vector3(-9, 0, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Keypad4))
        {

            transform.position += new Vector3(9, 0, 0) * Time.deltaTime;
            
        }
        else if (Input.GetKey(KeyCode.Keypad8))
        {

            transform.position += new Vector3(0, 9, 0) * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Keypad5))
        {

            transform.position += new Vector3(0, -9, 0) * Time.deltaTime;
        }
        else
        {

            Vector3 targetPosition = target.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        }
    }

    #endregion
}