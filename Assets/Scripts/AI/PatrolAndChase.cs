using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAndChase : MonoBehaviour
{
    [SerializeField]
    GameObject waypointParent;

    [SerializeField]
    float delayAtWaypoint = 2;

    [SerializeField]
    GameObject player;
    ExtensibleStateMachine stateMachine;
    Collider playerCollider;

    Patrol patrol;
    Pursue pursue;

    // Start is called before the first frame update
    void Start()
    {
        if (this.player == null)
        {
            this.player = GameObject.FindGameObjectWithTag("Player");
        }
        playerCollider = this.player.GetComponent<Collider>();
        stateMachine = new ExtensibleStateMachine();

        patrol = new Patrol(
            gameObject,
            waypointParent.GetComponentsInChildren<Transform>(),
            delayAtWaypoint
        );
        pursue = new Pursue(gameObject, player.transform, 1);

        stateMachine.AddTransition(patrol, pursue, FoundPlayer);
        stateMachine.AddTransition(pursue, patrol, LostPlayer);

        stateMachine.SetState(patrol);
    }

    bool FoundPlayer()
    {
        return pursue.CanSeeTarget();
    }

    bool LostPlayer()
    {
        return pursue.timeSinceLastSeen > 5;
    }

    void Update()
    {
        stateMachine.Update();
    }
}
