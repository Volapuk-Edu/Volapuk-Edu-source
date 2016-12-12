



public class PlayerAction  {
    string action;
    float time;
    string question;
    public string actionText;

    public void AddAction (string action, float time, string question)
    {
        actionText += question + " " + action + " en " + DataMgr.instance.FormatTime(time)  +"\n";
    }

}

