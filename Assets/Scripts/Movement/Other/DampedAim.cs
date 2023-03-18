using UnityEngine;

public class DampedAim : MonoBehaviour
{
    [SerializeField] float damping = 0;
    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        Vector3 toTarget = (target.position - transform.position).normalized;
        Quaternion targetRot = Quaternion.LookRotation(toTarget, Camera.main.transform.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 1 - Mathf.Pow(damping, Time.deltaTime));
    }
}
