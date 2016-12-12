using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour {




    public Button btn;
    public GameObject go;

    public Scrollbar verticalScroll;
    public DataMgr data;
    public GameManager game;
    public GameObject profil;
    public GameObject rankResult;
    public GameObject scores;
    public GameObject synopsis;
    public bool isEndGame;

    public Text debug;

    void OnEnable()
    {
        btn.onClick.AddListener(()=>
        {
            verticalScroll.value -= 0.25f;

            if (verticalScroll.value < 0.1)
            {
                verticalScroll.value = 0;
                go.SetActive(true);
                go.GetComponent<Button>().onClick.AddListener(() => {
                   
                    if (isEndGame)
                    {
                        scores.SetActive(true);
                        rankResult.SetActive(false);
                    }
                    else
                    {

                            data.Level = 0;
                            game.gameObject.SetActive(true);
                            GameManager.instance.StartGame();
                            synopsis.SetActive(false);
                     
                    }
                  
                    profil.gameObject.SetActive(false);
                    go.SetActive(false);
                });
          
            }
        });
    }

}
