using UnityEngine;

public class player : MonoBehaviour
{
    private float GroundCheckR;
    private Vector3 respawnPoint;
    private bool flipRight = true;
    public Animator animator;
    public int jumpForce = 10;
    public float speed = 6f;
    private Rigidbody2D rb;
    public float horizontal;
    public bool onGround;
    public LayerMask Ground;
    public Transform GroundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GroundCheckR = GroundCheck.GetComponent<CircleCollider2D>().radius;
        respawnPoint = transform.position;
    }

    void Update()
    {
        Jump();
        CheckingGround();
        move();
    }
    void move()
    {
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        animator.SetFloat("moveX", Mathf.Abs(horizontal));
        if ((horizontal > 0 && !flipRight) || (horizontal < 0 && flipRight))
        {
            transform.Rotate(0, 180, 0);
            flipRight = !flipRight;
        }
    }
    void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckR, Ground);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeadZone")
        {
            transform.position = respawnPoint;
        }
        else if (collision.tag == "checkpoint")
        {
            respawnPoint = transform.position;
        }
    }

    void Jump()
    {
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("platform"))
        {
            this.transform.parent = collision.transform;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("platform"))
        {
            this.transform.parent = null;
        }
    }
}
