using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class ProfilUI : MonoBehaviour {

    public DataMgr data;
    List<PlayerData> allPlayers = new List<PlayerData>();
    public Dropdown downDrop;
    public InputField nameUI;
    public Dialog  errorCreationDialog;
    List<string> allplayerName = new List<string>();

    public Text txy;
    public GameObject playbtn, synopsis;
    public Text currentPlayer;

    void OnEnable()
    {
        //.AddOptions()
        SetDownDropPlayer();
        FindObjectOfType<MenuManager>().MenuGameState = MenuState.Profil;
    }

  
    public void SetDownDropPlayer()
    {
        allPlayers.Clear();
        allPlayers.AddRange(data.AllPlayers.players);
        if (allPlayers.Count == 0)
        {
            //downDrop.gameObject.SetActive(false);
            //playbtn.gameObject.SetActive(false);
            downDrop.ClearOptions();
            playbtn.GetComponent<Button>().interactable = false;

            return;
        }
        downDrop.gameObject.SetActive(true);
        playbtn.gameObject.SetActive(true);
        playbtn.GetComponent<Button>().interactable = true;
        allplayerName.Clear();
        for (int i = 0; i < allPlayers.Count; i++)
        {
            allplayerName.Add(allPlayers[i].name);
        }

        downDrop.ClearOptions();

        downDrop.AddOptions(allplayerName);
    }

    public void AddPlayer()
    {
     
        
        if (allplayerName.Contains(nameUI.text) || nameUI.text=="")
        {
            errorCreationDialog.gameObject.SetActive(true);
    
            errorCreationDialog.confirmation.SetActive(false);
            errorCreationDialog.notification.SetActive(true);
            errorCreationDialog.message.text = "Veuillez entrez un nom ou ce profil existe déja!";
            errorCreationDialog.okBtnNotification.onClick.RemoveAllListeners();
            errorCreationDialog.okBtnNotification.onClick.AddListener(() => {
                errorCreationDialog.gameObject.SetActive(false);
                nameUI.text = "";
            });
        }
        else
        {
         
            allPlayers.Add(new PlayerData(nameUI.text, new List<PlayerLevelData>()));
            data.AllPlayers.players.Add(new PlayerData(nameUI.text, new List<PlayerLevelData>()));
            Players p = new Players(allPlayers);
            txy.text = JsonUtility.ToJson(data.AllPlayers);

            #if UNITY_EDITOR
            data.SaveData(JsonUtility.ToJson(p), "Assets/Resources/player.json");
            #else
                data.SaveData(JsonUtility.ToJson(p), Application.persistentDataPath + "/player.json");
           
            #endif
            for (int i = 0; i < allPlayers.Count; i++)
            {
                if (allPlayers[i].name == nameUI.text)
                {
                    data.CurrentPlayer = new PlayerData(allPlayers[i].name,allPlayers[i].playerLevelData);
                    break;
                }
            }
            SetDownDropPlayer();
            nameUI.text = "";
            synopsis.SetActive(true);
         

        }
    }

 
    public void SetCurrentPlayer()
    {
        for (int i =0; i<allPlayers.Count; i++)
        {
            if(allPlayers[i].name == currentPlayer.text)
            {
                data.CurrentPlayer = new PlayerData(allPlayers[i].name, allPlayers[i].playerLevelData);
                break;
            }
        }
    }


}
