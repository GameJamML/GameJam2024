using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.3f)] private float _attakDelaySpeed;
    [SerializeField] private Transform _attackTransform;
    [SerializeField] private Transform _mirrorTransform;
    [SerializeField] private GameObject _attackRange;
    [SerializeField] private ParticleSystem _attackParticle;


    [SerializeField] private EnemyDetector _enemyDetector;
    public bool Attack { get => _isAttack; }

    private Animator _anim;
    private bool _isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isAttack = true;

            _enemyDetector._active = false;
            
            AudioManager.Instace.PlayerSFX(AudioType.Attack);
        }

        //Aim assistance
        if (Input.GetKey(KeyCode.Space))
        {
            if (_enemyDetector._targetInstanceID != -1)
            {
                transform.LookAt(_enemyDetector._target);
                transform.eulerAngles = new Vector3(0.0f, transform.rotation.eulerAngles.y, 0.0f);
            }
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            BreakAttack();
            _enemyDetector._active = true;
        }

        _anim.SetBool("Attack", _isAttack);
    }

    private void StartAttack()
    {
        StopCoroutine(AttackEnding());
        StartCoroutine(TakeAttack());
        _attackRange.SetActive(true);
    }

    private void EndAttack()
    {
        StopCoroutine(TakeAttack());
        StartCoroutine(AttackEnding());
        _attackRange.SetActive(false);
    }

    private IEnumerator TakeAttack()
    {
        Vector3 _beginPos = new (0, 0, 0.28f);
        Vector3 _endPos = new (0, 0, 0.9f);

        for (float t = 0; t < 1; t += _attakDelaySpeed)
        {
            _attackTransform.localScale = Vector3.Lerp(Vector3.one * 0.3f, Vector3.one, t);
            _attackTransform.localPosition = Vector3.Lerp(_beginPos, _endPos, t);

            yield return null;
        }

        _attackParticle.transform.parent.transform.position = _mirrorTransform.position;

        _attackParticle.Play();
    }

    private IEnumerator AttackEnding()
    {
        Vector3 _beginPos = new (0, 0, 0.9f);
        Vector3 _endPos = new (0, 0, 0.28f);

        for (float t = 0; t < 1; t += _attakDelaySpeed)
        {
            _attackTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.3f, t);
            _attackTransform.localPosition = Vector3.Lerp(_beginPos, _endPos, t);
            yield return null;
        }

        _attackParticle.Stop();
    }

    public void BreakAttack()
    {
        _isAttack = false;
        EndAttack();
    }
}
