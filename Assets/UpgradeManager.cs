
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public PlayerController playerController;
    private Dictionary<Action, (string, Color)> actions;

    void Start()
    {
        // Zdefiniowanie dostêpnych funkcji wraz z tekstem i kolorem
        actions = new Dictionary<Action, (string, Color)>
        {
            { DoubleShoot, ("DoubleShots", Color.grey) },
            { IceShots, ("Ice Shots", Color.blue) },
            { FireShots, ("FireShots", Color.red) },
            { DamageUpgrade, ("Damage", Color.grey) },
            { Speed, ("Speed", Color.grey) },
            { HPIncrease, ("Health", Color.green) },
            { fasterReload, ("Reload", Color.gray) }
        };

     
    }


    public void AssignActionsToButtons()
    {
        var availableActions = new List<Action>(actions.Keys);

        AssignRandomAction(button1, availableActions);
        AssignRandomAction(button2, availableActions);
        AssignRandomAction(button3, availableActions);
    }

    public void AssignRandomAction(Button button, List<Action> availableActions)
    {
        // Wybierz losow¹ funkcjê z dostêpnych akcji
        Action randomAction = availableActions[UnityEngine.Random.Range(0, availableActions.Count)];
        availableActions.Remove(randomAction); // Usuñ wybran¹ akcjê z dostêpnych akcji

        // Przypisz losow¹ funkcjê do przycisku
        button.onClick.AddListener(() =>
        {
            randomAction.Invoke();
            ClearAllButtonListeners();
        });

        // Ustaw tekst i kolor przycisku
        var actionData = actions[randomAction];
        var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = actionData.Item1;
        }
        button.image.color = actionData.Item2;
    }

    Action RandomAction()
    {
        List<Action> actionKeys = new List<Action>(actions.Keys);
        return actionKeys[UnityEngine.Random.Range(0, actionKeys.Count)];
    }

    void ClearAllButtonListeners()
    {
        ClearButtonListener(button1);
        ClearButtonListener(button2);
        ClearButtonListener(button3);
    }

    void ClearButtonListener(Button button)
    {
        button.onClick.RemoveAllListeners();

        var tmpText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
        {
            tmpText.text = "No Action";
        }
        button.image.color = Color.gray;
    }

    public void DoubleShoot()
    {
        playerController.doubleShoot = true;
        GameManager.Instance.expSystem.AfterLevelUp();
        Debug.Log("DShotsUpgrade");

    }

    public void fasterReload()
    {
        playerController.upgradeInterval *= 0.9f;
        GameManager.Instance.expSystem.AfterLevelUp();
        Debug.Log("ReloadUpgrade");
    }

    public void FireShots()
    {
        playerController.projectileType = 1;
        GameManager.Instance.expSystem.AfterLevelUp();
        Debug.Log("FireUpgrade");
    }

    public void IceShots()
    {
        playerController.projectileType = 2;
        GameManager.Instance.expSystem.AfterLevelUp();
        Debug.Log("IceUpgrade");
    }

    public void Speed()
    {
        playerController.moveSpeed *= 1.1f;
        GameManager.Instance.expSystem.AfterLevelUp();
        Debug.Log("SpeedUpgrade");
    }

    public void DamageUpgrade()
    {
        GameManager.Instance.bulletDamage *= 1.1f;
        GameManager.Instance.expSystem.AfterLevelUp();
        Debug.Log("DamageUpgrade");
    }

    public void HPIncrease()
    {
        playerController.maxHealth += 20;
        playerController.Heal(20);
        GameManager.Instance.expSystem.AfterLevelUp();

    }

    
}
