using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public GameObject fireballFrefab;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private bool Attacking;
    private float LastAttack;

    //Manejar vida del player
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Movimiento
        Horizontal = Input.GetAxisRaw("Horizontal");

        if(Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if(Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //Animacion
        Animator.SetBool("Jumping", Grounded == false);
        Animator.SetBool("Running", Horizontal != 0.0f && Grounded);
        Animator.SetBool("Attacking", Attacking);

        //Ataque
        if (Input.GetMouseButtonDown(0) && Time.time > LastAttack + 0.60f)
        {
            Attack();
            LastAttack = Time.time;
        }
        else Attacking = false;

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
        Debug.Log("Atacando xd");

        Attacking = true;

        Vector3 direction = transform.localScale.x > 0.0f ? Vector2.right : Vector2.left;

        GameObject fireball = Instantiate(fireballFrefab, transform.position + new Vector3(direction.x * 0.2f, direction.y * 5f), Quaternion.identity);
        fireball.GetComponent<Fireball>().setFireballDirection(direction);
    }
}
