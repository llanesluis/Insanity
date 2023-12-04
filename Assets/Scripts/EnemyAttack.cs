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

    public GameObject disparoEnemigoPrefab;
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        jugadorEnRango = Physics2D.Raycast(controladorAtaque.position - new Vector3(0f, 0.3f, 0f), -transform.right , distanciaLinea, layerMask);
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
        Instantiate(disparoEnemigoPrefab, controladorAtaque.position - new Vector3(0f, 0.2f, 0f), controladorAtaque.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Ajusta la posición de destino en el eje y
        Gizmos.DrawLine(controladorAtaque.position - new Vector3(0f, 0.3f, 0f), controladorAtaque.position - new Vector3(0f, 0.3f, 0f) - transform.right * distanciaLinea);
    }
}
