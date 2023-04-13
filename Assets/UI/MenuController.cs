using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   [SerializeField] 
   string playSceneName; 
   
   [SerializeField] 
   string instructionsSceneName;
   
   [SerializeField] 
   string creditsSceneName;


   private UIDocument _doc;
   private Button _playButton;
   private Button _instructionsButton;
   private Button _creditsButton;

   private void Awake()
   {
    _doc = GetComponent<UIDocument>();

    _playButton = _doc.rootVisualElement.Q<Button>("PlayButton");
    _playButton.clicked += PlayButtonClicked; 

    _instructionsButton = _doc.rootVisualElement.Q<Button>("InstructionsButton");
    _instructionsButton.clicked += InstructionsButtonClicked;

    _creditsButton = _doc.rootVisualElement.Q<Button>("CreditsButton");
    _creditsButton.clicked += CreditsButtonClicked;
   }

   private void PlayButtonClicked()
   {
    SceneManager.LoadScene(playSceneName);
   }

   private void InstructionsButtonClicked()
   {
      SceneManager.LoadScene(instructionsSceneName);
   }

   private void CreditsButtonClicked()
   {
    SceneManager.LoadScene(creditsSceneName);
   }

}
