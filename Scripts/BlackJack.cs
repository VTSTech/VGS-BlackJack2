using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

/* 
 * v0.0.1-r06
 * Written by Veritas83
 * www.NigelTodman.com
 * /Scripts/BlackJack.cs
 */

public class BlackJack : MonoBehaviour {

    public int DH1Val, DH2Val, PH1Val, PH2Val, PH1Suit, PH2Suit, DH1Suit, DH2Suit, CardValue, CardSuit, CardIndex, CardHintA, CardHintB;
    public int NumPlayerCards, NumDealerCards, DealerHintA, DealerHintB, CurrentBet;
    public float SliderVal;
    public string ToWho;
    public static bool[] hasCardPlayed = new bool[53];
    public bool isPlayerStanding, isDealerStanding, DealerHasAce, PlayerHasAce;
    // Use this for initialization
    public void Start()
    {

        Debug.Log("BlackJack.cs called by GameObject: " + gameObject.name);
        GameObject pl = GameObject.FindGameObjectWithTag("PauseLabel");
        GameObject.FindGameObjectWithTag("GameOverPanel").transform.localScale = new Vector3(0, 0, 0);
        GameManager.Instance.CurrentCash = 1000;
        CurrentBet = 100;
        if (GameManager.Instance.IsPaused == true)
        {
            pl.GetComponent<Text>().enabled = true;
        } else if (GameManager.Instance.IsPaused == false)
        {
            pl.GetComponent<Text>().enabled = false;
        }
        CreateDeck();
    }
    // Update is called once per frame
    void Update()
    {

        GameObject CashLabel = GameObject.FindGameObjectWithTag("CashLabel");
        CashLabel.GetComponent<Text>().text = "Cash: $" + GameManager.Instance.CurrentCash.ToString();
        UpdateBet();
        CheckGame();
    }
    
