using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Add a 3D Dopplereffect sound of a wind fan, depending on the distance of player1 or player2
 */
public class WindTurbine : MonoBehaviour
{
    public GameObject[] players;
    private AudioSource audioSource;

    public float maxVolume = 1f;
    public float minVolume = 0f;
    public float maxDistance = 5f;
    public float minDistance = 1f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(players);
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, players[0].gameObject.transform.position);
        float distance2 = Vector2.Distance(transform.position, players[1].transform.position);
        float volume = Mathf.Lerp(maxVolume, minVolume, (distance - minDistance) / (maxDistance - minDistance));
        float volume2 = Mathf.Lerp(maxVolume, minVolume, (distance2 - minDistance) / (maxDistance - minDistance));
        audioSource.volume = Mathf.Max(volume, volume2);
    }
}
