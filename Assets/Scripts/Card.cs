using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;



public class Card : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isHeld = false;
    private float screenWidth;
    private SpriteRenderer sprite;
    private float detectLine; // invisible line that detect card

    // Update is called once per frame
    void Start()
    {
        screenWidth = Screen.width;
        sprite = GetComponent<SpriteRenderer>();
        detectLine = 3.5f;

    }
    void Update()
    { 
        moveObject();
        
    }

    private void moveObject()
    {

        if (isHeld == true)
        {
            checkThink();
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 objectPos = this.gameObject.transform.localPosition;

            if (checkPos())
            {             
                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, 0, 0);
            }
            else
            {
                
                if (mousePos.x >= detectLine || objectPos.x >= detectLine)
                {
                    this.gameObject.transform.localPosition = new Vector3(detectLine, 0, 0);
                }
                else if (mousePos.x <= -detectLine || objectPos.x <= -detectLine)
                {
                    this.gameObject.transform.localPosition = new Vector3(-detectLine, 0, 0);
                }
            }
        }
        else
        {
            resetPos();
        }
    }

    private void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0))
        {
            
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;

            isHeld = true;
        
        }
    }

    private void OnMouseUp()
    {
        isHeld = false;
        Manager.Instance.Think = -1;
        checkResult();
    }

    private bool checkPos() {

        float posX = this.transform.localPosition.x;
        bool canMove;
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (mousePos.x < detectLine && mousePos.x > -detectLine)
        {          
            canMove = true;
        }
        else { canMove = false; }

       return canMove;
    }

    private void checkThink() 
    {
        float posXt = this.transform.localPosition.x;
        if (posXt <= .5f && posXt >= -0.5f)
        {
            Manager.Instance.Think = -1;
        }
            else if (posXt > .5f)
        {
            Manager.Instance.Think = 1;
        }
        else if (posXt < .5f)
        {
            Manager.Instance.Think = 0;
        }   
    }

    private void checkResult(){
        float posX = this.transform.localPosition.x;

        if (posX >= detectLine)
        {
            Manager.Instance.Decide = 1;
            sprite.color = new Color(0, 1f, .5f, 1);
        }
        else if (posX <= -detectLine)
        {
            Manager.Instance.Decide = 0;
            sprite.color = new Color(1, 0, 0, 1);
        }

    }

    private void resetPos() {
        float popPos = this.transform.localPosition.x;
        float decay = (screenWidth/2)/ Mathf.Pow(screenWidth,1.15f); //random that relate to screen size
        if (popPos <= decay && popPos >= -decay) 
        {
            sprite.color = new Color(0, 0, 0, 1);        
            popPos = 0; 
        }
        else { 
            if (popPos > decay)
            {
                popPos = popPos - decay;
            }
            else if (popPos < -decay)
            {
                popPos = popPos + decay;
            }
        
        }
        this.gameObject.transform.localPosition = new Vector3(popPos, 0, 0);
    }

    public SpriteRenderer face;
    public Sprite[] sprites;
    public void ChangeSprite(int newFace)
    {
        face.sprite = sprites[newFace - 1];
    }
}
