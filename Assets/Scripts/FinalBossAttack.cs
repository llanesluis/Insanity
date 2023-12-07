using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossAttack : MonoBehaviour
{
    private Animator Animator;


    public Transform controladorAtaque;
    public float distanciaLinea;
    public LayerMask layerMask;
    public bool jugadorEnRango;

    public float tiempoEntreDisparos;
    public float tiempoUltimoDisparo;
    public float tiempoDeEsperaDisparo;

    public GameObject disparoNormalPrefab;
    public GameObject disparoFuertePrefab;

    [SerializeField] private GameObject Hero;
    private Vector3 Direction;

    private Vector3 transformRight;

    void Start()
    {
        Animator = GetComponent<Animator>();
        Hero = GameObject.FindWithTag("Player");

    }

    void Update()
    {
        SetDirection();

        jugadorEnRango = Physics2D.Raycast(controladorAtaque.position - new Vector3(0f, 0.25f, 0f), transformRight, distanciaLinea, layerMask);
        jugadorEnRango = Physics2D.Raycast(controladorAtaque.position, transformRight, distanciaLinea, layerMask);
        jugadorEnRango = Physics2D.Raycast(controladorAtaque.position - new Vector3(0f, -0.25f, 0f), transformRight, distanciaLinea, layerMask);


        if (jugadorEnRango)
        {
            if (Time.time > tiempoEntreDisparos + tiempoUltimoDisparo)
            {
                tiempoUltimoDisparo = Time.time;

                Animator.SetTrigger("Attack");
                Invoke(nameof(Disparar), tiempoDeEsperaDisparo);
            }
        }
    }

    private void Disparar()
    {
        Instantiate(disparoNormalPrefab, controladorAtaque.position - new Vector3(0f, 0.25f, 0f), controladorAtaque.rotation);
        Instantiate(disparoNormalPrefab, controladorAtaque.position, controladorAtaque.rotation);
        Instantiate(disparoNormalPrefab, controladorAtaque.position - new Vector3(0f, -0.25f, 0f), controladorAtaque.rotation);


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Ajusta la posición de destino en el eje y
        Gizmos.DrawLine(controladorAtaque.position - new Vector3(0f, 0.25f, 0f), controladorAtaque.position - new Vector3(0f, 0.25f, 0f) + transformRight * distanciaLinea);
        Gizmos.DrawLine(controladorAtaque.position, controladorAtaque.position + transformRight * distanciaLinea);
        Gizmos.DrawLine(controladorAtaque.position - new Vector3(0f, -0.25f, 0f), controladorAtaque.position - new Vector3(0f, -0.25f, 0f) + transformRight * distanciaLinea);


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
