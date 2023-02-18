using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeted : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        player.GetComponent<ShootProjectile>().FireAt(transform.position);
    }
}
