using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public struct PLayerScore
{
    public string namePlayer;
    public int score;
}
public class ScoreUI : MonoBehaviour {

    public DataMgr data;
    public GridManager gridMgr;
    public ScoreElt[] prefabScores;
    public ScoreElt lastScores;
    int score;
    List<PlayerData> allPlayers = new List<PlayerData>();
    List<PLayerScore> scores = new List<PLayerScore>();
    List<PLayerScore> sortedPlayer = new List<PLayerScore>();
    IEnumerable<PLayerScore> sortRequest;

    void OnEnable() {
        FindObjectOfType<MenuManager>().MenuGameState = MenuState.Score;      
        allPlayers.AddRange(data.AllPlayers.players);
        PopulateGrid();
    }
  

    void PopulateGrid()
    {
        //gridMgr.ResetContent();
        //sort score
        int nbtopscore;
        scores.Clear();
        for(int i = 0; i < allPlayers.Count; i++)
        {
            score = 0;

            for (int j = 0; j < allPlayers[i].playerLevelData.Count; j++)
            {
                score += allPlayers[i].playerLevelData[j].score;
            }
            PLayerScore p = new PLayerScore();
            p.namePlayer = allPlayers[i].name;
            p.score = score;
            scores.Add(p);
          
        }
        sortedPlayer.Clear();
        sortRequest = from pLayerScore in scores orderby pLayerScore.score select pLayerScore;
      
        foreach (PLayerScore p in sortRequest)
        {
            sortedPlayer.Add(p);
        }
        sortedPlayer.Reverse();
        if (sortedPlayer.Count >= prefabScores.Length)
        {
            nbtopscore = 5;
        }
        else
            nbtopscore = sortedPlayer.Count;

        for (int i = 0; i < nbtopscore; i++)
        {
            prefabScores[i].namePlayer.text = sortedPlayer[i].namePlayer;
            prefabScores[i].score.text = sortedPlayer[i].score.ToString();
        }

        score = 0;
        if (data.CurrentPlayer != null)
        {
            lastScores.gameObject.SetActive(true);
            lastScores.namePlayer.text = data.CurrentPlayer.name;
            for (int i = 0; i < allPlayers.Count; i++)
            {
                if(allPlayers[i].name == data.CurrentPlayer.name)
                {
                    for (int j = 0; j < allPlayers[i].playerLevelData.Count; j++)
                    {
                        score += allPlayers[i].playerLevelData[j].score;
                    }
                    break;
                }          
            }      
            lastScores.score.text = score.ToString();
        }
        else
        {
            lastScores.gameObject.SetActive(false);
        }       
    }
}
