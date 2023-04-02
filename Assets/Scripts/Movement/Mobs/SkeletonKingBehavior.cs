using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKingBehavior : MonoBehaviour, KillResponse
{
    [SerializeField]
    ToggleActive magicWall;

    [SerializeField]
    PortalEntrance portal;

    [SerializeField]
    AudioClip winSFX;

    [SerializeField] float targetDistanceToPlayer = 3;

    [SerializeField] int pursueHP = 50;
    Mortal mortal;

    ExtensibleStateMachine stateMachine;
    [SerializeField] GameObject waypointParent;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        stateMachine = new ExtensibleStateMachine();
        Transform player = GameObject.FindGameObjectWithTag("PlayerHitTarget").transform;
        mortal = GetComponent<Mortal>();

        Patrol patrol = new Patrol(gameObject, waypointParent.GetComponentsInChildren<Transform>(), 0.5f);
        Pursue pursue = new Pursue(gameObject, player, targetDistanceToPlayer);

        stateMachine.AddTransition(patrol, pursue, ShouldPursue);

        stateMachine.SetState(patrol);
    }

    bool ShouldPursue() {
        return mortal.hp < pursueHP;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    public void OnKilled()
    {
        magicWall.DeactivateObjects();
        portal.isActive = true;
        AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
    }
}
