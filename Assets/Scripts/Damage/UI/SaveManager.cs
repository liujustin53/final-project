using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static float Playtime = 0f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public static float RetrievePlayTime()
    {
        Playtime = PlayerPrefs.GetFloat("Playtime", 0f);
        return Playtime;
    }

    public static void SavePlayTime()
    {
        PlayerPrefs.SetFloat("Playtime", Playtime);
    }

    public static void RetrieveControlParameters()
    {
        ControlParameters.lookSensitivity = PlayerPrefs.GetFloat("Look Sensitivity", 0.5f);
    }

    public static void SaveControlParameters()
    {
        PlayerPrefs.SetFloat("Look Sensitivity", ControlParameters.lookSensitivity);
    }
}
