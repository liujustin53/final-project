using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAfterEntry : MonoBehaviour
{
    [SerializeField] GameObject boss;
    ToggleActive toggle;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<ToggleActive>();
        toggle.DeactivateObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When a player enters the trigger area, the object's children all get set to active
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            GetComponent<ToggleActive>().ActivateObjects();
            triggered = true; // Added to prevent being called after first entry
            boss.SetActive(true);
        }
    }
}
