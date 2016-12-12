using UnityEngine;
using UnityEngine.UI;
using System;

public class ResultUI : MonoBehaviour {

    public Text nbGoodANswer, nbBadANswer, nbNotAnswerd, timeRemaining;

    public RankUI rankUI;

    public GameObject nextLevel;
	
	// Update is called once per frame
	void OnEnable () {
 
        nbGoodANswer.text = ""+DataMgr.instance.nbtrueAnwer;
        nbBadANswer.text = "" + DataMgr.instance.nbbadAnswer;
        nbNotAnswerd.text = "" + DataMgr.instance.notANswer;
        timeRemaining.text = DataMgr.instance.FormatTime(DataMgr.instance.timeRemaining);// DataMgr.instance.timeRemaining +"ms";
       
    }

    
}
