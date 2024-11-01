﻿using UnityEngine;

public enum MoveVersion
{
    Version1,
    Version2
}

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private MoveVersion _moveVersion = MoveVersion.Version1;
    [SerializeField, Range(0.5f, 50)] private float _maxMoveSpeed = 1;
    [SerializeField, Range(1, 50)] private float _rotateSpeed = 0.2f;
    [SerializeField, Range(1, 50)] private float _acceleration = 1;
    [Header("Only for Movement version1"), SerializeField, Range(1, 50)] private float _desacceleration = 1;

    private float _speed = 0;
    private float _input_H, _input_V;
    private Animator _playerAnim;
    private Vector3 _dir;

    private PlayerAttack _playerAttack;

    public float MoveSpeed { get => _speed; }

    private void Start()
    {
        int camera = PlayerPrefs.GetInt("Camera", 1);

        if (camera == 2)
        {
            _moveVersion = MoveVersion.Version1;
        }
        else
        {
            _moveVersion = MoveVersion.Version2;
        }

        _playerAnim = GetComponent<Animator>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if (_playerAttack.Attack)
        {
            if (_speed != 0)
            {
                _speed = 0;
                _playerAnim.SetFloat("MoveSpeed", _speed);
            }
            return;
        }

        switch (_moveVersion)
        {
            case MoveVersion.Version1:
                MovementVersion1();
                break;
            case MoveVersion.Version2:
                MovementVersion2();
                break;
        }

        _playerAnim.SetFloat("MoveSpeed", _speed);
    }

    private void FixedUpdate()
    {
        if (_playerAttack.Attack)
            return;

        switch (_moveVersion)
        {
            case MoveVersion.Version1:
                transform.Translate(_speed * _playerAnim.GetFloat("MoveSpeedFromAnim") * Time.deltaTime * _dir, null);
                break;
            case MoveVersion.Version2:
                transform.Rotate(0, _input_H * _rotateSpeed, 0);
                transform.Translate(0, 0, _input_V * _speed * _playerAnim.GetFloat("MoveSpeedFromAnim") * Time.deltaTime);
                break;
        }
    }

    private void MovementVersion1()
    {
        // Movement version 1
        _input_H = Input.GetAxisRaw("Horizontal");
        _input_V = Input.GetAxisRaw("Vertical");

        _dir = new Vector3(_input_H, 0, _input_V).normalized;

        if (_dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_dir), _rotateSpeed * Time.deltaTime);

            if (_speed < _maxMoveSpeed)
                _speed += Time.deltaTime * _acceleration;
        }
        else
        {
            if (_speed > 0)
                _speed -= Time.deltaTime * _desacceleration;
            else
                _speed = 0;
        }
    }

    private void MovementVersion2()
    {
        // Movement version2
        _input_H = Input.GetAxisRaw("Horizontal");
        _input_V = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (_speed < _maxMoveSpeed)
                _speed += Time.deltaTime * _acceleration;
        }
        else
        {
            _speed = 0;
        }
    }
}
