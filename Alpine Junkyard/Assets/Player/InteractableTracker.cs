using System.Collections.Generic;
using UnityEngine;

public class InteractableTracker : MonoBehaviour
{
    private List<SceneObject> interactableSceneObjects = new List<SceneObject>();

    public List<SceneObject> InteractableSceneObjects { get => interactableSceneObjects; private set => interactableSceneObjects = value; }

    private void OnTriggerEnter(Collider collider)
    {
        var sceneObject = collider.gameObject.GetComponentInParent<SceneObject>();
        if (sceneObject != null)
        {
            this.interactableSceneObjects.Add(sceneObject);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        var sceneObject = collider.gameObject.GetComponentInParent<SceneObject>();
        if (sceneObject != null)
        {
            this.interactableSceneObjects.Remove(sceneObject);
        }
    }
}
