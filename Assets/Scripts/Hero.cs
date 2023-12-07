using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public GameObject fireballFrefab;
    public bool isHurt;
    public bool isDead;


    [SerializeField] private float tiempoEntreDPS = 1.0f;
    [SerializeField] private float tiempoUltimoDPS = 1.0f;
    [SerializeField] private float tiempoDeEsperaDPS;
    [SerializeField] private bool recibiendoDPS;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private bool Attacking;
    private float LastAttack;


    // Declarar el delegado y el evento
    public delegate void CambioHPDelegate();
    public event CambioHPDelegate OnCambioHP;

    [SerializeField] private int _Healt;

    public int Health
    {
        get { return _Healt; }
        set
        {
            // Comprobar si el valor de HP ha cambiado
            if (_Healt != value)
            {
                _Healt = value;

                // Disparar el evento cuando HP cambia
                OnCambioHP?.Invoke();
            }
        }
    }

    public Hero()
    {
        _Healt = 10;
    }


    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) {
            Animator.SetBool("Dead", isDead);
            return;
        }
       

        //Movimiento si el personaje no esta muerto

        if(!isDead) Horizontal = Input.GetAxisRaw("Horizontal");
        if (isDead) Horizontal = 0.0f;

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if(Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //Animacion
        Animator.SetBool("Jumping", Grounded == false);
        Animator.SetBool("Running", Horizontal != 0.0f && Grounded);
        Animator.SetBool("Attacking", Attacking);

        //Animacion de muerte
        //Animator.SetBool("Dead", isDead);
        Animator.SetBool("Hurt", isHurt);


        //Ataque
        if (Input.GetMouseButtonDown(0) && Time.time > LastAttack + 0.60f)
        {
            Attack();
            LastAttack = Time.time;
        }
        else Attacking = false;

        //Para controlar que solo salte si esta tocando el suelo
        Debug.DrawRay(transform.position, Vector3.down * 0.3f, Color.yellow);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.3f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if(Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }


        //Si se hace daño por segundo
        if (recibiendoDPS)
        {
            bajarVidaPorSegundo(1);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * speed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);
    }
    private void Attack()
    {
        Debug.Log("Atacando xd");

        Attacking = true;

        Vector3 direction = transform.localScale.x > 0.0f ? Vector2.right : Vector2.left;

        GameObject fireball = Instantiate(fireballFrefab, transform.position + new Vector3(direction.x * 0.2f, direction.y * 5f), Quaternion.identity);
        fireball.GetComponent<Fireball>().setFireballDirection(direction);
    }

    public void bajarVida(int puntosDeVida = 1)
    {
        Debug.Log("- " + puntosDeVida + " de vida");
        isHurt = true;

        Health -= puntosDeVida;

        if (Health <= 0)
        {
            isDead = true;
        }
    }

    public void obtenerVida(int puntosDeVida = 1)
    {
        if (Health >= 10)
        {
            return;
        }

        Debug.Log("+ " + puntosDeVida + " de vida");

        Health += puntosDeVida;

    }

    public void bajarVidaPorSegundo(int puntosDeVida = 1)
    {

        if (Time.time > tiempoEntreDPS + tiempoUltimoDPS)
        {
            tiempoUltimoDPS = Time.time;
            Debug.Log("- " + puntosDeVida + " DPS");

            bajarVida(puntosDeVida);

        }

    }

    public void gameOver()
    {
        //Escena que diga GAME OVER
        Debug.Log(">>>>>>>>>>GAME OVER<<<<<<<<<<");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead) {


            if (collision.CompareTag("Vida"))
            {

                obtenerVida(1);
                Destroy(collision.gameObject);

            }

            if (collision.CompareTag("Espinas"))
            {
                Debug.Log("Tocó espinas");
                //bajarVida(1);

                recibiendoDPS = true;
            }


            if (collision.CompareTag("AngelAttack"))
            {
                bajarVida(2);
                Destroy(collision.gameObject);

            }

            if (collision.CompareTag("BossAngelAttack"))
            {
                bajarVida(3);
                Destroy(collision.gameObject);

            }

            if (collision.CompareTag("BossGhostAttack"))
            {
                bajarVida(2);
                Destroy(collision.gameObject);
            }

            if (collision.CompareTag("FinalBossNormalAttack"))
            {
                bajarVida(1);
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            if (collision.collider.CompareTag("Skeleton"))
            {
                Debug.Log("Te ataco un skeleton");

                recibiendoDPS = true;
            }

            if (collision.collider.CompareTag("Gato"))
            {
                Debug.Log("Te ataco un gato");

                recibiendoDPS = true;
            }

            if (collision.collider.CompareTag("Thing"))
            {
                Debug.Log("Te ataco un thing");

                recibiendoDPS = true;
            }

            if (collision.collider.CompareTag("Spider"))
            {
                Debug.Log("Te ataco una spider");

                recibiendoDPS = true;
            }

            if (collision.collider.CompareTag("FinalBoss"))
            {
                bajarVida(8);
            }

            if (collision.collider.CompareTag("Town-Cementery"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (collision.collider.CompareTag("Cementery-Swamp"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isDead)
        {
            if (collision.collider.CompareTag("Skeleton"))
            {
                recibiendoDPS = false;
            }

            if (collision.collider.CompareTag("Gato"))
            {
                recibiendoDPS = false;
            }

            if (collision.collider.CompareTag("Thing"))
            {
                recibiendoDPS = false;
            }

            if (collision.collider.CompareTag("Spider"))
            {
                recibiendoDPS = false;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.CompareTag("Espinas"))
        {
            recibiendoDPS = false;
        }
    }
    public void resetNormalState()
    {
        isHurt = false;
    }
}
