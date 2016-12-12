using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelElt : MonoBehaviour {

    public Image[] stars;
    public GameObject lockedImg;
    public Text levelNb;
    public Button btn;
    bool IsLocked;

    public void SetStarsColor(int nb, bool isLocked)
    {
        lockedImg.SetActive(isLocked);
        IsLocked = isLocked;
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = new Color32(255, 255, 255, 255);
        }
        for (int i=0; i < nb; i++)
        {
            stars[i].color = new Color32(223,245,21,255);
        }
       
    }


    void OnEnable()
    {
       

        if (!IsLocked)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => {
                FindObjectOfType<LevelUI>().LoadCorrectLevel(int.Parse(levelNb.text) - 1);
            });
        }
    }
}
