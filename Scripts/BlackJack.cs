using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

/* 
 * v0.0.1-r02
 * Written by Veritas83
 * www.NigelTodman.com
 * /Scripts/BlackJack.cs
 */

public class BlackJack : MonoBehaviour {
    
    // Use this for initialization
    void Start() { }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n") && GameManager.Instance.isNewGame == true)
        {
            GameManager.Instance.isNewGame = false;
            GameObject DeckDisplay;
            DeckDisplay = Instantiate(Deck, transform.position, Quaternion.identity) as GameObject;
            DeckDisplay.name = "DealerHand1";
            DeckDisplay.tag = "DealerHand1";
            DeckDisplay.transform.parent = GameObject.FindGameObjectWithTag("gsui").transform;
            DeckDisplay.transform.localPosition = new Vector3(-100, 250, 1);
            DeckDisplay = Instantiate(Deck, transform.position, Quaternion.identity) as GameObject;
            DeckDisplay.name = "DealerHand2";
            DeckDisplay.tag = "DealerHand2";
            DeckDisplay.transform.parent = GameObject.FindGameObjectWithTag("gsui").transform;
            DeckDisplay.transform.localPosition = new Vector3(25, 250, 1);
            DeckDisplay = Instantiate(Deck, transform.position, Quaternion.identity) as GameObject;
            DeckDisplay.name = "PlayerHand1";
            DeckDisplay.tag = "PlayerHand1";
            DeckDisplay.transform.parent = GameObject.FindGameObjectWithTag("gsui").transform;
            DeckDisplay.transform.localPosition = new Vector3(-100, -86, 1);
            DeckDisplay = Instantiate(Deck, transform.position, Quaternion.identity) as GameObject;
            DeckDisplay.name = "PlayerHand2";
            DeckDisplay.tag = "PlayerHand2";
            DeckDisplay.transform.parent = GameObject.FindGameObjectWithTag("gsui").transform;
            DeckDisplay.transform.localPosition = new Vector3(25, -86, 1);
        }
    }
    
    //start
    public GameObject Deck;
    public void DealNewGame()
    {
        Debug.Log("DealNewGame() fired!");
    }
    public void CreateDeck()
    {
        
        Debug.Log("CreateDeck() fired!");
        //Deck[0].transform.SetParent(GameObject.FindGameObjectWithTag("gsui").transform);
    }
    //end
}
