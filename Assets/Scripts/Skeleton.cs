using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float speed;
    public bool isSpawned;
    public bool isDead;

    [SerializeField]
    private GameObject Hero;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private Vector3 Direction;
    

    //Manejar vida del enemigo
    private int Healt = 3;

    void Start()
    {
        Hero = GameObject.FindWithTag("Player");
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
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
        if (Direction.x >= 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Direction.x <= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
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
        Healt--;

        if (Healt == 0)
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
