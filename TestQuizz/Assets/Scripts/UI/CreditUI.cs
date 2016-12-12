using UnityEngine;
using System.Collections;

public class CreditUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        FindObjectOfType<MenuManager>().MenuGameState = MenuState.Credits;
    }
}
