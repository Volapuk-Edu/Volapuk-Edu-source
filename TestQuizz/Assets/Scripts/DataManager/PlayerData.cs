using System;
using System.Collections.Generic;


[Serializable]
public class PlayerData  {

    public string name;
    public List<PlayerLevelData> playerLevelData;

    public PlayerData(string _name, List<PlayerLevelData> _levelData)
    {
        this.name = _name;
        this.playerLevelData = _levelData;
    }
}

[Serializable]
public class PlayerLevelData
{
    public int levelNb;
    public int nbtrueAnwer;
    public int nbbadAnswer;
    public int notAnswer;
    public float timeRemaining;
    public int score;
    public int rank;

    public PlayerLevelData(int _levelNb, int _nbtrueAnswer, int _nbbadAnswer, int _notAnswer,float _timeRemaing,int _score, int _rank)
    {
        this.levelNb = _levelNb;
        this.nbtrueAnwer = _nbtrueAnswer;
        this.nbbadAnswer = _nbbadAnswer;
        this.notAnswer = _notAnswer;
        this.timeRemaining = _timeRemaing;
        this.score = _score;
        this.rank = _rank;
    }
}

[Serializable]
public class Players
{
    public List<PlayerData> players;
    public Players(List<PlayerData> _players)
    {
        this.players = _players;
    }
}
