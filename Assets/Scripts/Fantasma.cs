using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fantasma : MonoBehaviour
{
    public bool isDead;

    [SerializeField]
    private GameObject Hero;

    private Animator Animator;

    // Manejar vida del enemigo
    private int Health = 5;

    void Start()
    {
        Hero = GameObject.FindWithTag("Player");
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Animator.SetBool("isDead", isDead);
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
}
