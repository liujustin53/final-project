using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Behavior : MonoBehaviour, KillResponse
{
    [SerializeField]
    PortalEntrance portal;

    [SerializeField]
    AudioClip winSFX;

    [SerializeField]
    float targetDistanceToPlayer = 3;

    [SerializeField]
    int pursueHP = 100;
    Mortal mortal;

    ExtensibleStateMachine stateMachine;

    [SerializeField]
    GameObject waypointParent;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = new ExtensibleStateMachine();
        Transform player = GameObject.FindGameObjectWithTag("PlayerHitTarget").transform;
        mortal = GetComponent<Mortal>();

        // remove first waypoint, which is the parent
        var waypointsList = new List<Transform>(
            waypointParent.GetComponentsInChildren<Transform>()
        );
        waypointsList.RemoveAt(0);
        var waypoints = waypointsList.ToArray();

        Patrol patrol = new Patrol(gameObject, waypoints);
        Pursue pursue = new Pursue(gameObject, player, targetDistanceToPlayer);

        stateMachine.AddTransition(patrol, pursue, ShouldPursue);

        stateMachine.SetState(patrol);
    }

    bool ShouldPursue()
    {
        return mortal.hp < pursueHP;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    public void OnKilled()
    {
        Debug.Log("Killed");
        portal.isActive = true;
        AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
    }
}
