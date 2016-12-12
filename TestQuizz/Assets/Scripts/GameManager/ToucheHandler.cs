using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToucheHandler : MonoBehaviour {

    public GameObject answer;

    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos = Vector3.zero;
    Vector2 currentSwipe;
  
    public float multiplier;

    public Text rep,test;
    Vector3 startPosition;
    int onChangingDir;
    bool swipeRight = false;
    bool swipeLeft = false;
    int cuurrentindex = -1;
    TimerUI timer;
    public Text goodRep;
    public GameObject anime;


    public Sprite leftAnswer, rightAnswer;
    

    public bool SwipeRight
    {
        get
        {
            return swipeRight;
        }

        set
        {
            if (swipeRight != value)
            {
                swipeRight = value;
                if (swipeRight)
                {
                    transform.localEulerAngles = new Vector3(0, 0, -7f);
                }
          
            }
           
        }
    }

    public bool SwipeLeft
    {
        get
        {
            return swipeLeft;
        }

        set
        {
            if (swipeLeft != value)
            {
                swipeLeft = value;
                if (swipeLeft)
                {
                    transform.localEulerAngles = new Vector3(0, 0, 7f);
                }
             
            }
        }
    }


    // Use this for initialization
    void Start () {
        startPosition = answer.transform.position;
        goodRep.text = ""+0;
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButton(0))
        {

            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);


            //normalize the 2d vector
            currentSwipe.Normalize();
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {

            }  //swipe down
            if (currentSwipe.y < 0f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                if (!SwipeLeft && !SwipeRight)
                {
                    float y = transform.position.y - (multiplier * Time.deltaTime);
                    transform.position = new Vector3(startPosition.x, y, 0);




                    this.GetComponent<Image>().sprite = leftAnswer;
                    this.GetComponent<Image>().color = new Color32(165, 218, 255, 255);
                }

            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {

                SwipeLeft = true;
                SwipeRight = false;


                float x = Mathf.Clamp(transform.position.x - (multiplier * Time.deltaTime),95f,300f);
          
                
                transform.position = new Vector3(x, startPosition.y - (1 * multiplier / 5), 0);
                this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {

                SwipeRight = true;
                SwipeLeft = false;

                float x = Mathf.Clamp(answer.transform.position.x + (multiplier * Time.deltaTime), 95f, 300f);

                transform.position = new Vector3(x, startPosition.y - (1 * multiplier / 5), 0);
                this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            }
            CHeckPosition();

        }

        if (Input.GetMouseButtonUp(0))
        {
            cuurrentindex = GameManager.instance.CurrentQuestionIndex;
            string Playeranswer = "";
            timer = GameManager.instance.timer;
            if (rep.text == GameManager.instance.CurrentQuestion.eltquizz.option1)
                Playeranswer = "option1";

            if (rep.text == GameManager.instance.CurrentQuestion.eltquizz.option2)
                Playeranswer = "option2";

            if (this.GetComponent<RectTransform>().transform.localPosition.x < -275 || this.GetComponent<RectTransform>().transform.localPosition.x > 275)
            {

                if (Playeranswer == GameManager.instance.CurrentQuestion.eltquizz.response)
                {
                    DataMgr.instance.nbtrueAnwer++;
                    GoodRepAnime();
                    GameManager.instance.actionPlayer.AddAction("vrai", (timer.CountDownTimer - (timer.timeBuff * timer.CountDownTimer)), GameManager.instance.CurrentQuestion.eltquizz.question);
                }
                else
                {
                    DataMgr.instance.nbbadAnswer++;
                    GameManager.instance.actionPlayer.AddAction("faux", (timer.CountDownTimer - (timer.timeBuff * timer.CountDownTimer)), GameManager.instance.CurrentQuestion.eltquizz.question);
                }
                GameManager.instance.questionList[cuurrentindex].Isanswered = true;
                GameManager.instance.LoadNextQuestion();
            }
            if (this.GetComponent<RectTransform>().transform.localPosition.y < -600f)
            {

                GameManager.instance.actionPlayer.AddAction("passe",
                (timer.CountDownTimer - (timer.timeBuff * timer.CountDownTimer)), GameManager.instance.CurrentQuestion.eltquizz.question);
                GameManager.instance.LoadNextQuestion();
            }


            answer.transform.position = startPosition;

            answer.transform.localEulerAngles = Vector3.zero;
            rep.text = "";
            SwipeRight = false;
            SwipeLeft = false;
            this.GetComponent<Image>().color = new Color32(255, 255, 255, 1);
        }
#else
             
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    firstPressPos = new Vector2(touch.position.x, touch.position.y);
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    secondPressPos = new Vector2(touch.position.x, touch.position.y);
                    //create vector from the two points
                    currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                    //normalize the 2d vector
                    currentSwipe.Normalize();
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {

                    }  //swipe down
                    if (currentSwipe.y < 0f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        if (!SwipeLeft && !SwipeRight)
                        {
                            float y = transform.position.y - (multiplier * Time.deltaTime);
                            transform.position = new Vector3(startPosition.x, y, 0);

                      

                            this.GetComponent<Image>().sprite = leftAnswer;
                            this.GetComponent<Image>().color = new Color32(165, 218, 255, 255);
                        }

                    }
                    //swipe left
                    if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {

                        SwipeLeft = true;
                        SwipeRight = false;


                         float x = Mathf.Clamp(transform.position.x - (multiplier * Time.deltaTime),95f,300f);
                        transform.position = new Vector3(x, startPosition.y - (1 * multiplier / 5), 0);

                        this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

                    }
                    //swipe right
                    if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {

                        SwipeRight = true;
                        SwipeLeft = false;

                        float x = Mathf.Clamp(transform.position.x + (multiplier * Time.deltaTime),95f,300f);
                        transform.position = new Vector3(x, startPosition.y - (1 * multiplier / 5), 0);
                        this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);


                    }
                    CHeckPosition();
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    cuurrentindex = GameManager.instance.CurrentQuestionIndex;
                    string Playeranswer = "";
                    timer = GameManager.instance.timer;
                    if (rep.text == GameManager.instance.CurrentQuestion.eltquizz.option1)
                        Playeranswer = "option1";

                    if (rep.text == GameManager.instance.CurrentQuestion.eltquizz.option2)
                        Playeranswer = "option2";

                    if (this.GetComponent<RectTransform>().transform.localPosition.x < -200 || this.GetComponent<RectTransform>().transform.localPosition.x > 200)
                    {

                        if (Playeranswer == GameManager.instance.CurrentQuestion.eltquizz.response)
                        {
                            DataMgr.instance.nbtrueAnwer++;
                            GoodRepAnime();
                            GameManager.instance.actionPlayer.AddAction("vrai", (timer.CountDownTimer - (timer.timeBuff * timer.CountDownTimer)), GameManager.instance.CurrentQuestion.eltquizz.question);
                        }
                        else
                        {
                            DataMgr.instance.nbbadAnswer++;
                            GameManager.instance.actionPlayer.AddAction("faux", (timer.CountDownTimer - (timer.timeBuff * timer.CountDownTimer)), GameManager.instance.CurrentQuestion.eltquizz.question);
                        }
                        GameManager.instance.questionList[cuurrentindex].Isanswered = true;
                        GameManager.instance.LoadNextQuestion();
                    }
                    if (this.GetComponent<RectTransform>().transform.localPosition.y < -600f)
                    {

                        GameManager.instance.actionPlayer.AddAction("passe",
                        (timer.CountDownTimer - (timer.timeBuff * timer.CountDownTimer)), GameManager.instance.CurrentQuestion.eltquizz.question);
                        GameManager.instance.LoadNextQuestion();
                    }


                    answer.transform.position = startPosition;

                    answer.transform.localEulerAngles = Vector3.zero;
                    rep.text = "";
                    SwipeRight = false;
                    SwipeLeft = false;
                    this.GetComponent<Image>().color = new Color32(255, 255, 255, 1);
                    break;
            }
        }
