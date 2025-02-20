
namespace Megaton.Abstract
{
    /// <summary>
    /// 摄像头动效
    /// </summary>
    public abstract class CameraEffect : Command
    {
        /// <summary>
        /// 执行时效果
        /// </summary>
        public abstract void Execute();
    }
}