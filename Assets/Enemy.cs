using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float cooldownAtaque;
    public bool movimientoInfinito = true; // Activa o desactiva el movimiento aleatorio en X
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private float velocidadMovimiento = 2f;
    private float direccionMovimiento; // Variable para almacenar la direcci�n de movimiento

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb == null)
        {
            Debug.LogError("No se encontr� Rigidbody2D en el enemigo.");
        }

        // Inicia la corrutina de movimiento infinito
        StartCoroutine(MovimientoInfinito());
    }

    void Update()
    {
       
            // Establece la velocidad en el eje X en funci�n de la direcci�n
            rb.velocity = new Vector2(direccionMovimiento * velocidadMovimiento, rb.velocity.y);
        
    }

    private IEnumerator MovimientoInfinito()
    {
        while (movimientoInfinito)
        {
            animator.SetInteger("anim-state", 1); // Cambia a animaci�n de "run"

            // Asigna una direcci�n aleatoria
            direccionMovimiento = Random.Range(0, 2) == 0 ? -1f : 1f;

            // Cambia la direcci�n del sprite seg�n el valor de direcci�n
            transform.localScale = new Vector3(direccionMovimiento, 1f, 1f);

            // Cambia la direcci�n cada cierto tiempo aleatorio para hacer el movimiento m�s din�mico
            float tiempoCambioDireccion = Random.Range(5f, 8f);
            yield return new WaitForSeconds(tiempoCambioDireccion);
        }

        // Si se desactiva movimientoInfinito, el enemigo se detiene en estado "idle"
        rb.velocity = Vector2.zero;
        animator.SetInteger("anim-state", 0); // Estado "idle"
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!puedeAtacar) return;

            puedeAtacar = false;

            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            GameManager.Instance.lostLife();

            other.gameObject.GetComponent<PlayerMove>().hittingByEnemy();

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    void ReactivarAtaque()
    {
        puedeAtacar = true;
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }
}
