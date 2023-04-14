using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static bool LayerMaskHas(LayerMask mask, int layer)
    {
        return (mask & (1 << layer)) != 0;
    }

    public static Bounds GetBounds2D(Transform parent)
    {
        Bounds bounds = new Bounds();
        var colliders = parent.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            bounds.Encapsulate(collider.bounds);
        }
        return bounds;
    }
}
