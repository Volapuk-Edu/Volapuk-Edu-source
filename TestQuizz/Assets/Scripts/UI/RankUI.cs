using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankUI : MonoBehaviour {

    public Sprite[] rankImages;
    public Text[] textsRank;
    public Text rankText;
    public Image displayedImg;
    public ScrollRect text;
    Vector3 origin = new Vector3(-1.55f, -10, 0);
    public int rank;

    void OnEnable()
    {
        FindObjectOfType<MenuManager>().MenuGameState = MenuState.Rank;
        for (int i=0; i< textsRank.Length; i++){
            textsRank[i].gameObject.SetActive(false);
        }
        text.verticalNormalizedPosition = 1;
        SetImage(Mathf.RoundToInt(DataMgr.instance.currentPlayerRank/DataMgr.instance.QuizzAll.allQuestion.Count));

    }
    public void SetImage(int rank)
    {
        this.transform.localPosition = origin;
        this.transform.localScale = Vector3.one;
        displayedImg.sprite = rankImages[rank];
        textsRank[rank].gameObject.SetActive(true);
        text.content = textsRank[rank].rectTransform;
    }

   

}
