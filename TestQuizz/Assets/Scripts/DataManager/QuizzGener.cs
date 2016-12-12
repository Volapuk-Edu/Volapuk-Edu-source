using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class QuizzGener : MonoBehaviour {
    // Use this for initialization
    void Start()
    {

        /*List<QuizElt> quizz = new List<QuizElt>();

        QuizElt q = new QuizElt();
        q.question = "Où se trouvent les glandes sudoripares d'un chien ?";
        q.response = "Sous ses pattes";
        quizz.Add(q);

        QuizElt q1 = new QuizElt();
        q1.question = "Qui raconte les aventures de Sherlock Holmes ";
        q1.response = "Watson";
        quizz.Add(q1);

        QuizzLevelElt quest = new QuizzLevelElt();

        quest.question = quizz;
        List<QuizzLevelElt> allquest = new List<QuizzLevelElt>();
        allquest.Add(quest);
        allquest.Add(quest);

        QuizzData quizzdata = new QuizzData();
        quizzdata.allQuestion = allquest;
     

        string json = JsonUtility.ToJson(quizzdata);
        Save(json, "Assets/Resources/quizz.json");
        */

         //List<PlayerLevelData> playerLevelData = new List<PlayerLevelData>();
        List<PlayerData> playersData = new List<PlayerData>();
        /*for(int i = 0; i < 4; i++)
        {
            playerLevelData.Clear();
            int rd = Random.RandomRange(0, 16);
            playerLevelData.Add(new PlayerLevelData(i, rd + 10,(10- rd),0,8*i,i+10, rd));
            playersData.Add(new PlayerData("Player" + i, playerLevelData));
        }*/

        Players p = new Players(playersData);
        string json = JsonUtility.ToJson(p);
        Save(json, "Assets/Resources/player.json");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save(string json, string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
                writer.Close();
                writer.Dispose();
            }
            fs.Close();
            fs.Dispose();
        }
    }
}
