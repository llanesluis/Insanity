using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed;
    public bool isSpawned;
    public bool isDead;

    [SerializeField] private GameObject Hero;
    [SerializeField] private SpriteRenderer SpriteRenderer;


    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private Vector3 Direction;
   

    //Manejar vida del enemigo
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
        //Para que siempre este mirando al jugador
        SetEnemyDirection();

        Animator.SetBool("isSpawned", isSpawned);
        Animator.SetBool("isDead", isDead);


    }

    private void FixedUpdate()
    {
        if (isSpawned == true) FollowPLayer();
    }

    private void SetEnemyDirection()
    {
        Direction = Hero.transform.position - transform.position;
        //if (Direction.x >= 0.0f) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //else if (Direction.x <= 0.0f) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if (Direction.x >= 0.0f) SpriteRenderer.flipX = true;
        else if (Direction.x <= 0.0f) SpriteRenderer.flipX = false;
    }
    private void FollowPLayer()
    {
        Rigidbody2D.velocity = new Vector2((Direction.x > 0.0f ? 1 : -1) * speed, Rigidbody2D.velocity.y);
    }
    public void startMovement()
    {
        isSpawned = true;
    }
    public void hit()
    {
        Debug.Log("Bala pego a skeleton");
        Health--;

        if (Health == 0)
        {
            isDead = true;
        }

    }
    public void destroySkeleton()
    {
        Debug.Log("Enemigo muerto");
        Destroy(gameObject);
    }
}
