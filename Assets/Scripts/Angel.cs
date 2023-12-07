using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    public bool isDead;

    [SerializeField] private GameObject Hero;

    // Manejar vida del enemigo
    [SerializeField] bool isBoss = false;
    [SerializeField] private int Health = 3;


    private Animator Animator;


    void Start()
    {
        Hero = GameObject.FindWithTag("Player");
        Animator = GetComponent<Animator>();

        if(isBoss) Health = 5; 
        else Health = 3;
    }

    void Update()
    {
        Animator.SetBool("isDead", isDead);
    }

    public void hit()
    {
        Debug.Log("Bala pegó al angel");
        Health--;

        if (Health <= 0)
        {
            isDead = true;
        }
    }

    public void destroyAngel()
    {
        Debug.Log("Angel derrotado");
        Destroy(gameObject);
    }
}
