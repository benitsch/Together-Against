using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
   
   private UIDocument _doc;
   private Label _labelDetailsText;
   private Label _labelPlayer1Score;
   private Label _labelPlayer2Score;
   
   private void Awake()
   {
    _doc = GetComponent<UIDocument>();
    _labelDetailsText = _doc.rootVisualElement.Q<Label>("DetailsText");
    _labelPlayer1Score = _doc.rootVisualElement.Q<Label>("Player1Score");
    _labelPlayer2Score = _doc.rootVisualElement.Q<Label>("Player2Score");

    updateWinInformation();
   }

   private void updateWinInformation(){
      int player1score = GameEventManager.Instance.Player1Score;
      int player2score = GameEventManager.Instance.Player2Score;

      string detailText;
      if(player1score == player2score){
         detailText = string.Format("You are both winners!");
      }else if(player1score > player2score){
         // player 1 wins
         detailText = string.Format("If this would be a contest, player {0} would have won", 1);
      }else {
         // player 2 wins
         detailText = string.Format("If this would be a contest, player {0} would have won", 2);
      }

      _labelDetailsText.text = detailText;

      _labelPlayer1Score.text = string.Format("{0}", player1score);
      _labelPlayer2Score.text = string.Format("{0}", player2score);
   }

}
