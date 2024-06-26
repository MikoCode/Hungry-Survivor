using System.Collections;
using System.Collections.Generic;
using HungrySurvivor.Enemies;
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
    public TextMeshProUGUI sliderText;
    public float levelValue;
    private Vector3 initialScale; // Początkowa skala gracza
    public GameObject upgradeButtons;
    private EnemySpawner spawner;
    public UpgradeManager upgradeManager;
    public GameObject player;

    // Bazowe ustawienia
    private int baseTotalEXP;
    private int basePlayerLevel;
    private float baseCurrentEXP;
    private float baseExpNeeded;
    private float baseSliderValue;
    private Vector3 basePlayerScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = player.transform.localScale;
        spawner = GetComponent<EnemySpawner>();

        // Zapamiętaj bazowe ustawienia
        baseTotalEXP = totalEXP;
        basePlayerLevel = PlayerLevel;
        baseCurrentEXP = currentEXP;
        baseExpNeeded = expNeeded;
        baseSliderValue = slider.value;
        basePlayerScale = player.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GainExp(50);
        }
    }

    public void LevelUp()
    {
        PlayerLevel += 1;

        sliderText.text = "lvl " + PlayerLevel;
        currentEXP -= expNeeded;
        expNeeded += 20;
        upgradeButtons.gameObject.SetActive(true);
        upgradeManager.AssignActionsToButtons();
        Time.timeScale = 0;
    }

    public void GainExp(int exp)
    {
        totalEXP += exp;
        currentEXP += exp;
        if (currentEXP >= expNeeded && PlayerLevel <= maxLevel - 1)
        {
            LevelUp();
        }
        UpdateScale();
        UpdateSlider();
    }

    void UpdateScale()
    {
        float scalePercentage = (float)totalEXP / maxEXP; // Obliczenie procentowej wartości poziomu
        float newScale = Mathf.Lerp(minScale, maxScale, scalePercentage); // Interpolacja liniowa dla nowej skali gracza
        player.gameObject.transform.localScale = initialScale * newScale; // Ustawienie nowej skali gracza
    }

    public void UpdateSlider()
    {
        levelValue = (currentEXP / expNeeded);
        slider.value = levelValue;
    }

    public void AfterLevelUp()
    {
        Time.timeScale = 1;
        upgradeButtons.SetActive(false);
    }

    // Metoda resetująca ustawienia do wartości fabrycznych
    public void ResetToFactorySettings()
    {
        totalEXP = baseTotalEXP;
        PlayerLevel = basePlayerLevel;
        currentEXP = baseCurrentEXP;
        expNeeded = baseExpNeeded;
        slider.value = baseSliderValue;
        player.transform.localScale = basePlayerScale;

        // Aktualizacja UI
        sliderText.text = "lvl " + PlayerLevel;
        UpdateSlider();
        UpdateScale();
    }
}
