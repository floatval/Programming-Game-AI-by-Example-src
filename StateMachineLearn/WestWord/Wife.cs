namespace StateMachineLearn;
using Location = ConstDefine.Location.LocationType;

/// <summary>
/// 妻子的接口
/// </summary>
public interface IWife : IBaseGameEntity
{
    #region 数据成员

    /// <summary>
    /// 当前位置
    /// </summary>
    public Location  CurrentLocation { get; set; }
    
    /// <summary>
    /// 当前疲劳度 - 和矿工使用同样的疲劳度计算方式 - 疲劳满了，去上厕所
    /// </summary>
    public int CurrentTirednessThreshold { get; set; }
    
    /// <summary>
    /// 状态机
    /// </summary>
    public StateMachine<Wife> FSM { get; set; }
    
    #endregion

    #region 方法成员

    /// <summary>
    /// 是否需要去洗手间
    /// </summary>
    /// <returns></returns>
    public bool IsNeedToGoBathroom();

    #endregion
}

/// <summary>
/// 妻子的实现
/// </summary>
public class Wife : BaseGameEntity, IWife
{
    public Wife(IState<Wife> initState, IState<Wife> globalState)
    {
        FSM = new StateMachine<Wife>(this,initState, globalState);
    }
    
    #region Overrides of BaseGameEntity

    /// <summary>
    /// 刷新条目当前的状态 - 每帧(每次循环)调用
    /// </summary>
    public override void Update()
    {
        base.Update();
        
        FSM.Update();
    }

    #endregion

    #region Implementation of IWife

    /// <summary>
    /// 当前位置
    /// </summary>
    public Location CurrentLocation { get; set; }

    /// <summary>
    /// 当前疲劳度 - 和矿工使用同样的疲劳度计算方式 - 疲劳满了，去上厕所
    /// </summary>
    public int CurrentTirednessThreshold { get; set; }

    /// <summary>
    /// 状态机
    /// </summary>
    public StateMachine<Wife> FSM { get; set; }

    /// <summary>
    /// 是否需要去洗手间
    /// </summary>
    /// <returns></returns>
    public bool IsNeedToGoBathroom()
    {
        return CurrentTirednessThreshold >= ConstDefine.MinerBehavior.TirednessThresholdMaxVal;
    }

    #endregion

    #region 对象方法


    #endregion
}