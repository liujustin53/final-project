using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEntrance : MonoBehaviour
{
    public GameObject portalEffect;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && !portalEffect.activeInHierarchy)
        {
            portalEffect.SetActive(true);
        }
        else if (!isActive && portalEffect.activeInHierarchy)
        {
            portalEffect.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            // Level beat code goes here
        }

    }
}
