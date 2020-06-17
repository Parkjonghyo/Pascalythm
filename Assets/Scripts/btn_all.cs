using UnityEngine;

public class btn_all : MonoBehaviour
{
    private button[] btn = new button[16];

    private void Awake()
    {
        for (int i = 0; i < 16; i++)
        {
            btn[i] = gameObject.transform.GetChild(i).GetComponent<button>();   
        }
    }

    public button getButtonById(int Id)
    {
        return btn[Id];
    }

    public void buttonAnimationSpeed(float speed)
    {
        for (int i = 0; i < 16; i++)
        {
            btn[i].AnimationSpeed(speed);
        }
    }
}
