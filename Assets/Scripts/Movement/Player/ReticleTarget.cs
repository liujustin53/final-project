using UnityEngine;

// Moves to the point under the reticle.
public class ReticleTarget : MonoBehaviour
{
    [SerializeField]
    float maxDistance = 100;
    private new Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = camera.position + (camera.forward * maxDistance);
        if (Physics.Raycast(camera.position, camera.forward, out RaycastHit hit, maxDistance))
        {
            target = hit.point;
        }
        transform.position = target;
    }
}
