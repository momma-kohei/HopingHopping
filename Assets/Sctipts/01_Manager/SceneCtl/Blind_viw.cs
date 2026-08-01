using UnityEngine;

public class Blind_viw : MonoBehaviour
{
    private Vector3 _pos;
    private float[] _blindPosList = { 319f, 607f, 1033f, 1280f };

    void Start()
    {
        _pos = transform.position;
        SetBlindPos();
    }

    private void SetBlindPos()
    {
        _pos.x = _blindPosList[GameMgr.Instance.InfoMdl.LatestStageIndex];
        transform.localPosition = _pos;
    }
}
