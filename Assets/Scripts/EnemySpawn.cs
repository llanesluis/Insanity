using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private Animator Animator;


    public Transform controladorSpawn;
    public float distanciaLinea;
    public LayerMask layerMask;
    public bool jugadorEnRango;

    private bool isSpawned = false;


    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isSpawned) return;

        jugadorEnRango = Physics2D.Raycast(controladorSpawn.position, -transform.right, distanciaLinea, layerMask);
        
        if (jugadorEnRango && isSpawned == false)
        {
            isSpawned = true;
            Animator.SetBool("Spawn", jugadorEnRango);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Ajusta la posición de destino en el eje y
        Gizmos.DrawLine(controladorSpawn.position, controladorSpawn.position - transform.right * distanciaLinea);
    }
}
