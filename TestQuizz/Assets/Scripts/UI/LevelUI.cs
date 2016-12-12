using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour {

    //public GridManager levelGrid;
    public GridLayoutGroup levelGrid;
    public LevelElt levelElt;
    public DataMgr data;
    public PlayerData currentPlayer;
    public GameManager game;
    public Button next;
    public Button previous;



    void OnEnable()
    {
        FindObjectOfType<MenuManager>().MenuGameState = MenuState.Level;
        PopulateGrid();
    }



    void PopulateGrid()
    {
        currentPlayer = data.CurrentPlayer;
        if (data.QuizzAll.allQuestion.Count < 12)
        {
            previous.gameObject.SetActive(false);
            next.gameObject.SetActive(false);
        }
        foreach (Transform child in levelGrid.transform)
        {
           Destroy(child.gameObject);
        }

        // retrieve all played level
        List<int> achievedLevel = new List<int>();
        if (currentPlayer.playerLevelData.Count > 0)
        {
            for (int i = 0; i < data.QuizzAll.allQuestion.Count; i++)
            {
                for (int j = 0; j < currentPlayer.playerLevelData.Count; j++)
                {
                    if (currentPlayer.playerLevelData[j].levelNb == i)
                    {
                        achievedLevel.Add(i);
                    }
                }
            }
        }
     

        for (int i = 0; i < data.QuizzAll.allQuestion.Count; i++)
        {      
            if (achievedLevel.Contains( i))
            {
                levelElt.levelNb.text = (i + 1) + "";
                levelElt.SetStarsColor(currentPlayer.playerLevelData[i].rank, false);
                //levelGrid.AddContent(120, levelElt.gameObject);           
            }
            else
            {
                levelElt.levelNb.text = (i + 1) + "";
                levelElt.SetStarsColor(0, false);

            }
            GameObject go = Instantiate(levelElt.gameObject);
            go.transform.SetParent(levelGrid.transform, false);
            go.transform.localPosition = Vector3.zero;
        }


    }
    public void LoadCorrectLevel(int level)
    {
        data.Level = level;
        game.gameObject.SetActive(true);
        
        GameManager.instance.StartGame();
        this.gameObject.SetActive(false);
    }
}
