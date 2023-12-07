using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBoss : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float velocidadMovimiento;
    private int siguientePunto = 1;
    private bool ordenPuntos = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (ordenPuntos && siguientePunto + 1 >= puntosMovimiento.Length)
        {
            ordenPuntos = false;
        }
        if (!ordenPuntos && siguientePunto <= 0)
        {
            ordenPuntos = true;
        }
        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePunto].position) < 0.1f)
        {
            if (ordenPuntos) siguientePunto += 1;
            else siguientePunto -= 1;

        }

        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePunto].position, velocidadMovimiento * Time.deltaTime);
    }
}
