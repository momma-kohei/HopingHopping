using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    #region// インスペクターでいじる
    [Header("最大ジャンプ力")] public float jumpForce = 800f; // 基礎ジャンプ力
    [Header("最小角度")] public float minAngle = 30f; // 最低角度
    [Header("落下速度上限")] public float maxVelocity = 10f; // 速度上限
    [Header("チャージ曲線")] public AnimationCurve chargeCurve;

    [Header("接地判定")] public OnGround floor;
    #endregion

    #region// インスペクターから隠す
    private Rigidbody2D rb2D = null;
    private Animator anime = null;
    private bool isGrounded = false; // 接地判定用
    private bool enterGrounded = false;
    private float chargeTime = 0f;
    private float chargePower = 0f;
    private float mouseAngle = 0f; // マウス角度
    #endregion

    void Start()
    {
        // インスタンス処理
        rb2D = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    void Update()
    {        
        mouseAngle = GetMouseAngle();
        AddJumpForce();
    }

    private void FixedUpdate()
    {
        enterGrounded = floor.enterGround; // 接地入り判定
        isGrounded = floor.onGround; // 接地継続判定

        SetVelocity();
        SetAnimation();
    }

    /// <summary>
    /// プレイヤーに対するマウスの方向の角度を計算し，その値を返す
    /// </summary>
    /// <returns>マウス方向の角度（°）</returns>
    private float GetMouseAngle()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // マウスのワールド座標を取得
        mousePos.z = 0; // z座標は0

        Vector3 playerPos = new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z); // 足元への位置ベクトル
        Vector3 dt = mousePos - playerPos; // マウスの相対座標
        float rad = Mathf.Atan2(dt.y, dt.x);
        float mouseAngle = rad * Mathf.Rad2Deg; // マウス角度（[0,180]）
        if (mouseAngle < minAngle && mouseAngle > -90)
        {
            mouseAngle = minAngle;
        }
        else if (mouseAngle < -90 || mouseAngle > 180 - minAngle)
        {
            mouseAngle = 180 - minAngle;
        }

        return mouseAngle;
    }
    /// <summary>
    /// マウス角度を元にジャンプの力を加える
    /// </summary>
    private void AddJumpForce()
    {
        bool msDown = Input.GetMouseButtonDown(0);
        bool msPush = Input.GetMouseButton(0);
        bool msUp = Input.GetMouseButtonUp(0);
        if (isGrounded)
        {
            if (msPush)
            {
                chargeTime += Time.deltaTime;
                chargePower = jumpForce * chargeCurve.Evaluate(chargeTime);
            }
            if (msUp)
            {
                rb2D.AddForce(Vector2.up * Mathf.Sin(mouseAngle * Mathf.Deg2Rad) * chargePower, ForceMode2D.Impulse); // 上方向
                rb2D.AddForce(Vector2.right * Mathf.Cos(mouseAngle * Mathf.Deg2Rad) * chargePower, ForceMode2D.Impulse); // 右方向
                chargeTime = 0f; // 初期化
                chargePower = 0f; // 初期化
            }
        }
    }
    /// <summary>
    /// 速度の特殊処理を行う
    /// </summary>
    private void SetVelocity()
    {
        if (enterGrounded)
        {
            rb2D.linearVelocity = Vector2.zero; // 接地後の速度0
        }
        if (rb2D.linearVelocity.y < -maxVelocity)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, -maxVelocity); // 落下速度の制限
        }
    }
    /// <summary>
    /// アニメーションを設定
    /// </summary>
    private void SetAnimation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // マウスのワールド座標を取得
        mousePos.z = 0;
        bool msPush = Input.GetMouseButton(0);
        float playerSize = 1;

        // プレイヤー向きの変更
        if (mousePos.x - transform.position.x >= 0)
        {
            transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        }
        else
        {
            transform.localScale = new Vector3(-playerSize, playerSize, playerSize);
        }

        // 溜めモーション
        if (msPush)
        {
            anime.SetBool("charge", true);
        }
        else
        {
            anime.SetBool("charge", false);
        }

        // ジャンプアニメーション
        if (isGrounded)
        {
            anime.SetBool("stand", true);
        }
        else
        {
            anime.SetBool("stand", false);
        }

        return;
    }


}
