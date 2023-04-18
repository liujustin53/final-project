using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [HideInInspector]
    public float eulerY => look.y;
    private Vector3 look;
    private Transform player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        offset = transform.position - player.position;
    }

    public void Look()
    {
        Vector2 mouseLook =
            InputManager.deltaLook * ControlParameters.lookSensitivity * Time.timeScale;
        look.y += mouseLook.x * (ControlParameters.invertX ? -1 : 1);
        look.x += mouseLook.y * (ControlParameters.invertY ? -1 : 1);
        look.x = Mathf.Clamp(look.x, -90, 90);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (LevelManager.isGameOver || LevelManager.isPaused)
            return;
        Look();
        transform.rotation = Quaternion.Euler(look);
        transform.position = player.position + offset;
    }
}
