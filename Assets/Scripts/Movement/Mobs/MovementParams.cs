using UnityEngine;

// This makes it easier to adjust an entity's movement parameters globally.
[CreateAssetMenu(menuName = "Game/Movement/Movement Parameters")]
public class MovementParams : ScriptableObject
{
    public float speed = 5;
    public float jumpHeight = 1;
    public float airControl = 0.5f;
    public float groundControl = 0.99f;

    public float turnControl = 0.95f;
}