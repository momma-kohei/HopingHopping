using System;
using UnityEngine;

public class LaunchCtl : MonoBehaviour
{
    private Rigidbody2D _rb2d;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        if (_rb2d == null) throw new NullReferenceException("Rigidbody2Dが見つかりません．");
    }

    /// <summary>
    /// オブジェクトに初速度を与えるメソッド
    /// </summary>
    public void Launch(Vector2 vector)
    {
        _rb2d.linearVelocity = vector;
        SoundMgr.instance.Play(SEkey.Launch);
    }
}
