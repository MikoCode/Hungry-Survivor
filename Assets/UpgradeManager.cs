using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController= GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DoubleShoot()
    {
        playerController.doubleShoot = true;
    }

    public void fasterReload()
    {
        playerController.upgradeInterval *= 0.9f;
    }

    public void FireShots()
    {
        playerController.projectileType = 1;
    }

    public void IceShots()
    {
        playerController.projectileType = 2;
    }

    public void Speed()
    {
        playerController.moveSpeed *= 1.1f;
    }

    public void DamageUpgrade()
    {
        GameManager.Instance.bulletDamage *= 1.1f;
    }

    public void HPIncrease()
    {
        playerController.maxHealth += 20;
        playerController.currentHealth += 20;

    }
}
