using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    public float speed;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        //Movimiento de la bola de fuego
        Rigidbody2D.velocity = Direction * speed;
    }

    public void setFireballDirection(Vector2 direction)
    {
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (direction.x < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        Direction = direction;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Obtener los enemigos aqui
        Skeleton skeleton = collision.GetComponent<Skeleton>();
        Angel angel = collision.GetComponent<Angel>();
        Cat cat = collision.GetComponent<Cat>();

        //Restarles puntos de vida
        if (skeleton != null)
        {
            skeleton.hit();
            Destroy(gameObject);
        }

        if (angel != null)
        {
            angel.hit();
            Destroy(gameObject);
        }

        if (cat != null)
        {
            cat.hit();
            Destroy(gameObject);
        }

        if (collision.CompareTag("Piso")) Destroy(gameObject);

    }
}
