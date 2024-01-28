using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private int _nextSceneId;
    [SerializeField] private ParticleSystem[] particleSystems;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            foreach (var particleSystem in particleSystems)
            {
                particleSystem.Play();
            }
            
            StartCoroutine(ChangeScene());
        }
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_nextSceneId);
    }
}
