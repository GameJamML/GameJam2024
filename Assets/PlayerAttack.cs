using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Range(0.01f, 0.3f)] private float _attakDelaySpeed;
    [SerializeField] private Transform _attackTransform;
    [SerializeField] private Transform _mirrorTransform;
    [SerializeField] private GameObject _attackRange;
    [SerializeField] private ParticleSystem _attackParticle;

    private Animator _anim;

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
            _anim.SetTrigger("Attack");
            StartAttack();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            EndSAttack();
        }
    }

    private void StartAttack()
    {
        StopCoroutine(AttackEnding());
        StartCoroutine(TakeAttack());
        _attackRange.SetActive(true);
    }

    private void EndSAttack()
    {
        StopCoroutine(TakeAttack());
        StartCoroutine(AttackEnding());
        _attackRange.SetActive(false);
    }

    private IEnumerator TakeAttack()
    {
        for (float t = 0; t < 1; t += _attakDelaySpeed)
        {
            _attackTransform.localScale = Vector3.Lerp(Vector3.one * 0.3f, Vector3.one, t);

            yield return null;
        }

        _attackParticle.transform.parent.transform.position = _mirrorTransform.position;

        _attackParticle.Play();
    }

    private IEnumerator AttackEnding()
    {
        for (float t = 0; t < 1; t += _attakDelaySpeed)
        {
            _attackTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.3f, t);

            yield return null;
        }

        _attackParticle.Stop();
    }
}
