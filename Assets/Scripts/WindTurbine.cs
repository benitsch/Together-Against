using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Add a 3D Dopplereffect sound of a wind fan, depending on the distance of player1 or player2
 */
public class WindTurbine : MonoBehaviour
{
    public Transform player;
    public Transform player2;
    private AudioSource audioSource;

    public float maxVolume = 1f;
    public float minVolume = 0f;
    public float maxDistance = 5f;
    public float minDistance = 1f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1f;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        float distance2 = Vector2.Distance(transform.position, player2.position);
        float volume = Mathf.Lerp(maxVolume, minVolume, (distance - minDistance) / (maxDistance - minDistance));
        float volume2 = Mathf.Lerp(maxVolume, minVolume, (distance2 - minDistance) / (maxDistance - minDistance));
        audioSource.volume = Mathf.Max(volume, volume2);
    }
}
