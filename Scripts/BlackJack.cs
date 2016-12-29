using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

/* 
 * v0.0.1-r03
 * Written by Veritas83
 * www.NigelTodman.com
 * /Scripts/BlackJack.cs
 */

public class BlackJack : MonoBehaviour {

    public int DH1Val, DH2Val, PH1Val, PH2Val, PH1Suit, PH2Suit, DH1Suit, DH2Suit, CardValue, CardSuit;
    public string ToWho;

    // Use this for initialization
    void Start()
    {
        Debug.Log("BlackJack.cs called by GameObject: " + gameObject.name);
        CreateDeck();
    }
    // Update is called once per frame
    void Update()
    {

    }
    
    //start
    public GameObject Deck;
    public void DealNewGame()
    {
        Debug.Log("DealNewGame() fired!");
        GameManager.Instance.isNewGame = false;
        GameManager.Instance.isGameOver = false;
        DH1Val = Random.Range(1, 11);
        DH2Val = Random.Range(1, 11);
        PH1Val = Random.Range(1, 11);
        PH2Val = Random.Range(1, 11);
        DH1Suit = Random.Range(1, 4);
        DH2Suit = Random.Range(1, 4);
        PH1Suit = Random.Range(1, 4);
        PH2Suit = Random.Range(1, 4);
        //1=Club CardC
        //2=Diamond CardD
        //3=Heart CardH
        //4=Spade CardS
        DealCards("DealerHand1", DH1Val, DH1Suit);
        DealCards("DealerHand2", DH2Val, DH2Suit);
        DealCards("PlayerHand1", PH1Val, PH1Suit);
        DealCards("PlayerHand2", PH2Val, PH2Suit);
        //GameObject.FindGameObjectWithTag("DealerHand1").GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardSA").GetComponent<SpriteRenderer>().sprite;
    }
    public void CreateDeck()
    {
        //DestroyDeck();
        Debug.Log("CreateDeck() fired!");
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
        DealNewGame();
    }
    public void DealCards(string ToWho,int CardValue,int CardSuit)
    {
        //1=Club CardC
        //2=Diamond CardD
        //3=Heart CardH
        //4=Spade CardS
        Debug.Log("DealCards() fired!");
        if (CardValue >= 2 && CardValue <= 9)
        {
            if (CardSuit == 1)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardC" + CardValue.ToString()).GetComponent<SpriteRenderer>().sprite;
            }
            else if (CardSuit == 2)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardD" + CardValue.ToString()).GetComponent<SpriteRenderer>().sprite;
            }
            else if (CardSuit == 3)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardH" + CardValue.ToString()).GetComponent<SpriteRenderer>().sprite;
            }
            else if (CardSuit == 4)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardS" + CardValue.ToString()).GetComponent<SpriteRenderer>().sprite;
            }
        } else if (CardValue == 10)
        {
            if (CardSuit == 1)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardC10").GetComponent<SpriteRenderer>().sprite;
            }
            else if (CardSuit == 2)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardD10").GetComponent<SpriteRenderer>().sprite;
            }
            else if (CardSuit == 3)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardH10").GetComponent<SpriteRenderer>().sprite;
            }
            else if (CardSuit == 4)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardS10").GetComponent<SpriteRenderer>().sprite;
            }
        }
        else if (CardValue == 1 || CardValue == 11)
        {
            if (CardSuit == 1)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardCA").GetComponent<SpriteRenderer>().sprite;
            }
            else if (CardSuit == 2)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardDA").GetComponent<SpriteRenderer>().sprite;
            }
            else if (CardSuit == 3)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardHA").GetComponent<SpriteRenderer>().sprite;
            }
            else if (CardSuit == 4)
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardSA").GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
    public void DestroyDeck()
    {
        Debug.Log("DestroyDeck() fired!");
        Destroy(GameObject.FindGameObjectWithTag("DealerHand1"));
        Destroy(GameObject.FindGameObjectWithTag("PlayerHand1"));
        Destroy(GameObject.FindGameObjectWithTag("DealerHand2"));
        Destroy(GameObject.FindGameObjectWithTag("PlayerHand2"));
    }
    //end
}
