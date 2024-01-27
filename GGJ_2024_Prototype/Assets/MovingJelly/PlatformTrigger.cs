using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.parent = gameObject.transform;
    }

    public void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.parent = null;
    }
}
