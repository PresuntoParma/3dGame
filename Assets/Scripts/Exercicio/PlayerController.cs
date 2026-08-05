using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    [HideInInspector] public Rigidbody rb;

    public StateMachine StateMachine { get; private set; }

    public IdleState IdleState;
    public WalkState WalkState;
    public RunState RunState;
    public JumpState JumpState;

    public float Horizontal => Input.GetAxisRaw("Horizontal");
    public float Vertical => Input.GetAxisRaw("Vertical");

    public bool IsRunning => Input.GetKey(KeyCode.LeftShift);
    public bool JumpPressed => Input.GetKeyDown(KeyCode.Space);

    public bool IsGrounded =>
        Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        StateMachine = new StateMachine();

        IdleState = new IdleState(this, StateMachine);
        WalkState = new WalkState(this, StateMachine);
        RunState = new RunState(this, StateMachine);
        JumpState = new JumpState(this, StateMachine);
    }

    void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        StateMachine.Update();
    }

    public void Move(float speed)
    {
        Vector3 dir = new Vector3(Horizontal, 0, Vertical).normalized;

        Vector3 velocity = rb.velocity;
        velocity.x = dir.x * speed;
        velocity.z = dir.z * speed;

        rb.velocity = velocity;

        if (dir != Vector3.zero)
            transform.forward = dir;
    }

    public void Jump()
    {
        Vector3 velocity = rb.velocity;
        velocity.y = 0;

        rb.velocity = velocity;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}