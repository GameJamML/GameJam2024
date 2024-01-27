using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform relativeTransform;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotateSpeed = 0.2f;

    private float input_H, input_V;
    private Animator playerAnim;
    private Vector3 dir;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        input_H = Input.GetAxisRaw("Horizontal");
        input_V = Input.GetAxisRaw("Vertical");

        dir = new Vector3(input_H, 0, input_V);

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.deltaTime);
            //playerAnim.SetBool("isWalk", true);
        }
        else
        {
            //playerAnim.SetBool("isWalk", false);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * dir, relativeTransform);
    }
}
