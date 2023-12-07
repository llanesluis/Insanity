using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator Animator;


    public Transform controladorAtaque;
    public float distanciaLinea;
    public LayerMask layerMask;
    public bool jugadorEnRango;

    public float tiempoEntreDisparos;
    public float tiempoUltimoDisparo;
    public float tiempoDeEsperaDisparo;

    [SerializeField] private GameObject disparoEnemigoPrefab;
    [SerializeField] private GameObject Hero;
    private Vector3 Direction;

    private Vector3 controladorAtaquePosition;
    private Vector3 transformRight;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Hero = GameObject.FindWithTag("Player");

    }

    void Update()
    {
        SetDirection();

        jugadorEnRango = Physics2D.Raycast(controladorAtaque.position, transformRight, distanciaLinea, layerMask);
        //jugadorEnRango = Physics2D.Raycast(controladorAtaque.position, -transform.right, distanciaLinea, layerMask);

        if (jugadorEnRango)
        {
            if(Time.time > tiempoEntreDisparos + tiempoUltimoDisparo)
            {
                tiempoUltimoDisparo = Time.time;

                Animator.SetTrigger("Attack");
                Invoke(nameof(Disparar), tiempoDeEsperaDisparo);
            }
        }
    }

    private void Disparar()
    {
        Instantiate(disparoEnemigoPrefab, controladorAtaque.position, controladorAtaque.rotation);
        //Instantiate(disparoEnemigoPrefab, controladorAtaque.position, controladorAtaque.rotation);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Ajusta la posición de destino en el eje y
        Gizmos.DrawLine(controladorAtaque.position, controladorAtaque.position + transformRight * distanciaLinea);
        //Gizmos.DrawLine(controladorAtaque.position, controladorAtaque.position - transform.right * distanciaLinea);

    }

    private void SetDirection()
    {
        Direction = Hero.transform.position - transform.position;

        if (Direction.x >= 0.0f)
        {
            transformRight = transform.right;
        }
        else if (Direction.x <= 0.0f)
        {
            transformRight = -transform.right;
        }
    }
}
