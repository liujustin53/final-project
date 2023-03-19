using UnityEngine;

public class LoseOnDie : MonoBehaviour, KillResponse
{
    public void OnKilled()
    {
        LevelManager.Lose();
    }
}
