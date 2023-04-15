using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerScoreController : MonoBehaviour
{

   private UIDocument _doc;
   private Label _player1ScoreLabel;
   private Label _player2ScoreLabel;
   
   private void Awake()
   {
    _doc = GetComponent<UIDocument>();
    _player1ScoreLabel = _doc.rootVisualElement.Q<Label>("Player1ScoreLabel");
    _player2ScoreLabel = _doc.rootVisualElement.Q<Label>("Player2ScoreLabel");

    updateScoreLabels(GameEventManager.Instance.Player1Score, GameEventManager.Instance.Player2Score);
    GameEventManager.Instance.OnPlayerScoreEvent += updateScoreLabels;
   }

   private void updateScoreLabels(int player1score, int player2score){
      //Debug.Log(string.Format("p1: {0}, p2: {1}", player1score, player2score));

      string player1ScoreText = string.Format("P1 score: {0}", player1score);
      _player1ScoreLabel.text = player1ScoreText;

      string player2ScoreText = string.Format("P2 score: {0}", player2score);
      _player2ScoreLabel.text = player2ScoreText;
   }
}
