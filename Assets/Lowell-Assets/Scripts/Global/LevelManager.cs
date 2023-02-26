using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver;
    private static LevelManager instance;

    public static UnityEvent<GameOver> onGameOver;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }
    public static void Lose() {
        isGameOver = true;
        onGameOver.Invoke(GameOver.Lose);
    }

    public static void Win() {
        isGameOver = true;
        onGameOver.Invoke(GameOver.Win);
    }
}
