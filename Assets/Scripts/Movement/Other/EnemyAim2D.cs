using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim2D : MonoBehaviour
{
    [SerializeField]
    private float damping = 0;
    Transform target;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 toTarget = (target.position - transform.position);
        toTarget.y = transform.position.y;
        toTarget.Normalize();
        Quaternion targetRot = Quaternion.LookRotation(toTarget, Camera.main.transform.up);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            1 - Mathf.Pow(damping, Time.deltaTime)
        );
    }

    public void SetTarget2D(Transform target)
    {
        this.target = target;
    }
}
