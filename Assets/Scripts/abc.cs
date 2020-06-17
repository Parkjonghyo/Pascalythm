using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class abc : MonoBehaviour
{
    
    [SerializeField] private RectTransform _transform;
    [SerializeField] private VerticalLayoutGroup vlGroup;

    private void Start()
    {
        vlGroup.padding.top = vlGroup.padding.bottom = 330;
    }

    public void autoscroll()
    {
        Vector3 ItemPos = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().anchoredPosition;

        _transform.anchoredPosition = new Vector3(0, -360 - ItemPos.y);
    }

    public void setChildOutLine()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Outline>().enabled = false;
        }
    }
}
