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
    public GameObject EndScreen;
    public int deck = 0;
    public int card = 0;
    public int cardAmount;

    public int playerTeam = 0;
    private int decision;
    private int think;
    private bool displayThink;
    public int importantCount = 0;

    private int skip = 0;
    public string name;
    public string[] speech;
    public int speechCounter = 0;
    public int dialogueAmont;

    private int spriteIndex;
    
    public int playableDeck;
    private CardData cardData;
    [SerializeField] private StatusBar statusBar;
    [SerializeField] private CharacterName characterName;
    [SerializeField] private Textbox textbox;
    [SerializeField] private Card cardObject;
    [SerializeField] private AudioPlayer AudioPlayer;

    void Start()
    {
        cardData = JsonUtility.FromJson<CardData>(AllCardData[deck].text);
        cardAmount = cardData.CardAmount;
        speech = cardData.Cards[card].Speech;
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
            Main.SetActive(false);
            //Main.transform.Find("Card").gameObject.SetActive(false)
            textbox.updateText("Paused");
        }

        checkThink();
        checkDecision();
        checkEnd();
    }

    void checkThink()
    {
        if (dialogueAmont == speechCounter)
        {
            think = Manager.Instance.Think;
            if (think != -1)
            {
                Manager.Instance.Status = cardData.Cards[card].Decisions[think].Outcome;
                textbox.updateText(cardData.Cards[card].Decisions[think].SwipeText);
                statusBar.updateThink();
            }
            else
            {
                textbox.updateText(speech[dialogueAmont - 1]);

                Manager.Instance.Status = new int[] {0, 0, 0};
                statusBar.updateThink();
            }
        }
        else
        {
            Manager.Instance.Status = new int[] {0, 0, 0};
            statusBar.updateThink();
        }
    }

    void checkDecision()
    {
        decision = Manager.Instance.Decide;
        if (decision != -1)
        {
            if (checkCard())
            {
                if (checkDialogue())
                {
                    Manager.Instance.Status = cardData.Cards[card].Decisions[decision].Outcome;
                    statusBar.updateBar();
                    importantDecision(cardData.Cards[card].Decisions[decision].Important);


                    skip = cardData.Cards[card].Decisions[decision].Skip;
                    for (int i = 0; skip >= i; skip--)
                    {
                        getNewCard();
                    }
                } 
            }

            Debug.Log(speechCounter);
            Manager.Instance.Decide = -1;
        }
    }

    void importantDecision(int importantChoice)
    {
        if (importantChoice > 0)
        {
            importantCount += importantChoice;
        }
        else if (importantChoice == -1)
        {
            playerTeam = 1;
        }
        else if (importantChoice == -2)
        {
            playerTeam = 2;
        }
        else if (importantChoice == -3)
        {
            Manager.Instance.endStatus = 7;
        }
    }

    void getNewCard()
    {
        card++;
        speech = cardData.Cards[card].Speech;
        name = cardData.Cards[card].Name;
        spriteIndex = cardData.Cards[card].Picture;
        
        cardObject.ChangeSprite(spriteIndex);
        characterName.updateText(name);
        textbox.updateText(speech[0]);
        AudioPlayer.playSound(1);
        dialogueAmont = arrayCount(speech);
        speechCounter = 1;

        if (cardData.Cards[card].Team != 0 && cardData.Cards[card].Team != playerTeam)
        {
            getNewCard();
        }
        else if (importantCount < cardData.Cards[card].Require && cardData.Cards[card].Require > 0)
        {
            getNewCard();
        }
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

    bool checkCard()
    {
        if (card < cardData.CardAmount)
        {
            return true;
        }
        else
        {
            if (deck < playableDeck )
            {
                deck++;
                cardData = JsonUtility.FromJson<CardData>(AllCardData[deck].text);
                importantCount = 0;
            }
            else
            {
                Manager.Instance.endStatus = 0;
            }
            return false;
        }
    }

    void checkEnd()
    {
        if (Manager.Instance.endStatus != -1)
        {
            Main.SetActive(false);
            EndScreen.SetActive(true);
            EndScreen.GetComponent<EndScreen>().updateEnd(Manager.Instance.endStatus);
        }
    }

    public void newGame()
    {
        Main.SetActive(true);
        EndScreen.SetActive(false);
        Manager.Instance.endStatus = -1;
        // Main.transform.Find("Card").gameObject.SetActive(true);
        card = 0;
        speechCounter = 0;
        name = cardData.Cards[card].Name;
        characterName.updateText(name);
        
        speech = cardData.Cards[card].Speech;
        dialogueAmont = arrayCount(speech);
        textbox.updateText(speech[speechCounter]);

        speechCounter++;

        spriteIndex = cardData.Cards[card].Picture;
        cardObject.ChangeSprite(spriteIndex);

        textbox.newGame();
        statusBar.resetBar();
        Menu.SetActive(false);
    }

    public void resume()
    {
        Main.SetActive(true);
        //Main.transform.Find("Card").gameObject.SetActive(true);
        Menu.SetActive(false);
        textbox.updateText(speech[speechCounter]);
    }

    //count child in array
    int arrayCount(string[] speeches)
    {
        int c = 0;
        foreach (string i in speeches)
        {
            c++;
        }

        return c;
    }

    public TextAsset[] AllCardData;

    [Serializable]
    private class CardData
    {
        public int CardAmount;
        public List<MyCard> Cards;
    }

    [Serializable]
    private class MyCard
    {
        public int Team;
        public int Require;
        public string Name;
        public int Picture;
        public string[] Speech;
        public List<MyDecision> Decisions;
    }

    [Serializable]
    private class MyDecision
    {
        public string SwipeText;
        public int[] Outcome;
        public int Important;
        public int Skip;
    }
}