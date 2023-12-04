using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private GameObject Hero;
    private Vector3 Direction;


    private Rigidbody2D Rigidbody2D;
    void Start()
    {
        Hero = GameObject.FindWithTag("Player");
        Rigidbody2D = GetComponent<Rigidbody2D>();

        SetDisparoDirection();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2((Direction.x > 0.0f ? 1 : -1) * speed, Rigidbody2D.velocity.y);
    }

    private void SetDisparoDirection()
    {
        Direction = Hero.transform.position - transform.position;
        if (Direction.x >= 0.0f) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        else if (Direction.x <= 0.0f) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Piso"))
        {
            Destroy(gameObject);
        }
    }
}
