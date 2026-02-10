using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private InputAction moveAction;
    private Rigidbody2D rb;

    [Header("Puntuación")]
    public TMP_Text textoScore;
    public int puntuacion = 100;

    [Header("Vidas")]
    public int vidas = 3;                 // vidas iniciales
    public int vidasMaximas = 3;          // para la barra
    public Slider barraVida;              // referencia al slider
    public int puntosMalosTomados = 0;
    public int maxPuntosMalos = 10;

    [Header("UI")]
    public GameObject panelVictoria;
    public GameObject panelDerrota;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Start()
    {
        // Inicializar barra de vida
        barraVida.maxValue = vidasMaximas;
        barraVida.value = vidas;
    }

    private void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(input.x * speed, input.y * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si es una moneda buena
        if (collision.collider.CompareTag("MetalBueno"))
        {
            Destroy(collision.collider.gameObject);
            puntuacion++;
            textoScore.text = "Points: " + puntuacion;
            Victoria();
        }

        // Si es una moneda mala
        else if (collision.collider.CompareTag("MetalMalo"))
        {
            Destroy(collision.collider.gameObject);

            puntuacion--;
            vidas--;
            puntosMalosTomados++;

            textoScore.text = "Points: " + puntuacion;

            // Actualizar barra de vida
            barraVida.value = vidas;

            Derrota();
            Victoria();
        }
    }

    void Victoria()
    {
        if (puntuacion >= 100)
        {
            Time.timeScale = 0;
            panelVictoria.SetActive(true);
        }
    }

    void Derrota()
    {
        if (puntosMalosTomados >= maxPuntosMalos || vidas <= 0)
        {
            Time.timeScale = 0;
            panelDerrota.SetActive(true);
        }
    }

    public void RecargarEscena()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
