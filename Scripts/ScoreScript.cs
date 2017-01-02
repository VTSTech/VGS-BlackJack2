using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

/* 
 * v0.0.1-r04
 * Written by Veritas83
 * www.NigelTodman.com
 * /Scripts/ScoreScript.cs
 */

public class ScoreScript : MonoBehaviour {
    private string CashDisplay = "";
    //private string HighScoreDisplay = "";
    //private string PlayerDisplay = "";
    public Text CashLabel;
    public Text HighScoreLabel;
    public Text PlayerLabel;
    //bool HighScoreRefreshed = false;
    // Use this for initialization
    void Start () {
        GameManager.Instance.NewGame();
        CashLabel.text = "Cash: " + GameManager.Instance.CurrentCash.ToString() + "\n";
        PlayerLabel.text = GameManager.Instance.SetPlayerName;
    }
	
	// Update is called once per frame
	void UpdateScore () {
        CashDisplay = "Cash: " + GameManager.Instance.CurrentCash.ToString() + "\n";
        CashLabel.text = CashDisplay;
    }
}
