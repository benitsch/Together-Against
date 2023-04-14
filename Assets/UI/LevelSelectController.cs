using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
   [SerializeField]
   string backSceneName;

   [SerializeField] 
   string lvl1SceneName;

   [SerializeField] 
   string lvl2SceneName;

   [SerializeField] 
   string lvl3SceneName;

   [SerializeField] 
   string lvl4SceneName;

   [SerializeField] 
   string lvl5SceneName;

   [SerializeField] 
   string lvl6SceneName;

   [SerializeField] 
   string lvl7SceneName;

   [SerializeField] 
   string lvl8SceneName;

   [SerializeField] 
   string lvl9SceneName;

   [SerializeField] 
   string lvl10SceneName; 
   

   private UIDocument _doc;
   private Button button;
   
   private void Awake()
   {
    _doc = GetComponent<UIDocument>();

    button = _doc.rootVisualElement.Q<Button>("BackButton");
    button.clicked += () => {SceneManager.LoadScene(backSceneName);}; 

    button = _doc.rootVisualElement.Q<Button>("BtnLvl1");
    if(button != null){
      button.clicked += () => {SceneManager.LoadScene(lvl1SceneName);}; 
    }

    button = _doc.rootVisualElement.Q<Button>("BtnLvl2");
    if(button != null){
      button.clicked += () => {SceneManager.LoadScene(lvl2SceneName);}; 
    }
    
    button = _doc.rootVisualElement.Q<Button>("BtnLvl3");
    if(button != null){
      button.clicked += () => {SceneManager.LoadScene(lvl3SceneName);}; 
    }

    button = _doc.rootVisualElement.Q<Button>("BtnLvl4");
    if(button != null){
      button.clicked += () => {SceneManager.LoadScene(lvl4SceneName);}; 
    }

    button = _doc.rootVisualElement.Q<Button>("BtnLvl5");
    if(button != null){
      button.clicked += () => {SceneManager.LoadScene(lvl5SceneName);}; 
    }

    button = _doc.rootVisualElement.Q<Button>("BtnLvl6");
    if(button != null){
      button.clicked += () => {SceneManager.LoadScene(lvl6SceneName);}; 
    }

    button = _doc.rootVisualElement.Q<Button>("BtnLvl7");
    if(button != null){
      button.clicked += () => {SceneManager.LoadScene(lvl7SceneName);}; 
    }

    button = _doc.rootVisualElement.Q<Button>("BtnLvl8");
    if(button != null){
      button.clicked += () => {SceneManager.LoadScene(lvl8SceneName);}; 
    }

    button = _doc.rootVisualElement.Q<Button>("BtnLvl9");
    if(button != null){
      button.clicked += () => {SceneManager.LoadScene(lvl9SceneName);}; 
    }

    button = _doc.rootVisualElement.Q<Button>("BtnLvl10");
    if(button != null){
      button.clicked += () => {SceneManager.LoadScene(lvl10SceneName);}; 
    } 
   }

}
