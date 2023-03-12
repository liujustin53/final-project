using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [HideInInspector] public float eulerY => look.y;
    [SerializeField] private InputManager input;
    [SerializeField] private ControlParameters controlParameters;
    private Vector3 look;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (input == null) {
            input = FindObjectOfType<InputManager>();
        }
    }

    public void Look() {
        Vector2 mouseLook = input.deltaLook * controlParameters.lookSensitivity;
        look.y += mouseLook.x * (controlParameters.invertX ? -1 : 1);
        look.x += mouseLook.y * (controlParameters.invertY ? -1 : 1);
        look.x = Mathf.Clamp(look.x, -90, 90);
    }

    // Update is called once per frame
    void LateUpdate() {
        Look();
        transform.rotation = Quaternion.Euler(look);
    }
}
