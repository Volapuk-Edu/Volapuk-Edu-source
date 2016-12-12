using UnityEngine;
using System.Collections;


public enum MenuState
{
    Main,
    Profil,
    Synopsis,
    Score,
    Result,
    Rank,
    Credits,
    Level,
    Dialog,
    Game

}
public class MenuManager : MonoBehaviour {

    MenuState menuGameState;

    public GameObject main, profil, synospsis, score, result, rank, credits, level, dialog, game;

    public MenuState MenuGameState
    {
        get
        {
            return menuGameState;
        }

        set
        {
            menuGameState = value;
        }
    }

    void UpdateUIPanels()
    {


    }

    public void Test()
    {
        switch (menuGameState)
        {
            case MenuState.Main:
                dialog.SetActive(true);
                Dialog d1 = dialog.GetComponent<Dialog>();
        
                d1.message.text = "Etes vous sur de quitter l'application ?";
                d1.Initialize();
                d1.confirmation.SetActive(true);

                d1.okbtnConfirmation.onClick.RemoveAllListeners();
                d1.okbtnConfirmation.onClick.AddListener(() => {
                    Application.Quit();
                  
                });
                d1.noBtn.onClick.RemoveAllListeners();
                d1.noBtn.onClick.AddListener(() =>
                {
                    menuGameState = MenuState.Main;
                    dialog.SetActive(false);
                    
                });
                break;

            case MenuState.Profil:
                profil.SetActive(false);
                main.SetActive(true);
                menuGameState = MenuState.Main;
                break;

            case MenuState.Synopsis:
                synospsis.SetActive(false);
                profil.SetActive(true);

                break;
            case MenuState.Score:
                score.SetActive(false);
                main.SetActive(true);
                menuGameState = MenuState.Main;
                break;
            case MenuState.Result:
                result.SetActive(false);
                rank.SetActive(true);
                break;
            case MenuState.Rank:
                result.SetActive(false);
                level.SetActive(true);
                break;
            case MenuState.Credits:
                credits.SetActive(false);
                main.SetActive(true);
                menuGameState = MenuState.Main;
                break;
            case MenuState.Level:
                level.SetActive(false);
                profil.SetActive(true);

                break;
            case MenuState.Dialog:
                dialog.SetActive(false);
                if (profil.activeInHierarchy)
                    menuGameState = MenuState.Profil;
                else if (main.activeInHierarchy)
                    menuGameState = MenuState.Main;
                else
                    menuGameState = MenuState.Game;

                break;
            case MenuState.Game:
                // show a dialog box


                dialog.SetActive(true);
                Dialog d = dialog.GetComponent<Dialog>();
                Time.timeScale = 0;
                d.message.text = "Etes vous sur de revenir au menu principal? La partie sera abandonnée.";
                d.confirmation.SetActive(true);
                d.notification.SetActive(false);
                d.okbtnConfirmation.onClick.RemoveAllListeners();
                d.okbtnConfirmation.onClick.AddListener(() => {

                    game.SetActive(false);
                    Time.timeScale = 1;
                    menuGameState = MenuState.Main;
                    dialog.SetActive(false);
                    main.SetActive(true);
                   
                });
                d.noBtn.onClick.RemoveAllListeners();
                d.noBtn.onClick.AddListener(() =>
                {
                    Time.timeScale = 1;
                    dialog.SetActive(false);

                });
                break;

        }
    }

    public void OnCLickBackIngame()
    {
        dialog.SetActive(true);
        Dialog d = dialog.GetComponent<Dialog>();
        Time.timeScale = 0;
        d.message.text = "Etes vous sur de revenir au menu principal? La partie sera abandonnée.";
        d.confirmation.SetActive(true);
        d.notification.SetActive(false);
        d.okbtnConfirmation.onClick.RemoveAllListeners();
        d.okbtnConfirmation.onClick.AddListener(() => {

            game.SetActive(false);
            Time.timeScale = 1;
            dialog.SetActive(false);
            main.SetActive(true);
            menuGameState = MenuState.Main;
        });
        d.noBtn.onClick.RemoveAllListeners();
        d.noBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            dialog.SetActive(false);

        });
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
           switch (menuGameState)
            {
                case MenuState.Main:
                    dialog.SetActive(true);
                    Dialog d1 = dialog.GetComponent<Dialog>();

                    d1.message.text = "Etes vous sur de quitter l'application ?";
                    d1.Initialize();
                    d1.confirmation.SetActive(true);

                    d1.okbtnConfirmation.onClick.RemoveAllListeners();
                    d1.okbtnConfirmation.onClick.AddListener(() => {
                        Application.Quit();

                    });
                    d1.noBtn.onClick.RemoveAllListeners();
                    d1.noBtn.onClick.AddListener(() =>
                    {
                        menuGameState = MenuState.Main;
                        dialog.SetActive(false);

                    });
                    break;

                case MenuState.Profil:
                    profil.SetActive(false);
                    main.SetActive(true);
                    menuGameState = MenuState.Main;
                    break;

                case MenuState.Synopsis:
                    synospsis.SetActive(false);
                    profil.SetActive(true);
                   
                    break;
                case MenuState.Score:
                    score.SetActive(false);
                    main.SetActive(true);
                    menuGameState = MenuState.Main;
                    break;
                case MenuState.Result:
                    result.SetActive(false);
                    rank.SetActive(true);
                    break;
                case MenuState.Rank:             
                    result.SetActive(false);
                    level.SetActive(true);
                    break;
                case MenuState.Credits:
                    credits.SetActive(false);
                    main.SetActive(true);
                    menuGameState = MenuState.Main;
                    break;
                case MenuState.Level:
                    level.SetActive(false);
                    main.SetActive(true);
                    menuGameState = MenuState.Main;
                    break;
                case MenuState.Dialog:
                    dialog.SetActive(false);
                    if(profil.activeInHierarchy)
                        menuGameState = MenuState.Profil;
                    else if(main.activeInHierarchy)
                        menuGameState = MenuState.Main;
                    else
                        menuGameState = MenuState.Game;

                    break;
                case MenuState.Game:
                    // show a dialog box
                
             
                    dialog.SetActive(true);
                    Dialog d = dialog.GetComponent<Dialog>();
                    Time.timeScale = 0;
                    d.message.text = "Etes vous sur de revenir au menu principal? La partie sera abandonnée.";
                    d.confirmation.SetActive(true);
                    d.notification.SetActive(false);
                    d.okbtnConfirmation.onClick.RemoveAllListeners();
                    d.okbtnConfirmation.onClick.AddListener(() => {
                        
                        game.SetActive(false);
                        Time.timeScale = 1;
                        dialog.SetActive(false);
                        main.SetActive(true);
                        menuGameState = MenuState.Main;
                    });
                    d.noBtn.onClick.RemoveAllListeners();
                    d.noBtn.onClick.AddListener(() =>
                    {
                        Time.timeScale = 1;
                        dialog.SetActive(false);
                      
                    });
                    break;
               
            }
        }
    }
}

