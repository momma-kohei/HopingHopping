using System;

namespace Status
{
    /// <summary>
    /// ステージの全貌とステージ間の進行を扱うクラス
    /// </summary>
    public class Stages
    {
        #region// privateな処理
        private string[] _stageScenes = { "Stage1", "Stage2", "Stage3", "Stage4"}; // ステージシーンのリスト
        private int _currentStageIndex = 0; // ステージのインデックス
        private int _maxStageIndex = 2;
        private int[] _stageScores;
        #endregion

        #region// publicな情報
        public Stages() // コンストラクタ
        {
            _stageScores = new int[_stageScenes.Length]; // 各ステージのスコアを定義
        }
        public int MaxStageIndex { get { return _maxStageIndex; } set { _maxStageIndex = value; } }
        public int StageScore { get { return _stageScores[_currentStageIndex]; } } // UI表示用
        /// <summary>
        /// 指定したステージに進む処理
        /// </summary>
        /// <param name="num">入場するステージ</param>
        public void SetStage(int num)
        {
            if (num < 0 && num > _stageScenes.Length)
            {
                throw new NullReferenceException("ステージインデックスが不正です．");
            }
            _currentStageIndex = num;
        }
        /// <summary>
        /// 現在のステージインデックスからシーンの名前を取得するメソッド
        /// </summary>
        /// <returns>ステージのシーン名</returns>
        public string GetStage()
        {
            if (_stageScenes[_currentStageIndex] == null)
            {
                throw new NullReferenceException("ステージリストのインデックスが不正です．");
            }
            return _stageScenes[_currentStageIndex];
        }
        /// <summary>
        /// 現在のステージスコアにポイントを加算する処理
        /// </summary>
        /// <param name="point">アイテムのポイント</param>
        public void GetItem(int point)
        {
            if (_stageScores == null)
            {
                throw new NullReferenceException("スコアリストが不正です．");
            }
            _stageScores[_currentStageIndex] += point;
        }
        #endregion
    }

    /// <summary>
    /// プレイヤーの残機を扱うクラス
    /// </summary>
    public class Lives
    {
        #region// privateな処理
        private int _life; // 現在の残機
        #endregion
        #region// publicな情報
        public event Action GameoverEvent; // ゲームオーバー時に発火されるイベント
        public Lives() // コンストラクタ
        {
            _life = 3; // 残機の初期化
        }
        public int Life { get { return _life; } } // UI表示用
        public bool Living { get { return _life > 0; } } // 生存判定
        public void OneUp()
        {
            _life++;
        }
        public void Die()
        {
            _life--;
            if (!Living)
            {
                // ゲームオーバー
                GameoverEvent.Invoke();
            }
        }
        #endregion
    }
}
