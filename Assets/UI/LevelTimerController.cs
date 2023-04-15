using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelTimerController : MonoBehaviour
{
   [SerializeField]
   private string gameOverSceneName;

   [SerializeField] 
   private float timeLeft = 300;

   [SerializeField]
   private float lastSeconds = 10; 
   
   private bool timerOn = false;
   private bool lastSecondsReached = false;

   private UIDocument _doc;
   private Label _timerLabel;
   
   private void Awake()
   {
    _doc = GetComponent<UIDocument>();
    _timerLabel = _doc.rootVisualElement.Q<Label>("TimerLabel");

    GameEventManager.Instance.OnPlayerReachedFinish += OnPlayerReachedFinish;
   }

   private void Start()
   {
      timerOn = true;
   }

   private void Update()
   {
      if(timerOn){
         if(timeLeft > 0){
            timeLeft -= Time.deltaTime;
            updateTimer(timeLeft);

            if(timeLeft <= lastSeconds && !lastSecondsReached){
               lastSecondsReached = true;
               _timerLabel.style.color = new StyleColor(new Color(1.0f, 0.0f, 0.0f));
            }

         } else{
            timeLeft = 0;
            timerOn = false;
            TimerElapsed();
         }
      }
   }

   public void updateTimer(float currentTime){
      currentTime += 1;

      float minutes = Mathf.FloorToInt(currentTime / 60);
      float seconds = Mathf.FloorToInt(currentTime % 60);

       string displayText = string.Format("{0:00} : {1:00}", minutes, seconds); 
      _timerLabel.text = displayText;
   }

   private void TimerElapsed(){
     GameEventManager.Instance.LevelTimeEnded();
   }

   private void OnPlayerReachedFinish(int playerId, float newTimeLeft){
     // TODO: param and assignment commented out to avoid compiler error
     //timeLeft = newTimeLeft;
   }

}
