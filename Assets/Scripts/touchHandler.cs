using UnityEngine;

public class touchHandler : MonoBehaviour
{
    readonly RaycastHit2D[] _hit2dArray = new RaycastHit2D[10];
    [SerializeField] private Camera _camera;
    [SerializeField] private btn_all _btnAll;
    [SerializeField] private Trace_touch Touch_Red, Touch_Blue;
    public bool ignore = false;
    private int parsedName;
    private RaycastHit2D hit, Chit;

    void Update()
    {
        if (ignore) return;
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                var id = Input.GetTouch(i).fingerId;
                Vector2 pos = Input.GetTouch(i).position;    // 터치한 위치
                Vector2 touchPos = _camera.ScreenToWorldPoint(pos); // 스크린에 카메라를 기준으로 쏜다
                hit = Physics2D.Raycast(touchPos, Vector2.zero);
                TouchInput(i);
                Trace_TouchInput(i);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Input.mousePosition;    // 터치한 위치
            Vector2 touchPos = _camera.ScreenToWorldPoint(pos); // 스크린에 카메라를 기준으로 쏜다
            Chit = Physics2D.Raycast(touchPos, Vector2.zero);
            ClickInput();
        }else if (Input.GetMouseButton(0))
        {
            Vector2 pos = Input.mousePosition;    // 터치한 위치
            Vector2 touchPos = _camera.ScreenToWorldPoint(pos); // 스크린에 카메라를 기준으로 쏜다
            Chit = Physics2D.Raycast(touchPos, Vector2.zero);
            Trace_ClickInput();
        }
    }

    private void Trace_TouchInput(int touchNumber)
    {
        if (hit)    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
        {
            if (hit.transform.name.Equals("Trace_touch"))
            {
                switch (Input.GetTouch(touchNumber).phase)
                {
                    case TouchPhase.Began:
                        Touch_Blue.touched_Trace();
                        break;
                    case TouchPhase.Stationary :
                        Touch_Blue.touched_Trace();
                        break;
                    case TouchPhase.Moved :
                        Touch_Blue.touched_Trace();
                        break;
                }
            } else if(hit.transform.name.Equals("Trace_touch 2"))
            {
                switch (Input.GetTouch(touchNumber).phase)
                {
                    case TouchPhase.Began:
                        Touch_Red.touched_Trace();
                        break;
                    case TouchPhase.Stationary :
                        Touch_Red.touched_Trace();
                        break;
                    case TouchPhase.Moved :
                        Touch_Red.touched_Trace();
                        break;
                }
            }
        }
    }
    
    private void TouchInput(int touchNumber)
    {
        
        if (hit)    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
        {
            if (!int.TryParse(hit.transform.name, out parsedName)) return;
            var touchedButton = _btnAll.getButtonById(parsedName-1);
            
            switch (Input.GetTouch(touchNumber).phase)
            {
                case TouchPhase.Began:
                    /*_hit2dArray[id] = hit; // 해당 손이 처음 충돌한 객체를 hit2dArray에 저장*/
                    touchedButton.MobileTouch(0);
                    break;
            }
        }
    }
    
    private void ClickInput()
    {
        if (Chit)    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
        {
            if (!int.TryParse(Chit.transform.name, out parsedName)) return;
            var touchedButton = _btnAll.getButtonById(parsedName-1);
            touchedButton.MobileTouch(0);
        }
    }
    
    private void Trace_ClickInput()
    {
        if (Chit)    // 레이저를 끝까지 쏴블자. 충돌 한넘이 있으면 return true다.
        {
            if (Chit.transform.name.Equals("Trace_touch"))
            {
                Touch_Blue.touched_Trace();
            }else if(Chit.transform.name.Equals("Trace_touch 2"))
            {
                Touch_Red.touched_Trace();
            }
        }
    }
    
}