using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //「MainBall」ゲームオブジェクト
    [SerializeField] GameObject mainBall = null;
    // 打つ力
    [SerializeField] float power = 0.1f;
    // 方向表示用オブジェクトのトランスフォーム
    [SerializeField] Transform arrow = null;
    // ボールリスト
    [SerializeField] List<ColorBall> ballList = new List<ColorBall>();

    // マウス位置保管用
    Vector3 mousePosition = new Vector3();
    //「MainBall」のリジッドボディ
    Rigidbody mainRigid = null;
    // リセット時のためにメインボール初期位置を保管
    Vector3 mainBallDefaultPosition = new Vector3();

    void Start()
    {
        //「MainBall」のリジッドボディを取得
        mainRigid = mainBall.GetComponent<Rigidbody>();
        mainBallDefaultPosition = mainBall.transform.localPosition;
        arrow.gameObject.SetActive(false);
    }

    void Update()
    {
        // メインボールがアクティブなとき
        if(mainBall.activeSelf == true)
        {
            // マウスクリック開始時.
            if(Input.GetMouseButtonDown(0) == true)
            {
                // 開始位置を保管
                mousePosition = Input.mousePosition;
                // 方向線を表示
                arrow.gameObject.SetActive(true);
                Debug.Log("クリック開始");
            }

            // マウスクリック中
            if(Input.GetMouseButton(0) == true)
            {
                // 現在の位置を随時保管
                Vector3 position = Input.mousePosition;

                // 角度を算出
                Vector3 def = mousePosition - position;
                float rad = Mathf.Atan2(def.x, def.y);
                float angle = rad * Mathf.Rad2Deg;
                Vector3 rot = new Vector3(0, angle, 0);
                Quaternion qua = Quaternion.Euler(rot);

                // 方向船の位置角度を設定
                arrow.localRotation = qua;
                arrow.transform.position = mainBall.transform.position;

            }

            // マウスクリック終了時
            if(Input.GetMouseButtonUp(0) == true)
            {
                // 終了時の位置を保管
                Vector3 upPosition = Input.mousePosition;

                // 開始位置と終了位置のベクトル計算から打ち出す方向を算出
                Vector3 def = mousePosition - upPosition;
                Vector3 add = new Vector3(def.x, 0, def.y);

                // メインボールに力を加える
                mainRigid.AddForce(add * power);

                // 方向線を非表示に
                arrow.gameObject.SetActive(false);

                Debug.Log("クリック終了");
            }
        }
    }

    //--------------------------------------------------------------------------------
    // <summary>
    // リセットボタンクリックコールバック
    // </summary>
    //--------------------------------------------------------------------------------
    public void OnResetButtonClicked()
    {
        mainBall.SetActive(true);
        // メインボールの速度を強制的にゼロに
        mainRigid.velocity = Vector3.zero;
        // メインボールの回転速度を強制的にゼロに
        mainRigid.angularVelocity = Vector3.zero;
        // メインボールを初期位置に戻す
        mainBall.transform.localPosition = mainBallDefaultPosition;

        foreach( ColorBall ball in ballList)
        {
            // カラーボールのリセット
            ball.Reset();
        }
    }
}
