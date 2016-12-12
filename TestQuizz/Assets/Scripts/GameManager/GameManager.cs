using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public enum rank { perferct,good,medium,bad}
public class GameManager : MonoBehaviour {
   
    public Text question;
    int currentQuestionIndex =-1;
    LoadedQuestion currentQuestion = new LoadedQuestion(false, new QuizElt());

    public List<LoadedQuestion> questionList = new List<LoadedQuestion>();
   
    public static GameManager instance;
    public TimerUI timer;
    public GameObject resultPanel;
    public GameObject rankPanel;
    public GameObject gamePanel;
    public GameObject questionPanel;
    public PlayerAction actionPlayer;
    public Sprite[] allimgQuest;
    public Image imgQuest;
    public Text nbQuestion;

    public LoadedQuestion CurrentQuestion
    {
        get
        {
            return currentQuestion;
        }
    }

    public int CurrentQuestionIndex
    {
        get
        {
            return currentQuestionIndex;
        }

    }

    void Awake()
    {
        //create an instance
        instance = this;
    
    }

    void OnEnable()
    {
        FindObjectOfType<MenuManager>().MenuGameState = MenuState.Game;
    }

   public void GoToNextLevel()
    {
        
        actionPlayer = new PlayerAction();
        int nextLevel = DataMgr.instance.Level + 1;
        if (nextLevel < DataMgr.instance.QuizzAll.allQuestion.Count)
        {
            DataMgr.instance.Level++;
            StartGame();
        }
        else
        {
            rankPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
    
    }

    public void StartGame()
    {
        actionPlayer = new PlayerAction();

        //DataMgr.instance.Level = DataMgr.instance.CurrentPlayer.playerLevelData.Count; //PlayerPrefs.GetInt("Level");

        DataMgr.instance.ResetStat();
        timer = FindObjectOfType<TimerUI>();
        currentQuestionIndex = 0;
        questionList.Clear();

        for(int i =0; i < DataMgr.instance.CurrentQuizz.question.Count; i++)
        {
            questionList.Add(new LoadedQuestion(false, DataMgr.instance.CurrentQuizz.question[i]));
        }
        currentQuestion = questionList[0];
        question.text = questionList[0].eltquizz.question;
        nbQuestion.text = "/"+questionList.Count;
        timer.StartCoolDown();
    }

    public void LoadNextQuestion(){
        questionPanel.transform.localEulerAngles = new Vector3(0,180,0);
        imgQuest.sprite = allimgQuest[(int)Random.Range(0, 2)];
        LeanTween.rotateY(questionPanel, 0, 0.3f);
        //questionPanel.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
     
        //LeanTween.scale(questionPanel, Vector3.one, 0.8f);

        questionList[currentQuestionIndex].timeRemaining = timer.timeBuff;
        if (IsGameEnded())
        {
            DataMgr.instance.SaveAction(actionPlayer.actionText);
            ComputeScore();
            DataMgr.instance.UpdatePlayerData();
            gamePanel.SetActive(false);
            resultPanel.SetActive(true);
          
            return;
        }

        int generatedIndex = Random.Range(0, questionList.Count - 1);

        if (currentQuestionIndex != generatedIndex)
            currentQuestionIndex = generatedIndex;
        else
        {
            if (currentQuestionIndex < questionList.Count - 1)
            {
                currentQuestionIndex++;
            }
            else
                currentQuestionIndex = 0;
        }


        bool isFindNext = false;
        for (int i = currentQuestionIndex; i < questionList.Count; i++)
        {
            if(!questionList[i].Isanswered && !questionList[i].IsTimeOver)
            {
                currentQuestionIndex = i;
                isFindNext = true;
                break;
            }
        }

        if(!isFindNext)
        {
            for (int i =0 ; i < currentQuestionIndex; i++)
            {
                if (!questionList[i].Isanswered && !questionList[i].IsTimeOver)
                {
                    currentQuestionIndex = i;
                 
                    break;
                }
            }

        }

   

        currentQuestion = questionList[currentQuestionIndex];
        question.text = questionList[currentQuestionIndex].eltquizz.question;

        timer.StartCoolDown();

        if (questionList[currentQuestionIndex].timeRemaining != -2)
        {
            timer.timeBuff = questionList[CurrentQuestionIndex].timeRemaining;
        }
 
    }

    private bool IsGameEnded()
    {
     
        for(int i =0; i < questionList.Count; i++)
        {
            
            if (!questionList[i].Isanswered && !questionList[i].IsTimeOver)
                return false;
        }
        return true;
    }

    private void ComputeScore()
    {  
        question.text = "fin du jeu";
        float timeRemaining = 0;
        //compute not answered question
        for (int i = 0; i < questionList.Count; i++)
        {
            if (!questionList[i].Isanswered && questionList[i].IsTimeOver)
            {
                DataMgr.instance.notANswer++;
            }
            timeRemaining += questionList[i].timeRemaining;
        }

        DataMgr.instance.timeRemaining = timeRemaining;



        DataMgr.instance.SetCurrentPlayerRank(timer);
    }
}
