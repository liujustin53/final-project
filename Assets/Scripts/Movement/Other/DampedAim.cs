using UnityEngine;

public class DampedAim : MonoBehaviour
{
    [SerializeField]
    float damping = 0;

    [SerializeField]
    protected Transform target;

    [SerializeField]
    protected Transform alignYAxisWith;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        Vector3 toTarget = (target.position - transform.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(toTarget, alignYAxisWith.up);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            1 - Mathf.Pow(damping, Time.deltaTime)
        );
    }
}
