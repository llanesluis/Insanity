using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public float speed;
    public bool isDead;

    [SerializeField]
    private GameObject Hero;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private Vector3 Direction;

    // Manejar vida del enemigo
    private int Health = 3;

    void Start()
    {
        Hero = GameObject.FindWithTag("Player");
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Para que siempre esté mirando al jugador
        SetCatDirection();

        Animator.SetBool("isDead", isDead);
    }

    private void FixedUpdate()
    {
        if (Animator.GetBool("StartMovement")) FollowPlayer();
    }

    private void SetCatDirection()
    {
        Direction = Hero.transform.position - transform.position;
        if (Direction.x >= 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Direction.x <= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void FollowPlayer()
    {
        Rigidbody2D.velocity = new Vector2((Direction.x > 0.0f ? 1 : -1) * speed, Rigidbody2D.velocity.y);
    }

    public void hit()
    {
        Debug.Log("Bala pegó al gato");
        Health--;

        if (Health == 0)
        {
            isDead = true;
        }
    }

    public void destroyCat()
    {
        Debug.Log("Gato derrotado");
        Destroy(gameObject);
    }
}