using UnityEngine;

public class camSizeCon : MonoBehaviour
{
    float i_Scale;
    public Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        i_Scale = Screen.width / Screen.height;
        if (i_Scale.Equals(16 / 10))
            mainCam.orthographicSize = 7.666666f;
        //5.925926f
        else if (i_Scale == 4 / 3)
            mainCam.orthographicSize = 9f;
        else
            mainCam.orthographicSize = 7;
    }
}
