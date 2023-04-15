using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using FirstGearGames.SmoothCameraShaker;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1.0f;
    [SerializeField] private ShakeData shakeData;

    private void Start()
    {
        GameEventManager.Instance.NotifyPlayLevelTransition += PlaySceneTransition;
    }

    void PlaySceneTransition()
    {
        StartCoroutine(StartCameraShake());
        StartCoroutine(StartCrossfade(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator StartCameraShake()
    {
        CameraShakerHandler.Shake(shakeData);

        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator StartCrossfade(int levelIndex)
    {
        // Play Crossfade animation
        transition.SetTrigger("StartCrossfade");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // Load next Scene
        GameEventManager.Instance.ChangeLevel(levelIndex);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        GameEventManager.Instance.NotifyPlayLevelTransition -= PlaySceneTransition;
    }
}