#endif




    }
    void CHeckPosition()
    {

        if (this.GetComponent<RectTransform>().transform.localPosition.y < -560f)
        {

            rep.text = "Etes vous sur de vouloir passer la question?";
            return;
        }

        if (this.GetComponent<RectTransform>().transform.localPosition.x > -50 && this.GetComponent<RectTransform>().transform.localPosition.x <50 )
        {    
            rep.text = "...";
            transform.localEulerAngles = Vector3.zero;
         
        }
        else if (this.GetComponent<RectTransform>().transform.localPosition.x > 50)
        {
            transform.localEulerAngles = new Vector3(0, 0, -7f);
            rep.text = GameManager.instance.CurrentQuestion.eltquizz.option2;
            this.GetComponent<Image>().sprite = rightAnswer;
            this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        else if (this.GetComponent<RectTransform>().transform.localPosition.x < -50)
        {
            transform.localEulerAngles = new Vector3(0, 0, 7f);
            rep.text = GameManager.instance.CurrentQuestion.eltquizz.option1;
            this.GetComponent<Image>().sprite = leftAnswer;
            this.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        } 
    }

    public void GoodRepAnime()
    {
        goodRep.text = DataMgr.instance.nbtrueAnwer.ToString();
        anime.transform.localScale = Vector3.one;
        LeanTween.scale(anime, Vector3.zero, 1.0f);
    }
}
