using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawnable : Damageable
{
    Vector3 pos;
    [SerializeField] private GameObject particle;

    public bool canRespawn = true;
    [Range(1, 30)] public float respawnTime = 3.0f;

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
        if(!canRespawn)
        {
            return;
        }
        GameObject deathParticle = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(deathParticle, 2);
        gameObject.SetActive(false);
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }
}
