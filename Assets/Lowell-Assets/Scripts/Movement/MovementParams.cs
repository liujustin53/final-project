using UnityEngine;

[CreateAssetMenu(menuName = "Game/Movement/Movement Parameters")]
public class MovementParams : ScriptableObject
{
    public float speed = 5;
    public float jumpHeight = 1;
    public float airControl = 0.5f;
    public float groundControl = 0.99f;

    public float turnControl = 0.95f;

    public float coyoteTime = 0.2f;
    public float jumpBuffer = 0.2f;
}