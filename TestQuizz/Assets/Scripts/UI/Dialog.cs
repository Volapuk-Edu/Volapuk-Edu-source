using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Dialog : MonoBehaviour {

    public GameObject notification;
    public GameObject confirmation;

    public  Button okbtnConfirmation;
    public Button noBtn;

    public Button okBtnNotification;


    public Text message;

    public void Initialize()
    {
        notification.SetActive(false);
        confirmation.SetActive(false);     
    }


   void OnEnable()
    {
        FindObjectOfType<MenuManager>().MenuGameState = MenuState.Dialog;
    }

}
