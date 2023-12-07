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

        jugadorEnRango = Physics2D.Raycast(controladorMovimiento.position, transformRight, distanciaLinea, layerMask);

        if (jugadorEnRango )
        {
            Animator.SetBool("StartMovement", jugadorEnRango);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Ajusta la posición de destino en el eje y
        Gizmos.DrawLine(controladorMovimiento.position, controladorMovimiento.position + transformRight * distanciaLinea);
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
