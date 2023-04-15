using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelTitleController : MonoBehaviour
{
   [SerializeField]
   private string sceneTitle;
   [SerializeField]
   private int sceneLevel = 0;

   private UIDocument _doc;
   private Label titleLabel;
   private Label levelLabel;
   
   private void Awake()
   {
    _doc = GetComponent<UIDocument>();
    titleLabel = _doc.rootVisualElement.Q<Label>("TitleLabel");
    levelLabel = _doc.rootVisualElement.Q<Label>("LevelLabel");
   }

   private void Start()
   {
      titleLabel.text = sceneTitle;
      levelLabel.text = "Level " + sceneLevel;
   }
}
