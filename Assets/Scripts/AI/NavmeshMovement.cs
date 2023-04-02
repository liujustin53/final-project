using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PhysicsMover))]
public class NavmeshMovement : MonoBehaviour
{
    [SerializeField]
    float tolerance = 0.5f;

    [SerializeField]
    float lookAhead = 1f;

    [SerializeField]
    float forwardAttraction = 0.5f;
    public float stoppingDistance = 0f;

    Vector3 destination;
    PhysicsMover mover;
    NavMeshPath path;

    int segment;
    Vector3 seek;
    Vector3 normalPoint;
    Vector3 predictedPos;
    Vector3 here;

    Vector3 segmentStart => path.corners[segment];
    Vector3 segmentEnd => path.corners[segment + 1];

    // Start is called before the first frame update
    void Awake()
    {
        mover = GetComponent<PhysicsMover>();
        path = new NavMeshPath();
    }

    public void SetDestination(Vector3 dest, float delay = 0)
    {
        if (Vector3.Distance(dest, this.destination) < 0.01)
            return;
        this.destination = dest;
        CancelInvoke("ApplyDestination");
        Invoke("ApplyDestination", delay);
    }

    private void ApplyDestination()
    {
        NavMesh.CalculatePath(transform.position, this.destination, NavMesh.AllAreas, path);
        segment = 0;
    }

    public bool CanSee(Vector3 position)
    {
        return !NavMesh.Raycast(transform.position, position, out NavMeshHit hit, NavMesh.AllAreas);
    }

    // Update is called once per frame
    void Update()
    {
        if (path.status != NavMeshPathStatus.PathComplete)
        {
            Debug.Log("Path incomplete");
            return;
        }
        NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 2, NavMesh.AllAreas);
        here = hit.position;

        if (
            Vector3.Distance(here, path.corners[^1]) - stoppingDistance
            < Mathf.Max(mover.DistanceToStop(), tolerance)
        )
        {
            mover.Movement = Vector2.zero;
            return;
        }

        UpdateSeek();

        MoveToSeek();
    }

    void MoveToSeek()
    {
        if (Vector3.Distance(here, seek) <= Mathf.Max(mover.DistanceToStop(), tolerance))
        {
            mover.Movement = Vector2.zero;
            return;
        }
        Vector3 seekForce = seek - transform.position;
        mover.Movement = new Vector2(seekForce.x, seekForce.z).normalized;
    }

    void UpdateSeek()
    {
        Vector3 dest = path.corners[^1];

        predictedPos = here;
        if (mover.velocityXZ.magnitude > 0.25)
        {
            predictedPos += mover.velocity * lookAhead;
        }

        if (DistanceToSegment(here, predictedPos, dest) < tolerance)
        {
            seek = dest;
            return;
        }

        normalPoint = GetNormalPoint(segmentStart, segmentEnd, predictedPos);

        if (Vector3.Distance(here, segmentEnd) <= tolerance)
        {
            segment++;
        }

        seek = predictedPos;
        Vector3 seekNormal = GetNormalPoint(segmentStart, segmentEnd, seek);
        seek = Vector3.MoveTowards(seekNormal, seek, tolerance);

        seek = Vector3.MoveTowards(seek, segmentEnd, tolerance + forwardAttraction);
    }

    float DistanceToSegment(Vector3 start, Vector3 end, Vector3 point)
    {
        return Vector3.Distance(point, GetNormalPoint(start, end, point));
    }

    Vector3 GetNormalPoint(Vector3 start, Vector3 end, Vector3 point)
    {
        Vector3 relativePos = point - start;
        Vector3 relativeEnd = end - start;
        Vector3 segmentDirection = relativeEnd.normalized;
        float segmentLength = relativeEnd.magnitude;

        float t = Vector3.Dot(relativePos, segmentDirection);
        t = Mathf.Clamp(t, 0, segmentLength);

        return (segmentDirection * t) + start;
    }

    void OnDrawGizmosSelected()
    {
        if (path == null)
            return;
        if (path.status != NavMeshPathStatus.PathComplete)
            return;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(seek, 0.2f);

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Gizmos.color = (i == segment) ? Color.cyan : Color.gray;
            Vector3 start = path.corners[i];
            Vector3 end = path.corners[i + 1];
            Gizmos.DrawLine(start, end);

            Vector3 segmentNormal = GetNormalPoint(start, end, here);
            Gizmos.DrawSphere(segmentNormal, 0.1f);
        }
        Gizmos.DrawLine(segmentStart, segmentEnd);
        Gizmos.DrawSphere(segmentEnd, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(predictedPos, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(normalPoint, 0.1f);
    }
}
