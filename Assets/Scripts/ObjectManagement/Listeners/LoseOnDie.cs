using UnityEngine;

public class LoseOnDie : MonoBehaviour, KillResponse
{
    [SerializeField] AudioClip loseSFX;
    public void OnKilled()
    {
        AudioSource.PlayClipAtPoint(loseSFX, Camera.main.transform.position);
        LevelManager.Lose();
    }
}
