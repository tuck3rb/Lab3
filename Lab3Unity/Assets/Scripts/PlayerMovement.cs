using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D body;
    public float horizontal;
    private bool movingRight = true;
    public bool jumping = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float runSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void stopRunning(){
        runSpeed = 0f;
    }

    public void startRunning(){
        runSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("horizontal", horizontal);
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
            movingRight = false;
        }
        else if (horizontal == 0 && !movingRight)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
            movingRight = true;
        }
        else if (horizontal == 0 && movingRight)
        {
            spriteRenderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !jumping) {
            body.AddForce(new Vector2(0, runSpeed * 100));
            jumping = true;
        }
    }

    void FixedUpdate() {

        body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);

    }

    void OnCollisionEnter2D(Collision2D collision2D) {
        jumping = false;
    }
}