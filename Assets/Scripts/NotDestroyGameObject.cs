using UnityEngine;

public class NotDestroyGameObject : MonoBehaviour
{
    public string tag;
    void Awake()
    {
        if (GameObject.FindGameObjectWithTag(tag)){ gameObject.SetActive(false); return;}
        DontDestroyOnLoad(gameObject);
        gameObject.tag = tag;
    }
}
