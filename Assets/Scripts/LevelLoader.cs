using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1.0f;

    private void Start()
    {
        GameEventManager.Instance.NotifyPlayLevelTransition += PlaySceneTransition;
    }

    void PlaySceneTransition()
    {
        StartCoroutine(StartCrossfade(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator StartCrossfade(int levelIndex)
    {
        // Play Crossfade animation
        transition.SetTrigger("StartCrossfade");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // Load next Scene
        //SceneManager.LoadScene(levelIndex);
        GameEventManager.Instance.ChangeLevel(levelIndex);
    }
}
