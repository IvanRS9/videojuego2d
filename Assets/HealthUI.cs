using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab; // Prefab del coraz�n (Imagen)
    public Transform heartContainer; // Contenedor donde estar�n los corazones
    public int maxHealth = 5; // M�xima cantidad de corazones (vida m�xima)
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Establecemos la vida actual como el m�ximo al inicio
        UpdateHearts();
    }

    // M�todo para actualizar la UI de corazones
    public void UpdateHearts()
    {

        Debug.Log("Vida actual: " + currentHealth);
        // Limpiar corazones antiguos
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }

        // A�adir los corazones necesarios
        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(heartPrefab, heartContainer); // Instanciar el prefab del coraz�n
        }
    }

    // M�todo para reducir la vida del jugador
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHearts(); // Actualizar la UI cuando se toma da�o
    }

    // M�todo para curar al jugador
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHearts(); // Actualizar la UI cuando se cura
    }
}
