using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFinalFantasma : MonoBehaviour
{
    public bool isDead;

    [SerializeField] private GameObject Hero;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    private Animator Animator;
    private Vector3 Direction;


    // Manejar vida del enemigo
    [SerializeField] private int Health = 10;

    void Start()
    {
        Hero = GameObject.FindWithTag("Player");
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
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

    public void destroyFantasmaGameOver()
    {
        Debug.Log("MATASTE AL JEFE FINAL!");
        Destroy(gameObject);

        SceneManager.LoadScene(4);
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
