using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : MonoBehaviour
{
    public bool isDead;
    public bool isBoss;
    public bool isFinalBoss;



    [SerializeField] private GameObject Hero;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    private Animator Animator;
    private Vector3 Direction;


    // Manejar vida del enemigo
    private int Health = 3;

    void Start()
    {
        Hero = GameObject.FindWithTag("Player");
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        if (isBoss) Health = 6;
        else if (isFinalBoss) Health = 10;
        else Health = 3;
    }

    void Update()
    {
        if (isDead)
        {
            Animator.SetBool("isDead", isDead);
            return;

        }

        SetEnemyDirection();

    }

    public void hit()
    {
        Debug.Log("Bala pegó al fantasma");
        Health--;

        if (Health <= 0)
        {
            isDead = true;
        }
    }

    public void destroyFantasma()
    {
        Debug.Log("Fantasma derrotado");
        Destroy(gameObject);
    }

    private void SetEnemyDirection()
    {
        Direction = Hero.transform.position - transform.position;
        //if (Direction.x >= 0.0f) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //else if (Direction.x <= 0.0f) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if (Direction.x >= 0.0f) SpriteRenderer.flipX = false;
        else if (Direction.x <= 0.0f) SpriteRenderer.flipX = true;
    }
}
