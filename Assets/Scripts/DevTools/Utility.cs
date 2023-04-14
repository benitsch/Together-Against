using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public static bool LayerMaskHas(LayerMask mask, int layer)
    {
        return (mask & (1 << layer)) != 0;
    }
}
