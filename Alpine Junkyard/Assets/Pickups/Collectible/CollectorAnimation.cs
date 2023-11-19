using Shapes;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

[RequireComponent(typeof(Disc))]
public class CollectorAnimation : MonoBehaviour
{
    [SerializeField] private Collector collector;
    private Disc disc;

    private float originalRadius = 1.0f;
    private float animationDuration = 1.0f;
    private float animationStartTimestamp = 0f;
    private float positionYOffset = 0.5f;
    private Vector3 localStartPosition = Vector3.zero;
    private Vector3 fixedPosition = Vector3.zero;

    void OnEnable()
    {
        disc = GetComponent<Disc>();
        originalRadius = disc.Radius;
        localStartPosition = disc.transform.localPosition;
        disc.Radius = 0f;
        collector.itemCollected += Collector_itemCollected;
    }

    void OnDisable()
    {
        collector.itemCollected -= Collector_itemCollected;
    }

    private void Collector_itemCollected(CollectibleType collectibleType)
    {
        disc.Radius = originalRadius;
        disc.ColorInner = GetColor(collectibleType);
        var color = GetColor(collectibleType);
        color.a = 0f;
        disc.ColorOuter = color;

        StartCoroutine(AnimateFade());
    }

    private IEnumerator AnimateFade()
    {
        animationStartTimestamp = Time.time;
        disc.transform.localPosition = localStartPosition;
        fixedPosition = disc.transform.position;

        while (disc.enabled && disc.Radius > 0f)
        {
            var maxTime = this.animationDuration;
            var currentTime = Time.time - animationStartTimestamp;
            var t = 1 / maxTime * currentTime;
            disc.Radius = math.lerp(this.originalRadius, 0f, t);
            disc.transform.position = math.lerp(fixedPosition, new Vector3(fixedPosition.x, fixedPosition.y + positionYOffset, fixedPosition.z), t);

            if (disc.Radius < 0f)
            {
                yield return null;
            }
            yield return 0;
        }
    }

    private Color GetColor(CollectibleType collectibleType)
    {
        switch (collectibleType)
        {
            case CollectibleType.Coin:
                return Color.yellow;
            case CollectibleType.Snack:
                return new Color(0.76f, 0.24f, 0.24f, 1f);
            case CollectibleType.Trash:
                return new Color(0.3f, 0.3f, 0.3f, 1f);
            default:
                return Color.magenta;
        }
    }
}
