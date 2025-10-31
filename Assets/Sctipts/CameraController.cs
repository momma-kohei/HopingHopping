using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region// インスペクターで設定
    [Header("追従対称")] public GameObject Player;
    [Header("接地判定")] public OnGround floor;
    [Header("追従曲線")] public AnimationCurve cmCurve;
    #endregion

    #region// プライベート変数
    private float cameraPosX = 0f;
    private float cameraPosY = 0f;
    private float cameraPrePosY = 0f;
    private float cameraMove = 0f;
    private bool isGrounded = false; // 接地判定用
    private bool enterGrounded = false;
    private float groundTime = 0f;
    private float groundHeight = 0f;
    #endregion

    private void Update()
    {
        ScreenEdge();
        groundTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        enterGrounded = floor.enterGround; // 接地入り判定
        isGrounded = floor.onGround; // 接地継続判定

        ScreenFllow();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = new Vector3 (cameraPosX, cameraPosY, this.transform.position.z);
    }
    
    /// <summary>
    /// カメラの画面端に関する処理
    /// </summary>
    private void ScreenEdge()
    {
        if (Player.transform.position.x >= 0)
        {
            cameraPosX = Player.transform.position.x;
        }
        else
        {
            cameraPosX = 0;
        }
    }
    /// <summary>
    /// カメラのプレイヤー追従処理
    /// </summary>
    private void ScreenFllow()
    {
        if (enterGrounded)
        {
            cameraPrePosY = cameraPosY;
            groundHeight = Player.transform.position.y;
            groundTime = 0f;
        }
        if (isGrounded)
        {
            cameraMove = groundHeight - cameraPrePosY;
            if (cameraMove > 0.5f || cameraMove < -0.5f)
            {
                cameraPosY = cameraPrePosY + cameraMove * cmCurve.Evaluate(groundTime);
            }
        }
    }
}
