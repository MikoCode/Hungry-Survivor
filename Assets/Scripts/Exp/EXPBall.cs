using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBall : MonoBehaviour
{
    private EXPSystem expSystem;
    public int exp = 10;
    // Start is called before the first frame update
    void Start()
    {
        expSystem = GameObject.Find("GameManager").GetComponent<EXPSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           GiveExp(exp); 

            Debug.Log("EXP");
            Destroy(gameObject);
        }
    }

    private void GiveExp(int exp)
    {
        expSystem.GainExp(exp);
    }
}
