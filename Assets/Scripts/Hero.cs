using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float jumpForce;
    public float speed;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private bool Attacking;



    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento
        Horizontal = Input.GetAxisRaw("Horizontal");

        if(Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if(Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //Ataque
        if (Input.GetMouseButtonDown(0)) Attack();
        else Attacking = false;

        //Animacion
        Animator.SetBool("Jumping", Grounded == false);
        Animator.SetBool("Running", Horizontal != 0.0f && Grounded);
        Animator.SetBool("Attacking", Attacking);

        //Para controlar que solo salte si esta tocando el suelo
        Debug.DrawRay(transform.position, Vector3.down * 0.3f, Color.yellow);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.3f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if(Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * speed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
    }
    private void Attack()
    {
        Attacking = true;
    }
}
