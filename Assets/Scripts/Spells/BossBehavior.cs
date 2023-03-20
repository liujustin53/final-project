using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour, KillResponse
{
    [SerializeField] ToggleActive magicWall;
    [SerializeField] PortalEntrance portal;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float rotationAnlge = 45;
    [SerializeField] AudioClip winSFX;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoint.rotation *= Quaternion.Euler(Vector3.up * rotationAnlge * Time.deltaTime);
    }

    public void OnKilled()
    {
        magicWall.DeactivateObjects();
        portal.isActive = true;
        AudioSource.PlayClipAtPoint(winSFX, Camera.main.transform.position);
    }
}
