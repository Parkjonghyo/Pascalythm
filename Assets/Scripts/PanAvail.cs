using UnityEngine;
using UnityEngine.UI;

public class PanAvail : MonoBehaviour
{
    public Sprite Equal;
    public Sprite Normal;
    public Sprite Miss;
    [SerializeField] private Image img;
    [SerializeField] private Animation anim;
    [SerializeField] private elAvail ela;
    

    public enum Timing
    {
        Equal,
        Normal_Early,
        Normal_Late,
        Miss
    }

    public void CheckPan(Timing timing)
    {
        if (img.color != Color.white) { img.color = Color.white; }

        if (timing == Timing.Equal)
        {
            img.sprite = Equal;
            ela.EqualEl();
        }
        else if (timing == Timing.Normal_Early) { 
            img.sprite = Normal;
            ela.CheckEl(elAvail.el.Early);
        }
        else if (timing == Timing.Normal_Late)
        {
            img.sprite = Normal;
            ela.CheckEl(elAvail.el.Late);
        }
        else { img.sprite = Miss; }
        
        anim.Play();
    }
    
}
