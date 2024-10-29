using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPlayerDamage : MonoBehaviour
{
    public float cooldownAtaque = 2f; // Tiempo entre ataques
    private bool puedeAtacar = true; // Controla si las p�as pueden hacer da�o

    void Start()
    {
        // Inicializa si es necesario
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Verificamos si el objeto que toc� las p�as es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            // Si no puede atacar, salimos de la funci�n
            if (!puedeAtacar) return;

            // Desactivamos el ataque para evitar da�os m�ltiples en un corto tiempo
            puedeAtacar = false;

            // Reducimos la vida del jugador a trav�s del GameManager
            GameManager.Instance.lostLife();

            // Si el jugador tiene una funci�n para manejar el da�o, la llamamos
            other.gameObject.GetComponent<PlayerMove>().hittingByEnemy();

            // Esperamos el tiempo de cooldown antes de permitir otro ataque
            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    void ReactivarAtaque()
    {
        // Reactiva la capacidad de hacer da�o despu�s del cooldown
        puedeAtacar = true;
    }
}
