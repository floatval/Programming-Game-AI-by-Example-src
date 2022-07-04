using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace StateMachineLearn;
using Location = StateMachineLearn.ConstDefine.Location.MinerLocationType;
using Status = StateMachineLearn.ConstDefine.MinerState.MinerStateType;

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
    public Location CurrentLocation { get; set; }
    
    /// <summary>
    /// 矿工当前的状态
    /// </summary>
    public  Status CurrentStatus { get; set; }
    
    /// <summary>
    /// 状态机
    /// </summary>
    public IMinerState StateMachine { get; set; }
    
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
    
    
    /// <summary>
    /// 变更矿工状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool ChangeState(IMinerState state);
    
    #endregion
}

/// <summary>
/// 矿工类
/// </summary>
public class Miner : BaseGameEntity, IMiner
{
    public Miner(int id)
    {
    }
    
    #region Overrides of BaseGameEntity

    /// <summary>
    /// 刷新条目当前的状态 - 每帧(每次循环)调用
    /// </summary>
    public override void Update()
    {
        // 1. 调用父类更新
        base.Update();
        
        // 2. 运行当前状态的处理 - 内部可能会更新矿工的状态
        StateMachine.Execute(this);
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
    public Location CurrentLocation { get; set; } = Location.None;
    
    /// <summary>
    /// 矿工当前的状态
    /// </summary>
    public  Status CurrentStatus { get; set; } = Status.None;
    
    /// <summary>
    /// 状态机
    /// </summary>
    public IMinerState StateMachine { get; set; } = new InitState();

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

    /// <summary>
    /// 变更矿工状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool ChangeState([NotNull]IMinerState state)
    {
        Debug.Assert(state != null, "state is null");

        // 1. 改变状态前和改变状态后是同一个状态
        if (ReferenceEquals(StateMachine, state))
        {
            return false;
        }
        
        // 2. 退出当前状态
        StateMachine.Exit(this);
        
        // 3. 切换为新的状态
        StateMachine = state;
        // 3.1 进入新的状态
        state.Enter(this);
        
        return true;
    }

    #endregion

}