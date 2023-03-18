using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver;
    public static bool isPaused;
    private static LevelManager instance;

    /// <summary> Called whenever the game is concluded </summary>
    public static UnityAction<GameOver> OnGameOver;

    /// <summary> Called when pausing or unpausing the game </summary>
    public static UnityAction OnTogglePause;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        isGameOver = false;
        isPaused = false;
    }

    public static void Lose() {
        isGameOver = true;
        OnGameOver.Invoke(GameOver.Lose);
    }

    public static void Win() {
        isGameOver = true;
        OnGameOver.Invoke(GameOver.Win);
    }

    private void TogglePause() {
        isPaused = !isPaused;
        OnTogglePause.Invoke();
    }

    float counter = 0;
    float smoothTime = 0.5f;
    void FixedUpdate() {
        if (isGameOver || isPaused) {
            counter += Time.fixedUnscaledDeltaTime / smoothTime;
        } else {
            counter -= Time.fixedUnscaledDeltaTime / smoothTime;
        }
        counter = Mathf.Clamp01(counter);
        Time.timeScale = Mathf.SmoothStep(1, 0, counter);
        Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime * Time.timeScale;
    }

    void OnEnable()
    {
        InputManager.pause.AddListener(this.TogglePause);
    }

    void OnDisable()
    {
        InputManager.pause.RemoveListener(this.TogglePause);
    }
}
