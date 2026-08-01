using UnityEngine;
using System;
using System.Collections;

public class AutoJump : MonoBehaviour
{
    private JumpCtl jumpCtl;

    const float SHORT_CHARGE_TIME = 0.2f; // 短時間チャージ
    const float LONG_CHARGE_TIME = 1.5f; //長時間チャージ
    private Vector2 JUMP_VECTOR_RIGHT = new Vector2(1/Mathf.Sqrt(2), 1/Mathf.Sqrt(2));
    private Vector2 JUMP_VECTOR_LEFT = new Vector2(-1/Mathf.Sqrt(2), 1/Mathf.Sqrt(2));

    private void Start()
    {
        jumpCtl = GetComponent<JumpCtl>();
        if (jumpCtl == null)
        {
            throw new Exception("JumpControllerがアタッチされていません．");
        }
        StartCoroutine(AutoJumpLoop());
    }

    IEnumerator AutoJumpLoop()
    {
        yield return new WaitForSeconds(2f); // 初期待機
        while (true)
        {
            yield return StartCoroutine(Jump(LONG_CHARGE_TIME, true));
            yield return StartCoroutine(Jump(SHORT_CHARGE_TIME, true));
            yield return StartCoroutine(Jump(SHORT_CHARGE_TIME, false));
            yield return StartCoroutine(Jump(LONG_CHARGE_TIME, false));
            yield return StartCoroutine(Jump(SHORT_CHARGE_TIME, false));
            yield return StartCoroutine(Jump(SHORT_CHARGE_TIME, true));
        }
    }

    IEnumerator Jump(float time, bool isRight)
    {
        jumpCtl.JumpVector = isRight ? JUMP_VECTOR_RIGHT : JUMP_VECTOR_LEFT; // ベクトル設定
        jumpCtl.Charge(); // チャージ開始
        yield return new WaitForSeconds(time); // 設定時間待つ
        jumpCtl.Jump(); // ジャンプ
        yield return new WaitForSeconds(time + 0.5f); // ジャンプ後待機
    }
}
