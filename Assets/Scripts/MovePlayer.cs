﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float jumpPower = 5f;
    [SerializeField]
    private float widthJump = 1f;
    [SerializeField]
    private float widthCanJump = 2.0f;

    private Rigidbody rb;
    private bool canJump = true;
    private float _timerJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _timerJump = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

        if (_velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + _velocity * Time.deltaTime);
        }

        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, widthCanJump))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if (Input.GetButton("Jump") && canJump)
        {
            _timerJump = widthJump;
        }

        _timerJump -= Time.deltaTime;

        if ( _timerJump >= 0.0f)
        {
            rb.AddForce(Vector3.up * jumpPower * 1000 * Time.deltaTime, ForceMode.Acceleration);
        }

    }
}
