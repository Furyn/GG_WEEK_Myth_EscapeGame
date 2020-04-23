using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CrouchPlayer))]
[RequireComponent(typeof(RotationPlayer))]
public class MovePlayer : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float jumpPower = 5f;
    [SerializeField]
    private float maxTimeJump = 1f;
    [SerializeField]
    private float maxWithJump = 10.0f;
    [SerializeField]
    private float smoothDescentJump = 2.0f;

    private Rigidbody rb;
    private bool canJump = true;
    private bool onJump = false;
    private float _timerJump;
    private float widthCanJump = 1.2f;
    private float widthInitJump;

    private CrouchPlayer crP;
    private RotationPlayer rP;

    [HideInInspector]
    public bool isCrouch = false;

    // Start is called before the first frame update
    void Start()
    {
        crP = GetComponent<CrouchPlayer>();
        rb = GetComponent<Rigidbody>();
        rP = GetComponent<RotationPlayer>();
        _timerJump = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!GameManager.Instance.isPaused)
            {
                GameManager.Instance.PauseGame();
            }
            else
            {
                GameManager.Instance.ResumeGame();
            }
        }

        if (!GameManager.Instance.isPaused)
        {
            float _xMov = Input.GetAxis("Horizontal");
            float _zMov = Input.GetAxis("Vertical");

            Vector3 _movHorizontal = transform.right * _xMov;
            Vector3 _movVertical = transform.forward * _zMov;

            float _speed = speed;
            if (isCrouch)
            {
                _speed /= 2;
            }

            Vector3 _velocity = (_movHorizontal + _movVertical) * _speed;

            if (_velocity != Vector3.zero)
            {
                rb.MovePosition(rb.position + _velocity * Time.deltaTime);
            }

            RaycastHit _hit;
            if (Physics.Raycast(transform.position, Vector3.down, out _hit, widthCanJump) && !onJump)
            {
                if (!canJump)
                {
                    rP.screenCheckTouchGround = true;
                }
                canJump = true;
            }
            else
            {
                canJump = false;
            }

            if (Input.GetKey(KeyCode.Space) && canJump)
            {
                if (_timerJump < maxTimeJump)
                {
                    _timerJump += Time.deltaTime;
                    crP.onCrouch = true;
                }
                else if (canJump)
                {
                    widthInitJump = transform.position.y;
                    onJump = true;
                    crP.onCrouch = false;
                }
                else
                {
                    crP.onCrouch = false;
                }
            }
            else if (Input.GetKeyUp(KeyCode.Space) && canJump)
            {
                widthInitJump = transform.position.y;
                onJump = true;
                crP.onCrouch = false;
            }
            else if (_timerJump <= 0.0f)
            {
                onJump = false;
            }

            if (widthInitJump + maxWithJump < transform.position.y && onJump)
            {
                onJump = false;
            }

            if (onJump)
            {
                _timerJump -= Time.deltaTime;
            }

            if (rb.velocity.y > jumpPower && onJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
            }

            if (_timerJump > 0.0f && !isCrouch && onJump)
            {
                rb.drag = 0;
                rb.AddForce(Vector3.up * jumpPower * 1000 * Time.deltaTime, ForceMode.Acceleration);
            }
            else
            {
                rb.drag = smoothDescentJump;
            }
        }

    }
}
