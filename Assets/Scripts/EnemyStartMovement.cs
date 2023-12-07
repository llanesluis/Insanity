using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartMovement : MonoBehaviour
{
    private Animator Animator;


    public Transform controladorMovimiento;
    public float distanciaLinea;
    public LayerMask layerMask;
    public bool jugadorEnRango;



    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {

        jugadorEnRango = Physics2D.Raycast(controladorMovimiento.position, -transform.right, distanciaLinea, layerMask);

        if (jugadorEnRango )
        {
            Animator.SetBool("StartMovement", jugadorEnRango);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Ajusta la posición de destino en el eje y
        Gizmos.DrawLine(controladorMovimiento.position, controladorMovimiento.position - transform.right * distanciaLinea);
    }
}
