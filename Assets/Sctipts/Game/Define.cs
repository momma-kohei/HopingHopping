namespace Define
{
    public class DiscreteValue
    {
        private int _value = 0; // 代表数値
        private int _min = 0; // 最小値
        private int _max = 1; // 最大値
        private int _diff = 1; // 増減の差分
        
        public int Value { get { return _value; } } // 値を取得するプロパティ

        public DiscreteValue(int min, int max, int diff)
        {
            // 詳細な値はコンストラクタで設定する
            _min = min;
            _max = max;
            _diff = diff;
        }

        public void Increase()
        {
            if (_value + _diff > _max) return; // 最大値を上回らないようにする
            _value += _diff;
        }
        public void Decrease()
        {
            if (_value - _diff < _min) return; // 最小値を下回らないようにする
            _value -= _diff;
        }
    }
}
