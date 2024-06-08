using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform player; // Referencja do gracza
    public Vector3 offset;   // Offset od g�owy gracza
    public Image healthFill; // Referencja do obrazu wype�nienia paska zdrowia

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (player != null)
        {
            // Przenie� pasek zdrowia nad g�ow� gracza
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

