using UnityEngine;

public class HumanoidLocomotionAnimator : MonoBehaviour, PhysicsMover.JumpListener
{
    PhysicsMover mover;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        mover = GetComponent<PhysicsMover>();
    }
    public void OnJump() {
        animator.SetTrigger("Jump");
    }

    void Update()
    {
        animator.SetBool("Grounded", mover.isGrounded);
        animator.SetFloat("Speed", Vector3.Dot(mover.velocity, transform.forward));
    }
}
