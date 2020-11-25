using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject Handler;
    public Button m_newGame,m_resume;
    // Start is called before the first frame update
    void Start()
    {
        m_newGame.onClick.AddListener(newGame);
        m_resume.onClick.AddListener(resume);
    }

    // Update is called once per frame
    void newGame() 
    {
        Handler.GetComponent<GameHandler>().newGame();
    }
    void resume() 
    {
        Handler.GetComponent<GameHandler>().resume();
    }
}
