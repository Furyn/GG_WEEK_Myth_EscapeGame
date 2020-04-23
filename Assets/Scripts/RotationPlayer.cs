using UnityEngine;

public class RotationPlayer : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    public float amplitudeWobbling = 3f;
    public float amplitudeWobblingCrouch = 1.5f;
    public float speedWobbling = 2.5f;
    public float speedWobblingCrouch = 1.25f;

    [HideInInspector]
    public bool isCrouch = false;

    private float _numberIncrease;
    private bool _isIncreasing;

    [SerializeField]
    private Camera playerCam;

    // Start is called before the first frame update
    void Start()
    {
        if (playerCam == null)
        {
            Debug.LogError("CAMERA NON REFERENCER");
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isPaused)
        {
            float _amplitude = amplitudeWobbling;
            float _speed = speedWobbling;
            if (isCrouch)
            {
                _amplitude = amplitudeWobblingCrouch;
                _speed = speedWobblingCrouch;
            }

            if (_isIncreasing)
                _numberIncrease += _speed / 100;
            else
                _numberIncrease -= _speed / 1000;

            if (_numberIncrease > _amplitude)
            {
                _numberIncrease = _amplitude;
            }
            else if (_numberIncrease < -_amplitude)
            {
                _numberIncrease = -_amplitude;
            }

            if (_numberIncrease >= _amplitude)
                _isIncreasing = false;
            else if (_numberIncrease <= -_amplitude)
                _isIncreasing = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = (Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime) + _numberIncrease * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
            Cursor.visible = false;
        }
    }
}
