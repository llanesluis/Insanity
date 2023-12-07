using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isDead;
    [SerializeField] private GameObject Hero;
    [SerializeField] private SpriteRenderer SpriteRenderer;


    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private Vector3 Direction;

    // Manejar vida del enemigo
    private int Health = 1;

    void Start()
    {
        Hero = GameObject.FindWithTag("Player");
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        // Para que siempre esté mirando al jugador
        SetSpiderDirection();

        Animator.SetBool("isDead", isDead);
    }

    private void FixedUpdate()
    {
        if (Animator.GetBool("StartMovement")) FollowPlayer();
    }

    private void SetSpiderDirection()
    {
        Direction = Hero.transform.position - transform.position;
        if (Direction.x >= 0.0f) SpriteRenderer.flipX = true;
        else if (Direction.x <= 0.0f) SpriteRenderer.flipX = false;
    }

    private void FollowPlayer()
    {
        Rigidbody2D.velocity = new Vector2((Direction.x > 0.0f ? 1 : -1) * speed, Rigidbody2D.velocity.y);
    }

    public void hit()
    {
        Debug.Log("Bala pegó a la araña");
        Health--;

        if (Health == 0)
        {
            isDead = true;
        }
    }

    public void destroySpider()
    {
        Debug.Log("Araña derrotada");
        Destroy(gameObject);
    }
}
