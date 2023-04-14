using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Damageable
{
    Vector3 pos;
    [SerializeField] private GameObject particle;

    private void Start()
    {
        pos = transform.position;    
    }

    public override void Damage_Implementation()
    {
        SetRespawn();
    }

    private void OnBecameInvisible()
    {
        SetRespawn();
    }

    private void SetRespawn()
    {
        Destroy(Instantiate(particle, transform.position, Quaternion.identity), 2);
        gameObject.SetActive(false);
        Invoke("Respawn", 3);
    }

    void Respawn()
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }
}
