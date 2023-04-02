using UnityEngine;

[RequireComponent(typeof(NavmeshMovement))]
public class Pursue : ExtensibleStateMachine.State
{
    NavmeshMovement navigator;
    Transform target;
    Vector3 lastKnownPosition;
    public float timeSinceLastSeen { get; protected set; }

    float stoppingDistance;

    public Pursue(GameObject gameoOject, Transform target, float stoppingDistance) {
        this.target = target;
        navigator = gameoOject.GetComponent<NavmeshMovement>();
        this.stoppingDistance = stoppingDistance;
    }

    public override void OnEnter() {
        Debug.Log("Entering pursue");
        timeSinceLastSeen = 0;
        navigator.stoppingDistance = stoppingDistance;
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }

    public override void Update()
    {
        if (CanSeeTarget()) {
            lastKnownPosition = target.position;
            timeSinceLastSeen = 0;
            navigator.SetDestination(lastKnownPosition);
        } else {
            timeSinceLastSeen += Time.deltaTime;
        }

    }

    public bool CanSeeTarget() {
        return navigator.CanSee(target.position);
    }
}
