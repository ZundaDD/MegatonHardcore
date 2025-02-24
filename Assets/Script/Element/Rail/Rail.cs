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
        
        private float maxStart;
        private float maxEnd;

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
        public abstract bool[] QueryNoteState(Note note);

        /// <summary>
        /// 采样输入
        /// </summary>
        public abstract void Sample();

        /// <summary>
        /// 计算最大值
        /// </summary>
        public void CalculateMax()
        {
            maxStart = Notes.Max<Note>(x => x.JudgeStart);
            maxEnd = Notes.Max<Note>(x => x.JudgeEnd);
        }

        
        public virtual void TryJudge()
        {
            Sample();
            if (Notes == null) return;
            for(int i = 0;i < Notes.Count; i++)
            {
                var note = Notes[i];
                var gap = MusicPlayer.ExactTime - note.ExactTime;
                
                //过早检查
                if (gap < -maxStart) break;
                if (gap < -note.JudgeStart) continue;

                var sta = QueryNoteState(note);

                
                //判断是否完成判定
                if (note.Judge(sta[0], sta[1]))
                {
                    var judge = note.GetResult();
                    Debug.Log($"{note.ExactTime} {String.Format("{0:+0;-#;+0}",(MusicPlayer.ExactTime-note.ExactTime) * 1000).ToString()}ms {note.GetType().Name}:{judge}");
                    ScoreBoard.AddJudge(judge);
                    note.OnResult(judge);
                    Notes.RemoveAt(i);
                    i--;
                }
                
                note.OnJudge(sta[0], sta[1]);
            }
        }
    }
}