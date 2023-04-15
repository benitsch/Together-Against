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
        Destroy(Instantiate(particle, transform.position, Quaternion.identity), 5);
        SetRespawn(PlayerScoreReason.HazardDeath);
    }

    private void OnBecameInvisible()
    {
        SetRespawn(PlayerScoreReason.None);
    }

    public void SetRespawn(PlayerScoreReason score)
    {
        PlayerController pc = GetComponent<PlayerController>();
        if (pc != null) GameEventManager.Instance.PlayerScoreEvent(pc.playerID, score);

        if(!canRespawn) return;

        gameObject.SetActive(false);
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }
}
