using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour
{
    void Start()
    {
        // Inicializa si es necesario
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Verificamos si el objeto que toc� el veneno es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            // Reducimos la vida del jugador a trav�s del GameManager
            GameManager.Instance.lostLife();

            // Si el jugador tiene una funci�n para manejar el da�o, la llamamos
            other.gameObject.GetComponent<PlayerMove>().hittingByEnemy();

            // Destruye el objeto al instante
            Destroy(gameObject);
        }
    }
}
