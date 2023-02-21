using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1.0f);
        }
    }

    public void DamageEnemy()
    {
        Debug.Log("enemy hit");
        gameObject.SetActive(false);
        Destroy(gameObject, 1.0f);

    }
}