    //start
    public GameObject Deck;
    //public GameManager GameStatus;
    public void DealNewGame()
    {
        GameObject hl = GameObject.FindGameObjectWithTag("HintLabel");
        GameObject dh = GameObject.FindGameObjectWithTag("DealerHint");
        GameObject.FindGameObjectWithTag("GameOverPanel").transform.localScale = new Vector3(0, 0, 0);
        Debug.Log("DealNewGame() fired!");
        GameManager.Instance.isNewGame = false;
        GameManager.Instance.isGameOver = false;
        GameManager.Instance.isRewarded = false;
        isPlayerStanding = false;
        isDealerStanding = false;
        PlayerHasAce = false;
        DealerHasAce = false;
        hl.GetComponent<Text>().text = "0";
        HideDeck();
        DH1Val = Random.Range(1, 13);
        DH2Val = Random.Range(1, 13);
        PH1Val = Random.Range(1, 13);
        PH2Val = Random.Range(1, 13);
        DH1Suit = Random.Range(1, 4);
        DH2Suit = Random.Range(1, 4);
        PH1Suit = Random.Range(1, 4);
        PH2Suit = Random.Range(1, 4);
        CardHintA = 0;
        CardHintB = 0;
        DealerHintA = 0;
        DealerHintB = 0;
        CardValue = 0;
        CardSuit = 0;
        NumDealerCards = 2;
        NumPlayerCards = 2;
        GameManager.Instance.DealerValue = 0;
        GameManager.Instance.PlayerValue = 0;
        hl.GetComponent<Text>().text = GameManager.Instance.PlayerValue.ToString();
        dh.GetComponent<Text>().text = GameManager.Instance.DealerValue.ToString();
        for (int x = 0; x <= 52; x++)
        {
            //Debug.Log("hasCardPlayed[" + x.ToString() + "]");
            hasCardPlayed[x] = false;
        }
        //1=Club CardC 1-13
        //2=Diamond CardD 14-27
        //3=Heart CardH 28-40
        //4=Spade CardS 41-52
        DealCards("DealerHand1", DH1Val, DH1Suit);
        DealCards("DealerHand2", DH2Val, DH2Suit);
        DealCards("PlayerHand1", PH1Val, PH1Suit);
        DealCards("PlayerHand2", PH2Val, PH2Suit);

        //GameObject.FindGameObjectWithTag("DealerHand1").GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardSA").GetComponent<SpriteRenderer>().sprite;
    }
    public int GetCardIndex(int CardValue, int CardSuit)
    {
        if (CardSuit == 1)
        {
            return CardValue;
        }
        else if (CardSuit == 2)
        {
            return CardValue + 13;
        }
        else if (CardSuit == 3)
        {
            return CardValue + 26;
        }
        else if (CardSuit == 4)
        {
            return CardValue + 39;
        }
        else
        {
            return 0;
        }
    }
    public bool isPlayed(int CardIndex)
    {
        Debug.Log("isPlayed(" + CardIndex.ToString() + ") fired DEBUG: " + hasCardPlayed[CardIndex]);
        if (hasCardPlayed[CardIndex] == false)
        {
            return false;
        } else if (hasCardPlayed[CardIndex] == true)
        {
            return true;
        }
        else
        {
            return true;
        }
    }
    public void HideDeck()
    { 
        GameObject.FindGameObjectWithTag("PlayerHand3").transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("PlayerHand4").transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("PlayerHand5").transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("DealerHand3").transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("DealerHand4").transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("DealerHand5").transform.localScale = new Vector3(0, 0, 0);
        GameObject.FindGameObjectWithTag("Respawn").transform.localScale = new Vector3(0, 0, 0);
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
        DeckDisplay.name = "DealerHand3";
        DeckDisplay.tag = "DealerHand3";
        DeckDisplay.transform.parent = GameObject.FindGameObjectWithTag("gsui").transform;
        DeckDisplay.transform.localPosition = new Vector3(150, 250, 1);
        DeckDisplay = Instantiate(Deck, transform.position, Quaternion.identity) as GameObject;
        DeckDisplay.name = "DealerHand4";
        DeckDisplay.tag = "DealerHand4";
        DeckDisplay.transform.parent = GameObject.FindGameObjectWithTag("gsui").transform;
        DeckDisplay.transform.localPosition = new Vector3(275, 250, 1);
        DeckDisplay = Instantiate(Deck, transform.position, Quaternion.identity) as GameObject;
        DeckDisplay.name = "DealerHand5";
        DeckDisplay.tag = "DealerHand5";
        DeckDisplay.transform.parent = GameObject.FindGameObjectWithTag("gsui").transform;
        DeckDisplay.transform.localPosition = new Vector3(400, 250, 1);
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
        DeckDisplay = Instantiate(Deck, transform.position, Quaternion.identity) as GameObject;
        DeckDisplay.name = "PlayerHand3";
        DeckDisplay.tag = "PlayerHand3";
        DeckDisplay.transform.parent = GameObject.FindGameObjectWithTag("gsui").transform;
        DeckDisplay.transform.localPosition = new Vector3(150, -86, 1);
        DeckDisplay = Instantiate(Deck, transform.position, Quaternion.identity) as GameObject;
        DeckDisplay.name = "PlayerHand4";
        DeckDisplay.tag = "PlayerHand4";
        DeckDisplay.transform.parent = GameObject.FindGameObjectWithTag("gsui").transform;
        DeckDisplay.transform.localPosition = new Vector3(275, -86, 1);
        DeckDisplay = Instantiate(Deck, transform.position, Quaternion.identity) as GameObject;
        DeckDisplay.name = "PlayerHand5";
        DeckDisplay.tag = "PlayerHand5";
        DeckDisplay.transform.parent = GameObject.FindGameObjectWithTag("gsui").transform;
        DeckDisplay.transform.localPosition = new Vector3(400, -86, 1);
        HideDeck();
        DealNewGame();
    }
    public void UpdateHint(int CardValue)
    {
        GameObject hl = GameObject.FindGameObjectWithTag("HintLabel");
        Debug.Log("UpdateHint() fired! (Player)");
        if (CardValue >= 2 && CardValue <= 10)
        {
            if (CardHintA == 0)
            {
                CardHintA = CardValue;
            }
            else if (CardHintA != 0 && CardHintB == 0)
            {
                CardHintB = CardValue;
            }
            else
            {
                CardHintA = CardHintA + CardHintB;
                CardHintB = CardValue;
            }
            GameManager.Instance.PlayerValue = (CardHintA + CardHintB);
            hl.GetComponent<Text>().text = GameManager.Instance.PlayerValue.ToString();
        }
        else if (CardValue == 1 || CardValue >= 12)
        {
            if (CardHintA == 0)
            {
                CardHintA = 10;
            }
            else if (CardHintA != 0 && CardHintB == 0)
            {
                CardHintB = 10;
            }
            else
            {
                CardHintA = CardHintA + CardHintB;
                CardHintB = 10;
            }
            GameManager.Instance.PlayerValue = (CardHintA + CardHintB);
            hl.GetComponent<Text>().text = GameManager.Instance.PlayerValue.ToString();
        }
        else if (CardValue == 11)
        {
            if (CardHintA == 0)
            {
                CardHintA = 11;
            }
            else if (CardHintA != 0 && CardHintB == 0)
            {
                CardHintB = 11;
            }
            else
            {

                CardHintA = CardHintA + CardHintB;
                if (CardHintA + 11 >= 22)
                {
                    CardHintB = 1;
                    PlayerHasAce = false;
                }
                else if (CardHintA + 11 <= 21)
                {
                    CardHintB = 11;
                    PlayerHasAce = true;
                }

            }
            GameManager.Instance.PlayerValue = (CardHintA + CardHintB);
            hl.GetComponent<Text>().text = GameManager.Instance.PlayerValue.ToString();
        }

    }
    public void UpdateDealer(int CardValue)
    {
        GameObject dh = GameObject.FindGameObjectWithTag("DealerHint");
        Debug.Log("UpdateDealer() fired!");
        if (CardValue >= 2 && CardValue <= 10)
        {
            if (DealerHintA == 0)
            {
                DealerHintA = CardValue;
            }
            else if (DealerHintA != 0 && DealerHintB == 0)
            {
                DealerHintB = CardValue;
            }
            else
            {
                DealerHintA = DealerHintA + DealerHintB;
                DealerHintB = CardValue;
            }
            GameManager.Instance.DealerValue = DealerHintA + DealerHintB;
            dh.GetComponent<Text>().text = GameManager.Instance.DealerValue.ToString();
        }
        else if (CardValue == 1 || CardValue >= 12)
        {
            if (DealerHintA == 0)
            {
                DealerHintA = 10;
            }
            else if (DealerHintA != 0 && DealerHintB == 0)
            {
                DealerHintB = 10;
            }
            else
            {
                DealerHintA = DealerHintA + DealerHintB;
                DealerHintB = 10;
            }
            GameManager.Instance.DealerValue = DealerHintA + DealerHintB;
            dh.GetComponent<Text>().text = GameManager.Instance.DealerValue.ToString();
        } else if (CardValue == 11)
        {
            if (DealerHintA == 0)
            {
                DealerHintA = 11;
            }
            else if (DealerHintA != 0 && DealerHintB == 0)
            {
                DealerHintB = 11;
            }
            else
            {
                DealerHintA = DealerHintA + DealerHintB;
                if (DealerHintA + 11 >= 22)
                {
                    DealerHintB = 1;
                    DealerHasAce = false;
                }
                else if (DealerHintA + 11 <= 21)
                {
                    DealerHintB = 11;
                    DealerHasAce = true;
                }

            }
            GameManager.Instance.DealerValue = (DealerHintA + DealerHintB);
            dh.GetComponent<Text>().text = GameManager.Instance.DealerValue.ToString();
            
        }
    }
    public void DealCards(string ToWho, int CardValue, int CardSuit)
    {
        //1=Club CardC 1-13
        //2=Diamond CardD 14-27
        //3=Heart CardH 28-40
        //4=Spade CardS 41-52
        GameObject GameStatus = GameObject.FindGameObjectWithTag("GameStatus");
        GameStatus.GetComponent<Text>().text = "Dealing Cards...";
        Debug.Log("DealCards() fired! ToWho(" + ToWho + ")");
        GameObject.FindGameObjectWithTag(ToWho).transform.localScale = new Vector3(1, 1, 1);
        if (hasCardPlayed[GetCardIndex(CardValue, CardSuit)] == true)
        {
            Debug.Log("DEBUG: " + hasCardPlayed[GetCardIndex(CardValue, CardSuit)]);
            DealCards(ToWho, Random.Range(1, 13), Random.Range(1, 4));
        } else { 
            //Debug.Log("isPlayed(false)");
            hasCardPlayed[GetCardIndex(CardValue, CardSuit)] = true;
            if (CardValue >= 2 && CardValue <= 10)
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
            }
            else if (CardValue == 11)
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
            else if (CardValue == 12)
            {
                if (CardSuit == 1)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardCJ").GetComponent<SpriteRenderer>().sprite;
                }
                else if (CardSuit == 2)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardDJ").GetComponent<SpriteRenderer>().sprite;
                }
                else if (CardSuit == 3)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardHJ").GetComponent<SpriteRenderer>().sprite;
                }
                else if (CardSuit == 4)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardSJ").GetComponent<SpriteRenderer>().sprite;
                }
            }
            else if (CardValue == 13)
            {
                if (CardSuit == 1)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardCK").GetComponent<SpriteRenderer>().sprite;
                }
                else if (CardSuit == 2)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardDK").GetComponent<SpriteRenderer>().sprite;
                }
                else if (CardSuit == 3)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardHK").GetComponent<SpriteRenderer>().sprite;
                }
                else if (CardSuit == 4)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardSK").GetComponent<SpriteRenderer>().sprite;
                }
            }
            else if (CardValue == 1)
            {
                if (CardSuit == 1)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardCQ").GetComponent<SpriteRenderer>().sprite;
                }
                else if (CardSuit == 2)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardDQ").GetComponent<SpriteRenderer>().sprite;
                }
                else if (CardSuit == 3)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardHQ").GetComponent<SpriteRenderer>().sprite;
                }
                else if (CardSuit == 4)
                {
                    GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardSQ").GetComponent<SpriteRenderer>().sprite;
                }
            }
            //DealerHand1 is face down
            if (ToWho == "DealerHand1")
            {
                GameObject.FindGameObjectWithTag(ToWho).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("CardBack").GetComponent<SpriteRenderer>().sprite;
            }
            if (ToWho.Contains("Player"))
            {
                UpdateHint(CardValue);
            }
            else if (ToWho.Contains("Dealer"))
            {
                UpdateDealer(CardValue);
            }
        }
        //End DealCards()
    }
    public void HitPlayer()
    {
        GameObject GameStatus = GameObject.FindGameObjectWithTag("GameStatus");
        if (GameManager.Instance.isGameOver == false)
        {
            Debug.Log("HitPlayer() fired! A");
            GameStatus.GetComponent<Text>().text = "Player Hits... (Numcards = " + NumPlayerCards.ToString();
            if (NumPlayerCards == 2)
            {
                DealCards("PlayerHand3", Random.Range(1, 13), Random.Range(1, 4));
                NumPlayerCards++;
            }
            else if (NumPlayerCards == 3)
            {
                DealCards("PlayerHand4", Random.Range(1, 13), Random.Range(1, 4));
                NumPlayerCards++;
            }
            else if (NumPlayerCards == 4)
            {
                DealCards("PlayerHand5", Random.Range(1, 13), Random.Range(1, 4));
                NumPlayerCards++;
            }
            else if (NumPlayerCards >= 5)
            {
                StandPlayer();
            }
        }
        else if (GameManager.Instance.isGameOver == true)
        {
            Debug.Log("HitPlayer() fired! B");
            GameStatus.GetComponent<Text>().text = "Deal a New Game!";
            GameStatus.SetActive(true);
        }
    }
    public void StandPlayer()
    {
        GameObject hl = GameObject.FindGameObjectWithTag("HintLabel");
        GameObject GameStatus = GameObject.FindGameObjectWithTag("GameStatus");
        if (GameManager.Instance.isGameOver == false)
        { 
        isPlayerStanding = true;
        Debug.Log("StandPlayer() fired! A");
        GameManager.Instance.PlayerValue = int.Parse(hl.GetComponent<Text>().text);
        HitDealer();
        GameStatus.GetComponent<Text>().text = "Player Stands...";
        }
        else if (GameManager.Instance.isGameOver == true)
        {
            Debug.Log("StandPlayer() fired! B");
            GameStatus.GetComponent<Text>().text = "Deal a New Game!";
            GameStatus.SetActive(true);
        }
    }
    public void HitDealer()
    {
        GameObject GameStatus = GameObject.FindGameObjectWithTag("GameStatus");
        Debug.Log("HitDealer() fired!");
        GameStatus.GetComponent<Text>().text = "Dealer Hits...";
        do
        {
            if (GameManager.Instance.DealerValue <= 16)
            {
                if (NumDealerCards == 2)
                {
                    DealCards("DealerHand3", Random.Range(1, 13), Random.Range(1, 4));
                    NumDealerCards++;
                }
                else if (NumDealerCards == 3)
                {
                    DealCards("DealerHand4", Random.Range(1, 13), Random.Range(1, 4));
                    NumDealerCards++;
                }
                else if (NumDealerCards == 4)
                {
                    DealCards("DealerHand5", Random.Range(1, 13), Random.Range(1, 4));
                    NumDealerCards++;
                }
                else if (NumDealerCards >= 5)
                {
                    StandDealer();
                }
            }
            else if (GameManager.Instance.DealerValue >= 17)
            {
                StandDealer();
            }
        } while (GameManager.Instance.DealerValue <= 16);
        StandDealer();
    }
    public void StandDealer()
    {
        GameObject GameStatus = GameObject.FindGameObjectWithTag("GameStatus");
        isDealerStanding = true;
        Debug.Log("StandDealer() fired!");
        GameStatus.GetComponent<Text>().text = "Dealer Stands...";
        CheckGame();
    }
    public void ShowDealButton()
    {
        GameObject.FindGameObjectWithTag("Respawn").transform.localScale = new Vector3(1, 1, 1);
    }
    public void CheckGame()
    {
        GameObject GameStatus = GameObject.FindGameObjectWithTag("GameStatus");
        GameObject CashLabel = GameObject.FindGameObjectWithTag("CashLabel");
        GameObject hl = GameObject.FindGameObjectWithTag("HintLabel");
        GameObject dh = GameObject.FindGameObjectWithTag("DealerHint");

        //Set isGameOver here.

        //Bust Checks
        if (GameManager.Instance.PlayerValue >= 22 && GameManager.Instance.isGameOver == false)
        {
            if (PlayerHasAce == false)
            {
                Debug.Log("PLAYER BUST! (CheckGame())");
                GameStatus.GetComponent<Text>().text = "Dealer Wins!";
                ShowDealButton();
                GameManager.Instance.isGameOver = true;
            }
            else if (PlayerHasAce == true)
            {
                PlayerHasAce = false;
                GameManager.Instance.PlayerValue = (GameManager.Instance.PlayerValue - 10);
                hl.GetComponent<Text>().text = GameManager.Instance.PlayerValue.ToString();
            }
        }
        if (GameManager.Instance.DealerValue >= 22 && GameManager.Instance.isGameOver == false)
        {
            if (DealerHasAce == false)
            {
                Debug.Log("DEALER BUST! (CheckGame())");
                GameStatus.GetComponent<Text>().text = "Player Wins!";
                ShowDealButton();
                GameManager.Instance.isGameOver = true;
            }
            else if (DealerHasAce == true)
            {
                DealerHasAce = false;
                GameManager.Instance.DealerValue = (GameManager.Instance.DealerValue - 10);
                dh.GetComponent<Text>().text = GameManager.Instance.DealerValue.ToString();
            }
        }

        //Win Condition Check
        if (isDealerStanding == true && isPlayerStanding == true)
        {
            if (GameManager.Instance.DealerValue > GameManager.Instance.PlayerValue && GameManager.Instance.DealerValue <= 21)
            {
                Debug.Log("DEALER WINS! (CheckGame())");
                GameStatus.GetComponent<Text>().text = "Dealer Wins!";
                ShowDealButton();
                GameManager.Instance.isGameOver = true;
            }
            else if (GameManager.Instance.PlayerValue > GameManager.Instance.DealerValue && GameManager.Instance.PlayerValue <= 21)
            {
                Debug.Log("PLAYER WINS! (CheckGame())");
                GameStatus.GetComponent<Text>().text = "Player Wins!";
                ShowDealButton();
                GameManager.Instance.isGameOver = true;
            }
            else if (GameManager.Instance.PlayerValue == GameManager.Instance.DealerValue)
            {
                Debug.Log("PUSH! (CheckGame())");
                GameStatus.GetComponent<Text>().text = "No One Wins!";
                ShowDealButton();
                GameManager.Instance.isGameOver = true;
            }
            else  if (GameManager.Instance.PlayerValue >= 22)
            {
                Debug.Log("DEALER WINS! (CheckGame())");
                GameStatus.GetComponent<Text>().text = "Dealer Wins!";
                ShowDealButton();
                GameManager.Instance.isGameOver = true;
            }
            else if (GameManager.Instance.DealerValue >= 22)
            {
                Debug.Log("PLAYER WINS! (CheckGame())");
                GameStatus.GetComponent<Text>().text = "Player Wins!";
                ShowDealButton();
                GameManager.Instance.isGameOver = true;
            }
        }
       //GameManager.Instance.isGameOver = true;
       //Game is over. Do rewards...
       if (GameManager.Instance.isGameOver == true && GameManager.Instance.isRewarded == false)
       {
            GameManager.Instance.isRewarded = true;
            //DealerValue Checks
            if (GameManager.Instance.DealerValue > GameManager.Instance.PlayerValue && GameManager.Instance.DealerValue <= 21)
            {
                GameManager.Instance.CurrentCash = (GameManager.Instance.CurrentCash - CurrentBet);
                CashLabel.GetComponent<Text>().text = "Cash: $" + GameManager.Instance.CurrentCash.ToString();
            }
            else if (GameManager.Instance.DealerValue >= 22)
            {
                GameManager.Instance.CurrentCash = GameManager.Instance.CurrentCash + CurrentBet;
                CashLabel.GetComponent<Text>().text = "Cash: $" + GameManager.Instance.CurrentCash.ToString();
            }

            //PlayerValue Checks
            if (GameManager.Instance.PlayerValue > GameManager.Instance.DealerValue && GameManager.Instance.PlayerValue <= 21)
            {
                GameManager.Instance.CurrentCash = GameManager.Instance.CurrentCash + CurrentBet;
                CashLabel.GetComponent<Text>().text = "Cash: $" + GameManager.Instance.CurrentCash.ToString();
            } else if (GameManager.Instance.PlayerValue >= 22)
            {
                GameManager.Instance.CurrentCash = GameManager.Instance.CurrentCash - CurrentBet;
                CashLabel.GetComponent<Text>().text = "Cash: $" + GameManager.Instance.CurrentCash.ToString();
            }
        }

        //No $ then Game Over.
        GameObject gop = GameObject.FindGameObjectWithTag("GameOverPanel");
        if (GameManager.Instance.CurrentCash == 0)
        {
            gop.transform.localScale = new Vector3(1, 1, 1);
        }

    }
    public void UpdateBet()
    {
        GameObject BetLabel = GameObject.FindGameObjectWithTag("BetLabel");
        GameObject BetSlider = GameObject.FindGameObjectWithTag("BetSlider");
        if (isDealerStanding == true && isPlayerStanding == true || GameManager.Instance.isGameOver == true)
        {
            if (BetSlider.GetComponent<Slider>().value > GameManager.Instance.CurrentCash)
            {
                SliderVal = GameManager.Instance.CurrentCash;
                CurrentBet = GameManager.Instance.CurrentCash;
                BetSlider.GetComponent<Slider>().value = SliderVal;
                BetLabel.GetComponent<Text>().text = GameManager.Instance.CurrentCash.ToString();
            }
            else if (BetSlider.GetComponent<Slider>().value < GameManager.Instance.CurrentCash)
            {
                SliderVal = BetSlider.GetComponent<Slider>().value;
                CurrentBet = ((int)Mathf.Round(SliderVal));
                BetLabel.GetComponent<Text>().text = BetSlider.GetComponent<Slider>().value.ToString();
            }
        } else
        {
            SliderVal = CurrentBet;
            BetLabel.GetComponent<Text>().text = CurrentBet.ToString();
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
    //End BlackJack.cs
}
