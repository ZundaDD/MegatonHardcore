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
        private List<Note> Notes = new();

        /// <summary>
        /// 轨道发生判定的时候的回调
        /// </summary>
        public Action<bool,bool> OnJudge;

        /// <summary>
        /// 改变状态的回调
        /// </summary>
        public Action<bool> OnStateChange;

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
        /// <returns>(现态,次态)</returns>
        public abstract (bool current, bool form) QueryNoteState(Note note);

        /// <summary>
        /// 采样输入
        /// </summary>
        public abstract void Sample();

        /// <summary>
        /// 加载Note并生成实体
        /// </summary>
        /// <param name="notes">列表</param>
        public void LoadNote(List<Note> notes)
        {
            Notes = notes;
            foreach(var note in notes)
            {
                var go = Instantiate(RailCollection.Ins.notePrefabs[note.GetType().Name]);
                go.GetComponent<NoteSO>().Bind(note);
                go.transform.position += new Vector3(
                    this.transform.position.x,
                    this.transform.position.y,
                    GameCamera.Ins.JudgeLineZ);
            }
            Debug.Log($"Rail: {Id}, Note: {Notes.Count}");

        }

        /// <summary>
        /// 尝试判定，同时只能对一个Note进行判定
        /// </summary>
        public virtual void TryJudge()
        {
            Sample();
            if (Notes == null || Notes.Count == 0) return;

            int idx = 0;
            while(idx < Notes.Count)
            {
                var note = Notes[idx];
                var gap = MusicPlayer.ExactTime - note.ExactTime;
                
                if (gap < -note.JudgeStart) break;

                var state = QueryNoteState(note);
                var judge = note.Judge(state.current, state.form);


                //得到判定结果
                if (judge.success)
                {
                    var result = note.GetResult();
                    
                    this.OnJudge?.Invoke(result != JudgeEnum.MISS, judge.ifcontinue);
                    note.OnJudge?.Invoke(state.current, state.form);
                    note.OnResult?.Invoke(result);
                    
                    Notes.RemoveAt(idx);

                    //Debug.Log($"{note.ExactTime} {String.Format("{0:+0;-#;+0}", (MusicPlayer.ExactTime - note.ExactTime) * 1000).ToString()}ms {note.GetType().Name}:{judge}");
                }
                else
                {
                    this.OnJudge?.Invoke(false, judge.ifcontinue);
                    note.OnJudge?.Invoke(state.current, state.form);
                }

                if (!judge.ifcontinue) break;
                else idx++;
            }
        }
    }
}