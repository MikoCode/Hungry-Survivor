using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform player; // Referencja do gracza
    public Vector3 offset;   // Offset od g³owy gracza
    public Image healthFill; // Referencja do obrazu wype³nienia paska zdrowia

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (player != null)
        {
            // Przenieœ pasek zdrowia nad g³ow¹ gracza
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

