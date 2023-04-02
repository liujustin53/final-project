using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    string nextLevel;

    [SerializeField]
    Text gameText;
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

    public static void Lose()
    {
        isGameOver = true;
        instance.gameText.gameObject.SetActive(true);
        //OnGameOver.Invoke(GameOver.Lose);
        instance.Invoke("RetryLevel", 2.0f);
    }

    public static void Win()
    {
        isGameOver = true;
        //OnGameOver.Invoke(GameOver.Win);
        instance.Invoke("NextLevel", 0.0f);
    }

    public void NextLevel()
    {
        if (nextLevel == null || nextLevel.Length == 0)
            return;
        SceneManager.LoadScene(nextLevel);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void TogglePause()
    {
        //isPaused = !isPaused;
        if (OnTogglePause != null)
        {
            OnTogglePause.Invoke();
        }
    }

    float counter = 0;
    float smoothTime = 0.5f;

    void FixedUpdate()
    {
        /*
        if (isGameOver || isPaused) {
            counter += Time.fixedUnscaledDeltaTime / smoothTime;
        } else {
            counter -= Time.fixedUnscaledDeltaTime / smoothTime;
        }
        counter = Mathf.Clamp01(counter);
        Time.timeScale = Mathf.SmoothStep(1, 0, counter);
        Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime * Time.timeScale;
        */
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
