using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float lifeTime = 5.0f;
    public string sourceTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(sourceTag) && !other.gameObject.CompareTag(gameObject.tag))
        {
            Debug.Log(other.gameObject.tag);
            gameObject.SetActive(false);
            Destroy(gameObject, 1.0f);
        }
    }
}
