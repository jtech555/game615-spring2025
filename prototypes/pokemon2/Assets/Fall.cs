using UnityEngine;

public class PlatformFall : MonoBehaviour
{
    public float fallDelay = 1f; 
    public float destroyDelay = 2f; 

    private Rigidbody rb;
    private BoxCollider boxCollider;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider= GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            boxCollider.isTrigger = false;

            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
