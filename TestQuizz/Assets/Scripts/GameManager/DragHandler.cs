using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject itemBeingDragged;
    //test
    public Text rep;
    public GameObject left, right;

    Vector3 startPosition;

    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos = Vector3.zero;
    Vector2 currentSwipe;
    //Vector2 lastPressPos;
    //Vector2 lastCurrentSwipe;
    float  currentangle;
    int cuurrentindex = -1;

    bool isFromLeft = false;
    bool isFromRight = false;
    //bool isFromDown = false;
    
    bool noActionHappened = true;
    //int onChangingDir = 0;
    TimerUI timer;
    public Text goodRep;
    public GameObject anime;
    public Sprite leftAnswer, RightAnswer;

    public float multiplier = 1;
    private float testCount=0;
   
    void OnEnable()
    {
        ResetOptionElt();
        noActionHappened = true; ;
        goodRep.text = "0";
        //InvokeRepeating("CheckIfNoAction", 2.0f, 6.0f);

    }

    void CheckIfNoAction()
    {
        StartCoroutine(showIndication());
    }
    IEnumerator showIndication()
    {
        if (noActionHappened)
        {
            cuurrentindex = GameManager.instance.CurrentQuestionIndex;
            InclineToLeft();
            yield return new WaitForSeconds(2.0f);

            if (!noActionHappened)
                yield break;
            ResetOptionElt();

            yield return new WaitForSeconds(1.0f);
            InclineToRight();   
        }

    }
    void OnDisable()
    {
        CancelInvoke();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        left.SetActive(false);
        right.SetActive(false);
        noActionHappened = false;
        itemBeingDragged = this.gameObject;
        startPosition = transform.position;
        //save began touch 2d point
        firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //onChangingDir = 0;
        testCount = 0;
    }

    /*public void OnDrag(PointerEventData eventData)
    {
        lastPressPos = secondPressPos;
        transform.position = Input.mousePosition;
        
        //save ended touch 2d point
        secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        lastCurrentSwipe = new Vector2(secondPressPos.x - lastPressPos.x, secondPressPos.y - lastPressPos.y);


        //create vector from the two points
        currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

      
        //normalize the 2d vector
        currentSwipe.Normalize();


        //new Vector2(transform.position.x - test.transform.position.x, transform.position.y - test.transform.position.y);    /

        if (isFromLeft || isFromRight)
        {
            //transform.position = new Vector3(transform.position.x, startPosition.y, 0); ;

        }

        if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
        {
            ResetOptionElt();
        }  //swipe down
        if (currentSwipe.y <0f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
        {
            float y = transform.position.y - (0.02f * multiplier);
            testCount+= -(0.02f * multiplier);

            transform.localEulerAngles = new Vector3(0, 0, 0);
            transform.position = new Vector3(startPosition.x, y, 0);

           

            if (!isFromLeft && !isFromRight && testCount>0.2f)
            {
                
                rep.text = "Etes vous sur de vouloir passer la question?";
                   
                    this.GetComponent<Image>().sprite = leftAnswer;
                    this.GetComponent<Image>().color = new Color32(165, 218, 255, 255);
               
             }
            isFromDown = true;
        }
        //swipe left
        if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f  )
        {
            testCount = 0;
            if (isFromDown || isFromRight)
            {
                transform.position = new Vector3(startPosition.x, transform.position.y, 0); ;
            }
            else
            {
                if (lastCurrentSwipe.x > 0)
                {
                    onChangingDir++;
                }
                else
                {
                    onChangingDir = 0;
                }

                if (onChangingDir < 5)
                {

                    rep.text = GameManager.instance.CurrentQuestion.eltquizz.option1;
                    if (transform.localEulerAngles.z < 10f)
                    {
                        float z = transform.localEulerAngles.z + 1;
                        transform.localEulerAngles = new Vector3(0, 0, z);// currentSwipe.y * 100);
                    }

                    //transform.localEulerAngles = new Vector3(0, 0, 30f);
                    // transform.position = new Vector3(transform.position.x, startPosition.y, 0);
                    float x = transform.position.x + (0.02f * multiplier);

                    transform.position = new Vector3(x, startPosition.y, 0);
                    // this.GetComponent<Image>().color = new Color32(19, 167, 255, 255);
                    this.GetComponent<Image>().sprite = leftAnswer;
                    this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    
                    isFromLeft = true;
                    isFromRight = false;
                }
                else
                {
                    ResetOptionElt();
                }
            }
          
           

        }
        //swipe right
        if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        {
            testCount = 0;
            if (isFromDown)
            {
                transform.position = new Vector3(startPosition.x, transform.position.y, 0); ;
            }
            else
            {
                if (lastCurrentSwipe.x < 0)
                {
                    onChangingDir++;
                }
                else
                {
                    onChangingDir = 0;
                }

                if (onChangingDir < 5)
                {

                    rep.text = GameManager.instance.CurrentQuestion.eltquizz.option2;
                    // transform.localEulerAngles = new Vector3(0, 0, -30f);// currentSwipe.y * 100);

                    // transform.position = new Vector3(transform.position.x, startPosition.y, 0);
                    if (transform.localEulerAngles.z == 0 || transform.localEulerAngles.z > 350f) // correspond ) 360-20 
                    {
                        float z = transform.localEulerAngles.z - 1;
                        transform.localEulerAngles = new Vector3(0, 0, z);// currentSwipe.y * 100);
                    }

                    float x = transform.position.x - (0.02f * multiplier);

                    transform.position = new Vector3(x, startPosition.y, 0);
                    //this.GetComponent<Image>().color = new Color32(236, 88, 0, 255);
                    this.GetComponent<Image>().sprite = RightAnswer;
                    this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    isFromRight = true;
                    isFromLeft = false;
                }
                else
                {
                   ResetOptionElt();
                }
            }
        }
    }*/


    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = Input.mousePosition;

        //save ended touch 2d point
        secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        //create vector from the two points
        currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);


        //normalize the 2d vector
        currentSwipe.Normalize();


        //new Vector2(transform.position.x - test.transform.position.x, transform.position.y - test.transform.position.y);    /

        if (isFromLeft || isFromRight)
        {
            transform.position = new Vector3(transform.position.x, startPosition.y, 0); ;

        }

        if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
        {
            ResetOptionElt();
        }  //swipe down
        if (currentSwipe.y < 0f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
        {
            float y = transform.position.y - (0.02f * multiplier);
            testCount += (0.01f);
            if (!isFromLeft && !isFromRight && testCount > 0.05f)
            {
               
                transform.localEulerAngles = new Vector3(0, 0, 0);
                transform.position = new Vector3(startPosition.x, y, 0);

                rep.text = "Etes vous sur de vouloir passer la question?";

                this.GetComponent<Image>().sprite = leftAnswer;
                this.GetComponent<Image>().color = new Color32(165, 218, 255, 255);

            }
            //isFromDown = true;
        }
        //swipe left
        if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        {
            rep.text = GameManager.instance.CurrentQuestion.eltquizz.option1;
         
            //transform.localEulerAngles = new Vector3(0, 0, 30f);
    
            float x = transform.position.x + (0.02f * multiplier*Time.deltaTime);
            LeanTween.rotateZ(this.gameObject, 10f,0.2f);

            transform.position = new Vector3(x, startPosition.y, 0);
            // this.GetComponent<Image>().color = new Color32(19, 167, 255, 255);
            this.GetComponent<Image>().sprite = leftAnswer;
            this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            isFromLeft = true;
            isFromRight = false;

        }
        //swipe right
        if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        {
            rep.text = GameManager.instance.CurrentQuestion.eltquizz.option2;
         

            float x = transform.position.x - (0.02f * multiplier * Time.deltaTime);
            LeanTween.rotateZ(this.gameObject, -10f, 0.2f);

            transform.position = new Vector3(x, startPosition.y, 0);
            this.GetComponent<Image>().sprite = RightAnswer;
            this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            isFromRight = true;
            isFromLeft = false;

        }
    }
    void InclineToLeft()
    {
        if (!noActionHappened)
            return;
        left.SetActive(true);
        right.SetActive(false);
        left.transform.position = new Vector3(left.transform.position.x - 20, left.transform.position.y, left.transform.position.z);
        LeanTween.moveX(left, left.transform.position.x - 40, 1.8f);
       
        rep.text = GameManager.instance.CurrentQuestion.eltquizz.option1;
        LeanTween.rotateZ(this.gameObject, 30f, 1.8f);
        this.GetComponent<Image>().color = new Color32(193, 227, 143, 255);
    }

    void InclineToRight()
    {
        if (!noActionHappened)
            return;
        left.SetActive(false);
        right.SetActive(true);
        right.transform.position = new Vector3(right.transform.position.x +20, right.transform.position.y, right.transform.position.z);
        LeanTween.moveX(right, right.transform.position.x + 40, 1.8f);
        rep.text = GameManager.instance.CurrentQuestion.eltquizz.option2;
        LeanTween.rotateZ(this.gameObject, -30f, 1.8f);// currentSwipe.y * 100);

        this.GetComponent<Image>().color = new Color32(65, 196, 144, 255);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cuurrentindex = GameManager.instance.CurrentQuestionIndex;
        //save ended touch 2d point
        secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        //create vector from the two points
        currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

        //normalize the 2d vector
        currentSwipe.Normalize();

        string Playeranswer="";
        timer = GameManager.instance.timer;

        if (rep.text == GameManager.instance.CurrentQuestion.eltquizz.option1)
                 Playeranswer = "option1";

        if (rep.text == GameManager.instance.CurrentQuestion.eltquizz.option2)
            Playeranswer = "option2";


        if (  transform.localPosition.x > 300 || transform.localPosition.x < -300 && Playeranswer!="")//right==option 2
        {
            if(Playeranswer == GameManager.instance.CurrentQuestion.eltquizz.response)
            {
                DataMgr.instance.nbtrueAnwer++;
                GoodRepAnime();
                GameManager.instance.actionPlayer.AddAction("vrai", (timer.CountDownTimer - (timer.timeBuff * timer.CountDownTimer)), GameManager.instance.CurrentQuestion.eltquizz.question);
            }
            else
            {
                DataMgr.instance.nbbadAnswer++;
                GameManager.instance.actionPlayer.AddAction("faux",(timer.CountDownTimer - (timer.timeBuff * timer.CountDownTimer)), GameManager.instance.CurrentQuestion.eltquizz.question);
            }
            GameManager.instance.questionList[cuurrentindex].Isanswered = true;
            GameManager.instance.LoadNextQuestion();
        }
    
        else if (  transform.localPosition.y < -900)//down
        {
            GameManager.instance.actionPlayer.AddAction("passe",
                (timer.CountDownTimer - (timer.timeBuff * timer.CountDownTimer)), GameManager.instance.CurrentQuestion.eltquizz.question);
            GameManager.instance.LoadNextQuestion();  
               
        }
        else
        {
              
        }
        // this.GetComponent<Image>().color = new Color32(31, 178, 56,255);
        this.GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        // transform.position = startPosition;
        transform.localPosition = new Vector3(-2, -503f, 0); ;
        transform.localEulerAngles = Vector3.zero;
        itemBeingDragged = null;
        rep.text = "";
        ResetFromBool();


    }

    public void ResetOptionElt() {
        left.SetActive(false);
        right.SetActive(false);
        //this.GetComponent<Image>().color = new Color32(31, 178, 56, 255);
        this.GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        transform.localPosition = new Vector3(-2, -503f, 0); ;
        transform.localEulerAngles = Vector3.zero;
        rep.text = "";
        ResetFromBool();
    }
    void ResetFromBool()
    {
        //isFromDown = false;
        isFromLeft = false;
        isFromRight = false;
    }
    public void GoodRepAnime()
    {
        goodRep.text = DataMgr.instance.nbtrueAnwer.ToString();
        anime.transform.localScale = Vector3.one;
        LeanTween.scale(anime, Vector3.zero, 1.0f);
    }
  
}
