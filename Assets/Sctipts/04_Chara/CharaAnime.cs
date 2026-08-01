using System;
using UnityEngine;

public class CharaAnime : MonoBehaviour
{
    [SerializeField] private TriggerCtl groundTrigger;
    [SerializeField] private ChargeCtl chargeCtl;
    [SerializeField] private JumpCtl jumpCtl;

    private Animator playerAnime;
    const float SIZE = 1f;

    private void Start()
    {
        if (chargeCtl == null)
        {
            throw new Exception("ChargeControllerが見つかりません．");
        }
        if (jumpCtl == null)
        {
            throw new Exception("JumpControllerが見つかりません．");
        }

        playerAnime = GetComponent<Animator>();
        if (playerAnime == null)
        {
            throw new Exception("Animatorが見つかりません．");
        }
    }

    private void Update()
    {
        PlayerAnime();
    }

    private void PlayerAnime()
    {
        // プレイヤーの向き
        float scaleX = jumpCtl.JumpVector.x > 0 ? SIZE : -SIZE; // 右向きなら正、左向きなら負
        playerAnime.transform.localScale = new Vector3(scaleX, SIZE, SIZE);

        // チャージ判定
        playerAnime.SetBool("charge", chargeCtl.Charging);

        // 接地判定
        playerAnime.SetBool("stand", groundTrigger.IsActive);
    }
}
