using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class DataMgr : MonoBehaviour {



    public Text txy;
    //quizz data
    private QuizzData quizzAll;
    private QuizzLevelElt currentQuizz;

    //players data
    private Players allPlayers = new Players(new List<PlayerData>());
    private PlayerData currentPlayer;

    //scoring data
    public int nbtrueAnwer =0 ;
    public int nbbadAnswer = 0;
    public int notANswer = 0;
    public float timeRemaining = 0;
    public int score = 0;
    public int playerRank = 0;
    private int level = 0;


    public int currentPlayerRank;
    public PlayerAction actionPlayer;

    public static DataMgr instance;

    private string jsonString;

    public QuizzLevelElt CurrentQuizz
    {
        get
        {
            return currentQuizz;
        }
    }


    public QuizzData QuizzAll
    {
        get
        {
            return quizzAll;
        }
    }

    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
            currentQuizz = quizzAll.allQuestion[level];   
        }
    }

   
    public PlayerData CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }
        set
        {
            currentPlayer = value;
        }
    }

    public Players AllPlayers
    {
        get
        {
            return allPlayers;
        }

        set
        {
            allPlayers = value;
        }
    }

    void Awake()
    {
        //create an instance
        instance = this;
    }


    void OnEnable()
    {
     
        #if UNITY_EDITOR
            LoadQuizzData("Assets/Resources/player.json", true);
#else
        txy.text = "should load player";
                 LoadQuizzData("player",true);
#endif

        LoadDataQuizz();
    }

    private void LoadDataQuizz()
    {
        #if UNITY_EDITOR
            LoadQuizzData("Assets/Resources/quizz.json");
        #else
            LoadQuizzData("quizz");
        #endif
    }

    public void ResetStat()
    {
        nbtrueAnwer = 0;
        nbbadAnswer = 0;
        notANswer = 0;
        timeRemaining = 0;
    }

    public bool IsOnLastLevel()
    {
        if(Level == (QuizzAll.allQuestion.Count - 1))
        {
            return true;
        }
        return false;
    }

    public void SetCurrentPlayerRank(TimerUI timer)
    {
        timeRemaining = timeRemaining * timer.CountDownTimer;
        score = ((100 * nbtrueAnwer) / currentQuizz.question.Count); 

        //compute bonus
        if (score == 100)
        {
            playerRank = 3;
            score += Mathf.RoundToInt(timeRemaining * 3);
        }
        else if (score >= 99 && score <= 80)
        {
            playerRank = 2;
            score += Mathf.RoundToInt(timeRemaining * 2.4f);
        }
        else if (score >= 60 && score < 80)
        {
            playerRank = 1;
            score += Mathf.RoundToInt(timeRemaining * 1.8f);
        }
        else
        {
            playerRank = 0;
        }
    }

    public void UpdatePlayerData()
    {
        currentPlayerRank = 0;
        for (int i=0; i < AllPlayers.players.Count; i++)
        {
            if(AllPlayers.players[i].name == currentPlayer.name)
            {     
                    bool isDataExist = false;
                    for (int j = 0; j < AllPlayers.players[i].playerLevelData.Count;j++)
                    {
                        if(AllPlayers.players[i].playerLevelData[j].levelNb == Level)
                        {
                            isDataExist = true;

                            if (AllPlayers.players[i].playerLevelData[j].score< score || score==0)
                                AllPlayers.players[i].playerLevelData[j] = new PlayerLevelData(Level, nbtrueAnwer, nbbadAnswer, notANswer, timeRemaining, score, playerRank);

                           
                        }

                    currentPlayerRank += AllPlayers.players[i].playerLevelData[j].rank;
                   
                    }
                if (!isDataExist)
                    AllPlayers.players[i].playerLevelData.Add(new PlayerLevelData(Level, nbtrueAnwer, nbbadAnswer, notANswer, timeRemaining, score, playerRank));

                currentPlayer = AllPlayers.players[i];
            }
        }
        
        SavePlayerData();
    }

   private void SavePlayerData()
   {
      
       
        txy.text = JsonUtility.ToJson(AllPlayers);
        #if UNITY_EDITOR
        SaveData(JsonUtility.ToJson(AllPlayers), "Assets/Resources/player.json");
#else
              SaveData(JsonUtility.ToJson(AllPlayers), Application.persistentDataPath +"/player.json");
#endif
    }

    public void SaveAction(string data)
    {
        string d = "";
        #if UNITY_EDITOR
            if (System.IO.File.Exists("Assets/Resources/" + currentPlayer.name + ".txt"))
            {
                d = LoadData("Assets/Resources/" + currentPlayer.name + ".txt", true);
            }
            d += "\n" + "-----------" + System.DateTime.Now.ToString() + "--------- " + "\n";
            d += data;
            SaveData(d, "Assets/Resources/" + currentPlayer.name + ".txt");
        #else
            if (System.IO.File.Exists(Application.persistentDataPath +"/"+ currentPlayer.name + ".txt"))
            {
                d = LoadData(Application.persistentDataPath + "/" + currentPlayer.name + ".txt",true);
            }          
            d += "\n"+"-----------" + System.DateTime.Now.ToString() + "--------- "+"\n";
            d+= data;
            SaveData(d, Application.persistentDataPath + "/" + currentPlayer.name + ".txt");                   
        #endif
    }


    //write file on device
    public  void SaveData(string toSave, string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {                           
                writer.Write(toSave);
                writer.Close();
                writer.Dispose();
            }
            fs.Close();
            fs.Dispose();
        }
    }

    public string FormatTime(float secs)
    {
        TimeSpan t = TimeSpan.FromSeconds(secs);
        string ret = string.Format("{1:D2}m:{2:D2}s:{3:D3}ms",
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);

        return ret;
    }

    
    public string LoadData(string path, bool isHistory =false)
    {
        string jsonString = "";
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    if (isHistory)
                        jsonString = reader.ReadToEnd();
                    else
                        jsonString = reader.ReadLine();                 
                    // Do Something with the input. 
                }
                reader.Close();
                reader.Dispose();
            }
            fs.Close();
            fs.Dispose();
        }
        txy.text = jsonString;
        return jsonString;
    }


    public void LoadQuizzData(string filename, bool isPlayerData =false)
    {
        #if UNITY_EDITOR
                jsonString = System.IO.File.ReadAllText(filename);
#else

           txy.text = "should load "+filename ;
                    if (System.IO.File.Exists(Application.persistentDataPath + "/"+filename+".json"))
                    {
   
                        jsonString = System.IO.File.ReadAllText(Application.persistentDataPath + "/"+filename+".json");  
                        txy.text = "loading " + filename;                   
                    }
                    else
                    {
                
                        TextAsset targetFile = Resources.Load<TextAsset>(filename);
                        jsonString = targetFile.text;
                        SaveData(jsonString, Application.persistentDataPath + "/"+filename+".json");
                    } 
#endif

        if (isPlayerData)
        {
            
            AllPlayers = JsonUtility.FromJson<Players>(jsonString);
            txy.text = JsonUtility.ToJson(AllPlayers);

        }
        else
        {   
            quizzAll = JsonUtility.FromJson<QuizzData>(jsonString);
        }
    }

   

}
