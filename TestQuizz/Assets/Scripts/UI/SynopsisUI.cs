using UnityEngine;
using UnityEngine.UI;

public class SynopsisUI : MonoBehaviour {
   
    public ScrollRect scrollRect;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
     
        scrollRect.verticalNormalizedPosition = 1;
        FindObjectOfType<MenuManager>().MenuGameState = MenuState.Synopsis;
    }

  
}
