using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBehavior : MonoBehaviour
{

    [SerializeField]
    float targetDistanceToPlayer = 3;
    [SerializeField]
    float pursuitDistance = 10;

    ExtensibleStateMachine stateMachine;

    [SerializeField]
    GameObject waypointParent;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new ExtensibleStateMachine();
        player = GameObject.FindGameObjectWithTag("PlayerHitTarget").transform;

        // remove first waypoint, which is the parent
        var waypointsList = new List<Transform>(
            waypointParent.GetComponentsInChildren<Transform>()
        );
        waypointsList.RemoveAt(0);
        var waypoints = waypointsList.ToArray();

        Patrol patrol = new Patrol(gameObject, waypoints);
        Pursue pursue = new Pursue(gameObject, player, targetDistanceToPlayer);

        stateMachine.AddTransition(patrol, pursue, ShouldPursue);
        stateMachine.AddTransition(pursue, patrol, () => !ShouldPursue());

        stateMachine.SetState(patrol);
    }

    bool ShouldPursue()
    {
        if(player == null)
        {
            return false;
        }
        return Vector3.Distance(player.position, transform.position) < pursuitDistance;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
