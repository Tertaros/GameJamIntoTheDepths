using Unity.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float m_speed = 4f;
    public float m_speedCrouch = 3f;
    public float m_speedProne = 2f;
    public float m_jumpForce = 2f;
    public float m_jumpForwardScale = 0.1f;
    public float m_fallForce = 2f;

    // Delegates and Events
    public delegate void EnterStateEventHandler();

    public event EnterStateEventHandler m_eventStand;
    public event EnterStateEventHandler m_eventCrouch;
    public event EnterStateEventHandler m_eventProne;
    public event EnterStateEventHandler m_eventJump;

    public bool IsGrounded { get; private set; }

    private Rigidbody m_rigidBody;
    private CapsuleCollider m_collider;

    private float m_colliderOriginalHeight;
    private StateMachine m_stateMachine;

    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
        if (!m_rigidBody)
        {
            Debug.LogError("Rigidbody not found");
        }
        else
        {
            m_rigidBody.freezeRotation = true;
        }

        m_collider = gameObject.GetComponent<CapsuleCollider>();
        if(!m_collider)
        {
            Debug.LogError("CapsuleCollider not found");
        }
        else
        {
            m_colliderOriginalHeight = m_collider.height;
        }

        m_animator = gameObject.GetComponent<Animator>();
        if (!m_animator)
            Debug.LogError("Animator not found");
    }

    // Update is called once per frame
    void Update()
    {
        if(m_stateMachine == null)
        {
            m_stateMachine = new StateMachine();
            m_stateMachine.Init(gameObject, new StateStand());
        }

        m_stateMachine.Update();
    }

    private void OnDestroy()
    {
        m_stateMachine.CleanUp();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsGrounded = false;
        }
    }

    public void Move(float speed)
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (direction.magnitude > 0.01) //move
        {
            Vector3 movement = vertical * transform.forward + horizontal * transform.right;
            if (movement.magnitude > 1.0f)
            {
                movement.Normalize();
            }
            transform.position = transform.position + movement * Time.deltaTime * speed;
        }
    }

    public void Jump()
    {
        m_rigidBody.AddForce((Vector3.up + transform.forward * m_speed * m_jumpForwardScale) * m_jumpForce, ForceMode.Impulse);
    }

    public bool CheckOnGround()
    {
        float dis = m_collider.height / 2.0f + 0.01f;
        RaycastHit hit;

        bool result = Physics.Raycast(transform.position, -Vector3.up, out hit, dis, LayerMask.GetMask("Ground"));
        //Debug.Log("OnGround--" + result.ToString() + "--" + dis.ToString());

        return result;
    }

    public void Fall()
    {
        if (m_rigidBody.velocity.y < 0.0f)
        {
            m_rigidBody.AddForce(Vector3.down * m_fallForce, ForceMode.Acceleration);
        }
    }

    public void OnStand() { m_eventStand?.Invoke(); ScaleCapsuleColliderHeightByBottom(1.0f); }
    public void OnCrouch() { m_eventCrouch?.Invoke(); ScaleCapsuleColliderHeightByBottom(0.5f); }
    public void OnProne() { m_eventProne?.Invoke(); ScaleCapsuleColliderHeightByBottom(0.2f); }
    public void OnJump() { m_eventJump?.Invoke(); }

    // This is only scaling the part of (height - 2 * radius)
    private void ScaleCapsuleColliderHeightByBottom(float scale)
    {
        scale = Mathf.Max(scale, 0.0f);
        float newHeight = scale * (m_colliderOriginalHeight - 2 * m_collider.radius);
        float oldHeight = m_collider.height - 2 * m_collider.radius;
        m_collider.center = m_collider.center + transform.up * (newHeight - oldHeight) / 2.0f;
        m_collider.height = newHeight + 2 * m_collider.radius;
    }
}

public class StateStand : State
{
    private PlayerMovementController m_controller;

    public override void OnEntrance(State lastState)
    {
        Debug.Log("OnStand");
        m_controller = Parent.Owner.GetComponent<PlayerMovementController>();
        m_controller.OnStand();
    }

    public override State OnRun()
    {
        m_controller.Move(m_controller.m_speed);

        if (Input.GetButtonDown("Crouch"))
        {
            return new StateCrouch();
        }

        if(Input.GetButtonDown("Prone"))
        {
            return new StateProne();
        }

        if (Input.GetButtonDown("Jump"))
        {
            return new StateJump();
        }

        return null;
    }
}

public class StateCrouch : State
{
    private PlayerMovementController m_controller;

    public override void OnEntrance(State lastState)
    {
        Debug.Log("OnCrouch");
        m_controller = Parent.Owner.GetComponent<PlayerMovementController>();
        m_controller.OnCrouch();
    }

    public override State OnRun()
    {
        if (Input.GetButton("Crouch"))
        {
            m_controller.Move(m_controller.m_speedCrouch);

            if (Input.GetButtonDown("Prone"))
            {
                return new StateProne();
            }
        }
        else
        {
            return new StateStand();
        }

        return null;
    }
}

public class StateProne : State
{
    private PlayerMovementController m_controller;

    public override void OnEntrance(State lastState)
    {
        Debug.Log("OnProne");

        m_controller = Parent.Owner.GetComponent<PlayerMovementController>();
        m_controller.OnProne();
    }

    public override State OnRun()
    {
        if (Input.GetButton("Prone"))
        {
            m_controller.Move(m_controller.m_speedProne);
        }
        else
        {
            if (Input.GetButton("Crouch"))
            {
                return new StateCrouch();
            }
            else
            {
                return new StateStand();
            }
        }

        return null;
    }
}

public class StateJump : State
{
    private PlayerMovementController m_controller;
    private float m_countDown;

    public override void OnEntrance(State lastState)
    {
        Debug.Log("OnJump");

        m_controller = Parent.Owner.GetComponent<PlayerMovementController>();

        m_controller.Jump();
        m_countDown = 0.1f;

        m_controller.OnJump();
    }

    public override State OnRun()
    {
        if(m_countDown>0.0f)
        {
            m_countDown -= Time.deltaTime;
        }
        else
        {
            if (m_controller.CheckOnGround())
            {
                return new StateStand();
            }
        }

        return null;
    }
}