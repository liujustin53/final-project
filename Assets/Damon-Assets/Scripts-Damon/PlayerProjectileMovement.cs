using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileMovement : MonoBehaviour
{
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger hit");
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHit>().TakeDamage();
        }
    }
}
