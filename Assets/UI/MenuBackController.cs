using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuBackController : MonoBehaviour
{
   [SerializeField] 
   string backSceneName; 
   

   private UIDocument _doc;
   private Button _backButton;
   
   private void Awake()
   {
    _doc = GetComponent<UIDocument>();

    _backButton = _doc.rootVisualElement.Q<Button>("BackButton");
    _backButton.clicked += BackButtonClicked; 
   }

   private void BackButtonClicked()
   {
    SceneManager.LoadScene(backSceneName);
   }

}
