using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellManager : MonoBehaviour
{
    [SerializeField] private Spell primaryAttack;
    [SerializeField] private GameObject player;
    [SerializeField] private float attackDistance = 15;
    void PrimaryInvoke()
    {
        primaryAttack.StartCast();
    }

    void PrimaryCancel()
    {
        primaryAttack.ReleaseCast();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Implement with raycasting later

        if(player != null && Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            PrimaryInvoke();
        }
        else
        {
            PrimaryCancel();
        }
    }


    private void OnDisable()
    {
        PrimaryCancel();
    }
}
