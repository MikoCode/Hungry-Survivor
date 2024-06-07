using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class EXPSystem : MonoBehaviour
{
    public int maxLevel = 30;
    public int maxEXP = 12000;
    public int totalEXP;
    public int PlayerLevel = 1;
    public float currentEXP;
    public float expNeeded;
    public float minScale = 1f; // Minimalna skala gracza
    public float maxScale = 5f; // Maksymalna skala gracza
    public Slider slider;
    public float levelValue;
    private Vector3 initialScale; // Pocz¹tkowa skala gracza
    public Button[] upgradeButtons;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {

            GainExp(50);
        }
    }

    public void LevelUp()
    {
        PlayerLevel += 1;
        currentEXP -= expNeeded;
        expNeeded += 20;
    }

    public void GainExp(int exp)
    {
        totalEXP += exp;
        currentEXP += exp;
        if(currentEXP >= expNeeded && PlayerLevel <= maxLevel - 1)
        {
            LevelUp();
        }
        UpdateScale();
        UpdateSlider();
    }

    void UpdateScale()
    {
        float scalePercentage = (float)totalEXP / maxEXP; // Obliczenie procentowej wartoœci poziomu
        float newScale = Mathf.Lerp(minScale, maxScale, scalePercentage); // Interpolacja liniowa dla nowej skali gracza
        transform.localScale = initialScale * newScale; // Ustawienie nowej skali gracza
    }


    public void UpdateSlider()

    {
        levelValue = (currentEXP / expNeeded);

        slider.value = levelValue;
    }
}

