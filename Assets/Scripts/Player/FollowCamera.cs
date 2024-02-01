using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private PlayerMove _playerMove;

    [SerializeField, Range(0.1f, 0.9f)] private float _cameraSmooth = 0.5f;

    [SerializeField, Range(1, 20)] private float _fieldSpeed = 10f;

    private Vector3 offset;

    private Camera _camera;

    private const float MaxField = 65;
    private const float MinField = 60;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        if (_playerMove == null)
            _playerMove = FindAnyObjectByType<PlayerMove>();

        offset = transform.position - _target.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + offset, _cameraSmooth);

        if (_playerMove.MoveSpeed > 5)
        {
            if (_camera.fieldOfView < MaxField)
            {
                _camera.fieldOfView += Time.deltaTime * _fieldSpeed;
            }
        }
        else
        {
            if (_camera.fieldOfView > MinField)
            {
                _camera.fieldOfView -= Time.deltaTime * _fieldSpeed;
            }
        }
    }
}
