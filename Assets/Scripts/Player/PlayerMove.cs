using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Range(0.5f, 50)] private float _maxMoveSpeed = 1;
    [SerializeField, Range(1, 50)] private float _acceleration = 1;
    [SerializeField, Range(1, 50)] private float _desacceleration = 1;
    [SerializeField] private float _rotateSpeed = 0.2f;


    private float _speed = 0;
    private float _input_H, _input_V;
    private Animator _playerAnim;
    private Vector3 _dir;

    private void Start()
    {
        _playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        _input_H = Input.GetAxisRaw("Horizontal");
        _input_V = Input.GetAxisRaw("Vertical");

        _dir = new Vector3(_input_H, 0, _input_V);

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

        _playerAnim.SetFloat("MoveSpeed", _speed);
    }

    private void FixedUpdate()
    {
        transform.Translate(_speed * Time.deltaTime * _dir, null);
    }
}
