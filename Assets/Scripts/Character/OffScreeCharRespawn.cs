using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreeCharRespawn : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Respawnable res = GetComponentInParent<Respawnable>();
        if (res != null) res.SetRespawn();
    }
}
