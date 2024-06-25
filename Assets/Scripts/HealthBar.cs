using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform player; // Referencja do gracza
    public Vector3 offset;   // Offset od głowy gracza
    public Image healthFill; // Referencja do obrazu wypełnienia paska zdrowia

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (player != null)
        {
            // Przenieś pasek zdrowia nad głową gracza
            Vector3 screenPos = mainCamera.WorldToScreenPoint(player.position + offset);
            transform.position = screenPos;
        }
    }

    // Metoda do aktualizacji zdrowia
    public void UpdateHealth(float healthPercentage)
    {
        healthFill.fillAmount = healthPercentage;
    }
}

