using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HistoryUI : MonoBehaviour {

    public Text path;
    void OnEnable()
    {
        path.text = Application.persistentDataPath;
    }

}
