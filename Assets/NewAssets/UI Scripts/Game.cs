using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    [SerializeField] private Hero player;
    
    [SerializeField] private Image HP;
    [SerializeField] private Sprite HPFull;
    [SerializeField] private Sprite HP9;
    [SerializeField] private Sprite HP8;
    [SerializeField] private Sprite HP7;
    [SerializeField] private Sprite HP6;
    [SerializeField] private Sprite HP5;
    [SerializeField] private Sprite HP4;
    [SerializeField] private Sprite HP3;
    [SerializeField] private Sprite HP2;
    [SerializeField] private Sprite HP1;
    [SerializeField] private Sprite HPEmpty;

    private bool juegoPausado = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        menuPause.SetActive(false);
        player.OnCambioHP += ChangeHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }

    public void PausarJuego()
    {
        Time.timeScale = 0f; // Esto pausa el tiempo en el juego
        juegoPausado = true;
        menuPause.SetActive(true);
    }

    public void ReanudarJuego()
    {
        juegoPausado = false;
        menuPause.SetActive(false);
        Time.timeScale = 1f; // Esto reanuda el tiempo en el juego
    }

    void ChangeHP()
    {
        // Diccionario que mapea valores de HP a sprites
        Dictionary<int, Sprite> hpSprites = new Dictionary<int, Sprite>
    {
        { 10, HPFull },
        { 9, HP9 },
        { 8, HP8 },
        { 7, HP7 },
        { 6, HP6 },
        { 5, HP5 },
        { 4, HP4 },
        { 3, HP3 },
        { 2, HP2 },
        { 1, HP1 }
    };

        // Obtener el sprite correspondiente al valor actual de HP
        if (hpSprites.TryGetValue(player.Health, out Sprite hpSprite))
        {
            HP.sprite = hpSprite;
        }
        else
        {
            // Cuando HP es 0 o cualquier otro valor no especificado
            HP.sprite = HPEmpty;
        }
    }


    //void ChangeHP2()
    //{
    //    if (player.HP == 10)
    //    {
    //        HP.sprite = HPFull;
    //    }
    //    else if (player.HP == 9)
    //    {
    //        HP.sprite = HP9;
    //    }
    //    else if (player.HP == 8)
    //    {
    //        HP.sprite = HP8;
    //    }
    //    else if (player.HP == 7)
    //    {
    //        HP.sprite = HP7;
    //    }
    //    else if (player.HP == 6)
    //    {
    //        HP.sprite = HP6;
    //    }
    //    else if (player.HP == 5)
    //    {
    //        HP.sprite = HP5;
    //    }
    //    else if (player.HP == 4)
    //    {
    //        HP.sprite = HP4;
    //    }
    //    else if (player.HP == 3)
    //    {
    //        HP.sprite = HP3;
    //    }
    //    else if (player.HP == 2)
    //    {
    //        HP.sprite = HP2;
    //    }
    //    else if (player.HP == 1)
    //    {
    //        HP.sprite = HP1;
    //    }
    //    else
    //    {
    //        // Cuando HP es 0 o cualquier otro valor no especificado
    //        HP.sprite = HPEmpty;
    //    }
    //}

}
