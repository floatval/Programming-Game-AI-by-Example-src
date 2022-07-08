namespace StateMachineLearn;

/// <summary>
/// 矿工接口
/// </summary>
public interface IMiner : IBaseGameEntity
{
    #region 数据属性

    /// <summary>
    /// 当前携带的金币数量
    /// </summary>
    public int CurrentGoldCarried { get; set; }
    
    /// <summary>
    /// 当前饥渴度
    /// </summary>
    public int CurrentThirstLevel { get; set; }
    
    /// <summary>
    /// 当前疲劳度
    /// </summary>
    public int CurrentTirednessThreshold { get; set; }

    /// <summary>
    /// 当前位置
    /// </summary>
    public ConstDefine.Location.LocationType CurrentLocation { get; set; }

    /// <summary>
    /// 状态机
    /// </summary>
    public StateMachine<Miner> FSM { get; }

    #endregion

    #region 方法属性

    /// <summary>
    /// 背包是否满了
    /// </summary>
    /// <returns></returns>
    public bool IsPocketsFull();
    
    /// <summary>
    /// 是否疲劳了
    /// </summary>
    /// <returns></returns>
    public bool IsFatigued();

    /// <summary>
    /// 是否不疲劳了
    /// </summary>
    /// <returns></returns>
    public bool IsNotFatigued();
    
    /// <summary>
    /// 是否渴了
    /// </summary>
    /// <returns></returns>
    public bool IsThirsty();
    
    /// <summary>
    /// 是否不渴了
    /// </summary>
    /// <returns></returns>
    public bool IsNotThirsty();
    
    #endregion
}

/// <summary>
/// 矿工类
/// </summary>
public class Miner : BaseGameEntity, IMiner
{
    public Miner(IState<Miner> initState, IState<Miner> preState, EntityName name) : base(name)
    {
        FSM = new StateMachine<Miner>(this, initState, preState);
    }

    #region 覆写

    /// <summary>
    /// 处理信息
    /// </summary>
    /// <param name="msg"></param>
    public override void HandleMessage(in Telegram msg)
    {
        FSM.HandleMessage(in msg);
    }

    /// <summary>
    /// 刷新条目当前的状态 - 每帧(每次循环)调用
    /// </summary>
    public override void Update()
    {
        // 1. 调用父类更新
        base.Update();
        
        // 2. 运行当前状态的处理 - 内部可能会更新矿工的状态
        FSM.Update();
    }

    #endregion
    
    #region 数据成员

    /// <summary>
    /// 当前携带的金币数量
    /// </summary>
    public int CurrentGoldCarried { get; set; }
    
    /// <summary>
    /// 当前饥渴度
    /// </summary>
    public int CurrentThirstLevel { get; set; }
    
    /// <summary>
    /// 当前疲劳度
    /// </summary>
    public int CurrentTirednessThreshold { get; set; }

    /// <summary>
    /// 当前位置
    /// </summary>
    public ConstDefine.Location.LocationType CurrentLocation { get; set; } = ConstDefine.Location.LocationType.None;

    /// <summary>
    /// 状态机
    /// </summary>
    public StateMachine<Miner> FSM { get; private set; }

    /// <summary>
    /// 背包是否满了
    /// </summary>
    /// <returns></returns>
    public bool IsPocketsFull()
    {
        return CurrentGoldCarried >= ConstDefine.MinerBehavior.NuggetsMaxVal;
    }

    /// <summary>
    /// 是否疲劳了
    /// </summary>
    /// <returns></returns>
    public bool IsFatigued()
    {
        return CurrentTirednessThreshold>= ConstDefine.MinerBehavior.TirednessThresholdMaxVal;
    }

    /// <summary>
    /// 是否不疲劳了
    /// </summary>
    /// <returns></returns>
    public bool IsNotFatigued()
    {
        return CurrentTirednessThreshold == 0;
    }

    /// <summary>
    /// 是否渴了
    /// </summary>
    /// <returns></returns>
    public bool IsThirsty()
    {
        return CurrentThirstLevel >= ConstDefine.MinerBehavior.ThirstLevelMaxVal;
    }

    /// <summary>
    /// 是否不渴了
    /// </summary>
    /// <returns></returns>
    public bool IsNotThirsty()
    {
        return CurrentThirstLevel == 0;
    }

    #endregion

}