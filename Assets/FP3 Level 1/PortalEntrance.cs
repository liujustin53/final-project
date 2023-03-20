using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEntrance : MonoBehaviour
{
    public GameObject portalEffect;
    public bool isActive = false;

    AudioSource portalSFX;
    // Start is called before the first frame update
    void Start()
    {
        portalSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && !portalEffect.activeInHierarchy)
        {
            portalEffect.SetActive(true);
            portalSFX.Play();
        }
        else if (!isActive && portalEffect.activeInHierarchy)
        {
            portalEffect.SetActive(false);
            portalSFX.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            Debug.Log("Player entered Portal");
            // Level beat code goes here
            LevelManager.Win();
        }

    }
}
