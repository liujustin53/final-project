using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver;
    private static LevelManager instance;

    private static List<LevelListener> listeners;
    // Start is called before the first frame update
    void Start()
    {
        listeners = new List<LevelListener>();
        isGameOver = false;
    }
    public static void Lose() {
        isGameOver = true;
        foreach (LevelListener listener in listeners) {
            listener.OnLose();
        }
    }

    public static void Win() {
        isGameOver = true;
        foreach (LevelListener listener in listeners) {
            listener.OnWin();
        }
    }

    public static void Subscribe(LevelListener subscriber) {
        listeners.Add(subscriber);
    }
}
