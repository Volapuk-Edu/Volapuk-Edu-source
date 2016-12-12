using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerUI : MonoBehaviour
{

    #region fields
    public Color[] colors = new Color[3];
    private float countDownTimer = 16;
 
    public Image[] imgsCountDown;
   
   

    Gradient g = new Gradient();
    GradientColorKey[] gck = new GradientColorKey[3];
    GradientAlphaKey[] gak = new GradientAlphaKey[3];

    public float blinkSpeed = 2f;
    public float alphaTopLimit = 0.4f;
    public float alphaBottomLimit = 0.1f;
    public float blinkTriggerPercentage = 0.75f;
    float value = 0f;
    float tmp;
    bool up;
    Color[] resetColors;
    bool isBlinking = false;
    //float lastTime;
    //bool isStarted = false;
    public float timeBuff = -1f;

    bool isTimeOver = false;

    public bool IsTimeOver
    {
        get { return isTimeOver; }
        set {
            isTimeOver = value;
            if (isTimeOver) {

                //QuizzLoader.instance.EndGame();
                int currentindex = GameManager.instance.CurrentQuestionIndex;
                if (currentindex != -1 )
                {
                    GameManager.instance.questionList[currentindex].IsTimeOver = true;
                    GameManager.instance.LoadNextQuestion();
                }
     
                       
            }
        }
    }
    #endregion


    #region methods

    // Use this for initialization
    public float CountDownTimer
    {
        get { return countDownTimer; }
        set { countDownTimer = value; }
    }

    void OnEnable()
    {
       
        isTimeOver = false;
        gck = new GradientColorKey[colors.Length];
        gak = new GradientAlphaKey[colors.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            gck[i].color = colors[i];
            gck[i].time = 1f / colors.Length * i;
            gak[i].alpha = 1f;
            gak[i].time = 1f / colors.Length * i;
        }
        g.SetKeys(gck, gak);
        resetColors = new Color[imgsCountDown.Length];
        for (int i = 0; i < resetColors.Length; i++)
            resetColors[i] = imgsCountDown[i].color;

        //StartCoolDown();


    }


    // Update is called once per frame
    void Update()
    {
        
        if (timeBuff > 0f)
        {
            timeBuff -= Time.deltaTime / CountDownTimer;
            if (timeBuff < 1-blinkTriggerPercentage)
            {
                if (up)
                {
                    value += blinkSpeed * Time.deltaTime;
                    if (value > alphaTopLimit) up = false;
                }
                else
                {
                    value -= blinkSpeed * Time.deltaTime;
                    if (value <= alphaBottomLimit) up = true;
                }
                tmp = value;
                isBlinking = true;
            }
            else
            {
                isBlinking = false;
            }

            for (int i = 0; i < imgsCountDown.Length; i++)
            {
                imgsCountDown[i].fillAmount = timeBuff;

                //if (i != 0)
                {
                    if (isBlinking)
                        imgsCountDown[i].color = new Color(g.Evaluate(1-timeBuff).r, g.Evaluate(1-timeBuff).g, g.Evaluate(1-timeBuff).b, tmp);
                    else
                        imgsCountDown[i].color = new Color(g.Evaluate(1-timeBuff).r, g.Evaluate(1-timeBuff).g, g.Evaluate(1-timeBuff).b, imgsCountDown[i].color.a);
                }
            }
        }
        else
        {
            //ResetAnimations();
            IsTimeOver = true;
        }
    }

  

    void OnDisable()
    {
        //stopTimer();
        StopCoolDown();
    }

    public void StartCoolDown()
    {
        ResetAnimations();
        //this.cooldownDuration = duration;
        timeBuff = 1;
    }

    void ResetAnimations()
    {
        for (int i = 0; i < resetColors.Length; i++)
        {
            imgsCountDown[i].color = resetColors[i];
            imgsCountDown[i].fillAmount = 1;
        }
    }

     void StopCoolDown()
    {
        ResetAnimations();
        timeBuff = -1;

    }

 

    #endregion
}
