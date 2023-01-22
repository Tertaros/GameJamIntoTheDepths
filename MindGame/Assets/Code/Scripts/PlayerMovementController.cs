using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] public float m_speed = 3f;
    [SerializeField] public float r_speed = 3f;
    [SerializeField] public float jumpForce = 2f;

    private Rigidbody rb;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (!rb)
            Debug.LogError("Rigidbody not found");

    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (direction.magnitude > 0.01) //move
        {
            transform.Rotate(new Vector3(0f, horizontal, 0f) * r_speed * Time.deltaTime, Space.World);
            transform.position = transform.position + vertical * transform.forward * Time.deltaTime * m_speed;

        }


        if (Input.GetKey(KeyCode.Space) && isGrounded) //Jump
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        /* if (transform.position.y < -1) //in case the player falls
             transform.position = new Vector3(transform.position.x, 2, transform.position.z);*/

    }
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
}
