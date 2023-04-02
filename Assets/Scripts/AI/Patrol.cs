using UnityEngine;

public class Patrol : ExtensibleStateMachine.State
{
    float delayAtWaypoint;
    float tolerance;
    GameObject gameObject;
    NavmeshMovement navigator;
    Transform[] waypoints;
    int target;

    public Patrol(GameObject gameObject, Transform[] waypoints, float delayAtWaypoint, float tolerance = 1.0f) {
        this.waypoints = waypoints;
        this.tolerance = tolerance;
        this.gameObject = gameObject;
        this.delayAtWaypoint = delayAtWaypoint;
        this.navigator = gameObject.GetComponent<NavmeshMovement>();
        
    }

    public override void OnEnter() {
        Debug.Log("Entering Patrol");
        float closest = float.PositiveInfinity;
        for (int i = 0; i < waypoints.Length; i++) {
            float dist = Vector3.Distance(
                    waypoints[i].position, 
                    gameObject.transform.position
                );
            if (dist < closest) {
                closest = dist;
                target = i;
            }
        }
        navigator.SetDestination(waypoints[target].position);
    }

    public override void Update()
    {
        
        if (Vector3.Distance(gameObject.transform.position, waypoints[target].position) < tolerance) {
            target = (target + 1) % waypoints.Length;
            navigator.SetDestination(waypoints[target].position, delayAtWaypoint);
            navigator.stoppingDistance = 0;
        }
    }
}
