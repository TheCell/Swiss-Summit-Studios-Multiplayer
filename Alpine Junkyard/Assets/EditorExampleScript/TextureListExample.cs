using System.Collections.Generic;
using UnityEngine;

public class TextureListExample : MonoBehaviour
{
    [SerializeField] private List<Texture2D> allMaterials = new List<Texture2D>();

    public void SetMaterials(List<Texture2D> materials)
    {
        allMaterials = materials;
    }
}
