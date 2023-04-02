using UnityEngine;

[RequireComponent(typeof(NavmeshMovement))]
public class Pursue : ExtensibleStateMachine.State
{
    NavmeshMovement navigator;
    Transform target;
    Vector3 lastKnownPosition;
    public float timeSinceLastSeen { get; protected set; }

    public Pursue(GameObject gameoOject, Transform target) {
        this.target = target;
        navigator = gameoOject.GetComponent<NavmeshMovement>();
    }

    public override void OnEnter() {
        Debug.Log("Entering pursue");
        timeSinceLastSeen = 0;
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
