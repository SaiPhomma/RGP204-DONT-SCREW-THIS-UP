using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        //grab references
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput =  Input.GetAxis("Horizontal");
        
        //flip player when moving left-right
        if(horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        
        //set animator parameters
            anim.SetBool("walk", horizontalInput != 0);
            anim.SetBool("grounded",  isGrounded());
           
        if(Input.GetKey(KeyCode.Space) &&  isGrounded())
            Jump();
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            anim.SetTrigger("jump");
        }
        else 
        {
            body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * jumpForce, body.linearVelocity.y);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
