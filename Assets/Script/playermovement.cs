using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private bool isGrounded;

    public float jump_force = 8f;
    private int jump_init = 0;
    private int jump_max= 2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && (isGrounded || jump_init < jump_max))
        {
            jump_init++;
            rb.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jump_init = 0;
            isGrounded = true;
        }
    }

}
