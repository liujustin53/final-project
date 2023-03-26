using UnityEngine;

// This makes it easier to adjust an entity's movement parameters globally.
[CreateAssetMenu(menuName = "Game/Movement/Movement Parameters")]
public class MovementParams : ScriptableObject
{
    public float maxSpeed = 5;
    public float jumpHeight = 1;

    public float acceleration = 200;

    public float maxAccelForce = 150;
    public float airMaxAccelForce = 10;

    public float angularAcceleration = 100;
    public float angularDampening = 10;

    public float suspensionStrength = 100;
    public float suspensionDampening = 10;
}