using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridManager : MonoBehaviour {

    public RectTransform content;
    public ScrollRect rectScroll;

    public void AddContent( int y , GameObject itemContent)
    {
        if (rectScroll != null)
        {
            rectScroll.enabled = true;
            rectScroll.content = content.GetComponent<RectTransform>();
            rectScroll.verticalScrollbar.value = 0;
        }
   

        GameObject go = Instantiate(itemContent);
        go.transform.SetParent(content.transform, false);
        go.transform.localPosition = Vector3.zero;


        this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, content.GetComponent<RectTransform>().sizeDelta.y + y);
    }

    public void ResetContent()
    {
        if (rectScroll != null)
        {
            rectScroll.enabled = true;
        }
        foreach (Transform child in this.transform)
        {
            Destroy(child.GetComponent<Text>());
            Destroy(child.gameObject);
        }
    }
}
