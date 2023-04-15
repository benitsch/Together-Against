using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1.0f;
   

    // Update is called once per frame
    void Update()
    {
        // TODO Add a condition to execute LoadNextLevel when the level is done and the next level should be loaded!
        //LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play Crossfade animation
        transition.SetTrigger("StartCrossfade");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // Load next Scene
        SceneManager.LoadScene(levelIndex);
    }
}
