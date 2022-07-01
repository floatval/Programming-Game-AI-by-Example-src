namespace StateMachineLearn;

public interface IMinerState
{
    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    void Enter(Miner miner);
    
    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    void Execute(Miner miner);
    
    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    void Exit(Miner miner);
}

/// <summary>
/// 状态类
/// </summary>
public abstract class MinerState: IMinerState
{
    #region Implementation of IMinerState

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public virtual void Enter(Miner miner)
    {
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public virtual void Execute(Miner miner)
    {
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public virtual void Exit(Miner miner)
    {
    }

    #endregion
}