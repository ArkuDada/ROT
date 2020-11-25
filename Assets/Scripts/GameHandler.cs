using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameHandler : MonoBehaviour
{
    public Camera cam;
    public GameObject Main;
    public GameObject Menu;
    public int deck = 0;
    public int card = 0;

    private int decision;
    private int think;
    private bool displayThink;

    public string[] speech;
    public int speechCounter = 0;
    public int dialogueAmont;

    private int spriteIndex;
   

    private CardData cardData;
    [SerializeField] private StatusBar statusBar;
    [SerializeField] private Textbox textbox;
    [SerializeField] private Card cardObject;
    [SerializeField] private AudioPlayer AudioPlayer;

    void Start()
    {
        cardData = JsonUtility.FromJson<CardData>(AllCardData.text);
        speech = cardData.Decks[deck].Cards[card].Speech;
        Manager.Instance.Decide = -1;
        Manager.Instance.Think = -1;
        textbox.updateText("RoT v0.0.1c");
        Menu.SetActive(true);
        string testString = JsonUtility.ToJson(cardData);
        Debug.Log(testString);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Menu.SetActive(true);
            //Main.SetActive(false);
            textbox.updateText("Paused");
        }
        checkThink();
        checkDecision();
    }
   
    void checkThink() {        
        if (displayThink) {
            think = Manager.Instance.Think;
            if (think != -1)
            {
                Manager.Instance.Status = cardData.Decks[deck].Cards[card].Decisions[think].Outcome;
                textbox.updateText(cardData.Decks[deck].Cards[card].Decisions[think].SwipeText);
                statusBar.updateThink();
            }
            else
            {
                Manager.Instance.Status = new int[] { 0, 0, 0 };
                textbox.updateText(speech[dialogueAmont-1]);
                statusBar.updateThink();
            }
        }
        else
        {
            Manager.Instance.Status = new int[] { 0, 0, 0 };          
            statusBar.updateThink();
        }

    }

    void checkDecision()
    {
        decision = Manager.Instance.Decide;
        if (decision != -1)
        {
            if (checkDeck())
            {
                if (checkDialogue())
                {
                    Manager.Instance.Status = cardData.Decks[deck].Cards[card].Decisions[decision].Outcome;
                    statusBar.updateBar();
                    displayThink = false;
                    getNewCard();
                }
                if (speechCounter == dialogueAmont)
                {
                    displayThink = true;
                }
                Debug.Log(speechCounter);
            }
            Manager.Instance.Decide = -1;
        }
    }

    void getNewCard()
    {
        card++;
        speech = cardData.Decks[deck].Cards[card].Speech;
        dialogueAmont = arrayCount(speech);
        spriteIndex = cardData.Decks[deck].Cards[card].Picture;
        cardObject.ChangeSprite(spriteIndex);
        textbox.updateText(speech[0]);
        AudioPlayer.playSound(1);
        speechCounter = 1;

    }
   
    bool checkDialogue() 
    {
        if (speechCounter < dialogueAmont)
        {
            AudioPlayer.playSound(0);
            textbox.updateText(speech[speechCounter]);
            speechCounter++;           
            return false;
        }
        else
        {
            speechCounter = 1;
            return true;
        }
    }

    public int cardAmount;
    public int deckAmount;
    bool checkDeck() 
    {
        deckAmount = cardData.DeckAmount;
        cardAmount = cardData.Decks[deck].CardAmount;
        if (deck < deckAmount)
        {
            if (card < cardAmount)
            {
                return true;
            }
            else
            {
                deck++;
                card = -1;
                getNewCard();          
                return false;
            }
        }
        else { 
            newGame();
            return false;
        }
    }

    public void newGame()
    {
        //Main.SetActive(true);
        deck = 0;
        card = 0;
        speechCounter = 0;
        
        speech = cardData.Decks[deck].Cards[card].Speech;      
        dialogueAmont = arrayCount(speech);      
        textbox.updateText(speech[speechCounter]);

        speechCounter++;

        spriteIndex = cardData.Decks[deck].Cards[card].Picture;
        cardObject.ChangeSprite(spriteIndex);

        textbox.newGame();
        statusBar.resetBar();
        Menu.SetActive(false);
    }

    public void resume()
    {
        //Main.SetActive(true);
        Menu.SetActive(false);
        textbox.updateText(speech[speechCounter]);
    }

    //count child in array
    int arrayCount(string[] speeches) {
        int c = 0;
        foreach (string i in speeches)
        {
            c++;
        }
        return c;
    }

    public TextAsset AllCardData;
    [Serializable]
    private class CardData
    {
        public int DeckAmount;
        public List<MyDecks> Decks;
    }
    [Serializable]
    private class MyDecks
    {
        public int CardAmount;
        public List<MyCard> Cards;
    }
    [Serializable]
    private class MyCard
    {
        public int Picture;
        public string[] Speech;
        public List<MyDecision> Decisions;
       
    }
    [Serializable]
    private class MyDecision
    {
        public string SwipeText;
        public int[] Outcome;
    } 
}
