using UnityEngine;
using UnityEngine.UI;

public class elAvail : MonoBehaviour
{
    public Sprite Early;
    public Sprite Late;
    [SerializeField] private Image img;
    [SerializeField] private Animation anim;
    private float timer;
    private el? el_now;

    public enum el
    {
        Early,
        Late
    }
    
    private void Awake()
    {
        img.color = Color.clear;
        timer = 0;
    }

    private void Update()
    {
        if (timer > 0) { timer -= Time.deltaTime; return; }
        img.color = Color.clear;
    }

    public void CheckEl(el El)
    {
        if (el_now == El) return;
        if (img.color != Color.white) { img.color = Color.white; }
        
        el_now = El;
        
        if (El == el.Early) { img.sprite = Early; }
        else { img.sprite = Late; }

        timer = 2;
        anim.Play();
    }

    public void EqualEl()
    {
        el_now = null;

        timer = 0;
    }
    
}