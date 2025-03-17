using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minBallBounceBackSpeed;
    [SerializeField] private float maxBallBounceBackSpeed;
    public GameObject explosionParticle;

    private Rigidbody rb;
    // To handle ball particle effect
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            Instantiate(explosionParticle, collision.transform.position, collision.transform.rotation);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputHandler.Instance.OnMove.AddListener(MovePaddle);
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnMove.RemoveListener(MovePaddle);
    }

    private void MovePaddle(Vector3 moveDirection)
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
