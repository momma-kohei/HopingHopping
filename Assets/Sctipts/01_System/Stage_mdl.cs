namespace Stage_mdl
{
    public class  LifeClass
    {
        const int _initLife = 5;
        private int _life;
        public int Get() { return _life; }

        public LifeClass()
        {
            Reset();
        }

        public void Add(int num)
        {
            _life = num;
        }

        public void Decliment()
        {
            _life--;
        }

        public void Reset()
        {
            _life = _initLife;
        }
    }

    public class ScoreClass
    {
        private int _score = 0;
        public int Get() { return _score; }

        public ScoreClass()
        {
            Reset();
        }

        public void Add(int num)
        {
            _score += num;
        }

        public void Reset()
        {
            _score = 0;
        }
    }

    public class StageClass
    {
        const int _maxStage = 5;

        private int _stage = 1;
        private int[] _point = new int[_maxStage];

        public int GetStage() { return _stage; }
        public int GetCheckPoint() { return _point[_stage]; }

        public StageClass() {}

        void CheckStageInput(int num)
        {
            if (num < 1 || num > _maxStage)
            {
                throw new System.ArgumentOutOfRangeException("ステージ番号は1から" + _maxStage + "の範囲で指定してください．");
            }
        }

        /// <summary>
        /// 任意のステージをセットする
        /// </summary>
        /// <param name="num">ステージ番号</param>
        public void SetStage(int num)
        {
            CheckStageInput(num);
            _stage = num;
        }

        /// <summary>
        /// 次のステージへ進む
        /// </summary>
        public void NextStage()
        {
            if (_stage < _maxStage)
            {
                _stage++;
            }
        }

        /// <summary>
        /// 次のチェックポイントを記録する
        /// </summary>
        public void NextPoint()
        {
            _point[_stage]++;
        }

        public void ResetPoint(int num)
        {
            CheckStageInput(num);
            _point[num] = 0;
        }


    }
}
