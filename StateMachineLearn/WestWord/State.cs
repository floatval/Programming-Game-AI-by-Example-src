namespace StateMachineLearn;

public interface IState<in TOwner> where TOwner : class
{
    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Enter(TOwner owner);

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Execute(TOwner owner);

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Exit(TOwner owner);

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool OnMessage(in Telegram message, TOwner owner);
}

/// <summary>
/// 状态类
/// </summary>
public abstract class State<TOwner> : IState<TOwner> where TOwner : class
{
    #region Implementation of IMinerState

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public virtual void Enter(TOwner owner)
    {
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public virtual void Execute(TOwner owner)
    {
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public virtual void Exit(TOwner owner)
    {
    }

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public virtual bool OnMessage(in Telegram message, TOwner owner)
    {
        return false;
    }

    #endregion
}