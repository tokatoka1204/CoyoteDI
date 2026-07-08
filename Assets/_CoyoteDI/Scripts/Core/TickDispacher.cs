using System.Collections.Generic;

namespace CoyoteDI.Core
{
    /// <summary>
    /// 常時実行される処理をまとめて呼び出します。
    /// </summary>
    public sealed class TickDispacher
    {
        /// <summary>
        /// 常に更新され続けるオブジェクトの一覧
        /// </summary>
        private readonly List<ITickable> _tickables = new();

        /// <summary>
        /// 次の更新から処理を行うオブジェクトの一覧
        /// </summary>
        private readonly List<ITickable> _toAdd = new();

        /// <summary>
        /// 次の更新で処理をやめるオブジェクトの一覧
        /// </summary>
        private readonly List<ITickable> _toRemove = new();

        /// <summary>
        /// 一定間隔で常に呼び出されるメソッドです。
        /// </summary>
        /// <param name="deltaTime">前回の更新からの経過時間</param>
        public void Tick(float deltaTime)
        {
            Refresh();
            foreach (var t in _tickables)
            {
                t.Tick(deltaTime);
            }
        }

        /// <summary>
        /// 新しく常時実行処理を行いたいオブジェクトを登録します。
        /// </summary>
        /// <param name="t">登録したいオブジェクト</param>
        public void Add(ITickable t)
        {
            if (_toAdd.Contains(t) || _tickables.Contains(t)) return;
            _toAdd.Add(t);
        }

        /// <summary>
        /// 常時実行処理を止めたいオブジェクトを登録します。
        /// </summary>
        /// <param name="t">登録したいオブジェクト</param>
        public void Remove(ITickable t)
        {
            _toRemove.Add(t);
        }

        /// <summary>
        /// _tickablesの中身を更新します。
        /// </summary>
        private void Refresh()
        {
            foreach (var t in _toAdd)
            {
                _tickables.Add(t);
            }
            _toAdd.Clear();

            foreach (var t in _toRemove)
            {
                _tickables.Remove(t);
            }
            _toRemove.Clear();
        }
    }
}
