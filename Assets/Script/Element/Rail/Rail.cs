using Megaton.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Megaton.Abstract
{
    /// <summary>
    /// 轨道的抽象类
    /// </summary>
    public abstract class Rail : MonoBehaviour
    {
        public RailEnum Id;
        public List<Note> Notes = new();

        /// <summary>
        /// 轨道发生判定的时候调用
        /// </summary>
        public Action<bool,bool> OnJudge;

        /// <summary>
        /// 按下
        /// </summary>
        public abstract void Tap(InputAction.CallbackContext ctx);

        /// <summary>
        /// 按住
        /// </summary>
        /// <param name="ctx"></param>
        public abstract void Hold(InputAction.CallbackContext ctx);

        /// <summary>
        /// 松开
        /// </summary>
        public abstract void Release(InputAction.CallbackContext ctx);

        /// <summary>
        /// 查询Note状态
        /// </summary>
        /// <param name="note"></param>
        /// <returns>[现态,次态]</returns>
        public abstract (bool current, bool form) QueryNoteState(Note note);

        /// <summary>
        /// 采样输入
        /// </summary>
        public abstract void Sample();

        /// <summary>
        /// 尝试判定，同时只能对一个Note进行判定
        /// </summary>
        public virtual void TryJudge()
        {
            Sample();
            if (Notes == null || Notes.Count == 0) return;
            for(int i = 0;i < Notes.Count; i++)
            {
                var note = Notes[i];
                var gap = MusicPlayer.ExactTime - note.ExactTime;
                Debug.Log(gap);
                if (gap < -note.JudgeStart) break;

                var state = QueryNoteState(note);
                var judge = note.Judge(state.current, state.form);
                OnJudge(judge.success, judge.ifcontinue);
                note.OnJudge(state.current, state.form);

                //判断是否完成判定
                if (judge.success)
                {
                    var result = note.GetResult();
                    Debug.Log($"{note.ExactTime} {String.Format("{0:+0;-#;+0}", (MusicPlayer.ExactTime - note.ExactTime) * 1000).ToString()}ms {note.GetType().Name}:{judge}");
                    ScoreBoard.AddJudge(result);
                    note.OnResult(result);
                    Notes.RemoveAt(i);
                    i--;
                }

                if (!judge.ifcontinue) break;
            }
        }
    }
}