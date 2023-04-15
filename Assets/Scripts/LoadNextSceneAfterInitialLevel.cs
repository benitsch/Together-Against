using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextSceneAfterInitialLevel : MonoBehaviour
{
    public void LoadNextSceneAfterInitialLevelAnimationIsDone()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
