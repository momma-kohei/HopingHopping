//using UnityEngine;

//public class SBlind_ctl : MonoBehaviour
//{
//    [SerializeField] private bool _isStart;
//    [SerializeField] private float _delay;

//    private float _time = 0f;
//    private Vector3 _pos;
//    private float _initPosX;
//    private float[] _blindList = { 319f, 607f, 1033f, 1280f};

//    void Start()
//    {
//        _pos = transform.localPosition;
//        _initPosX = _pos.x;
//    }

//    void Update()
//    {
//        if (_time > 1f + _delay)
//        {
//            return;
//        }
//        if (_isStart) _time += Time.deltaTime;
//        if (_time > _delay)
//        {
//            Slide();
//            SetPosition();
//        }
//    }

//    public void SetIsStarted()
//    {
//        _isStart = true;
//    }

//    void SetPosition()
//    {
//        transform.localPosition = _pos;
//    } // henkou

//    void Slide()
//    {
//        if (GameManager.instance == null) return;
//        _pos.x = _initPosX + _blindList[GameManager.instance.StageNum] * (_time - _delay);
//    }
//}
