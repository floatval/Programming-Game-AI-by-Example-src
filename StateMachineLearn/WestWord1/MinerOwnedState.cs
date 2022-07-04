using System.Diagnostics.CodeAnalysis;

namespace StateMachineLearn;
using Behavior = StateMachineLearn.ConstDefine.MinerBehavior;
using Location = StateMachineLearn.ConstDefine.Location;
using EntityManger = StateMachineLearn.GameEntityManger;

/*
 * 矿工状态机
 * 状态机只负责矿工的状态过渡（将矿工的状态从A设置为B - 只是数据层面的改变，不是逻辑层面的改变 - 是逻辑层的前置依赖）处理
 * 负责状态的切换（进入状态、退出状态）由矿工对象的 Update 处理
 */

public sealed class InitState : MinerState
{
    
}

/// <summary>
/// 进入金矿挖矿的状态
/// </summary>
public sealed class EnterMineAndDigForNuggetState : MinerState
{
    #region Overrides of MinerState

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Enter([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        base.Enter(miner);
        
        // 1. 已经在矿洞里了，不进行额外的处理
        if (miner.CurrentLocation == Location.MinerLocationType.Goldmine)
        {
            return;
        }
        miner.CurrentLocation= Location.MinerLocationType.Goldmine;

        // 2. 不在矿洞里面则进行移动到矿洞的操作
        WriteExt.WriteBgWhiteAndFgYellow($"MinerId:{entity.InsId}, EnterMineAndDigForNuggetState，进入金矿");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Execute([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        base.Execute(miner);
        // 1. 增加身上的金子
        miner.CurrentGoldCarried++;
        
        // 2. 增加疲劳度
        miner.CurrentTirednessThreshold++;
        WriteExt.WriteBgWhiteAndFgBlue($"MinerId:{miner.InsId}, EnterMineAndDigForNuggetState，挖到了金子");
        
        // 3. 判断背包是否满了
        if (miner.IsPocketsFull())
        {
            miner.ChangeState(VisitBankAndDepositGoldState.Instance);
        }
        
        // 4. 判断饥渴度是否达到了上限
        if (miner.IsThirsty())
        {
            miner.ChangeState(QuenchThirstState.Instance);
        }
        
        // 5. 判断疲劳值是否达到上限
        if (miner.IsFatigued())
        {
            miner.ChangeState(GoHomeAndSleepTilRestedState.Instance);
        }
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Exit([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        WriteExt.WriteBgWhiteAndFgRed($"MinerId:{miner.InsId}, EnterMineAndDigForNuggetState，金子满了，退出金矿挖矿");
        base.Exit(miner);
        Console.WriteLine('\n');
    }

    #endregion

    #region Singleton

    private static EnterMineAndDigForNuggetState? m_instance;
    
    /// <summary>
    /// 获取实例
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public static EnterMineAndDigForNuggetState Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new EnterMineAndDigForNuggetState();
            }

            return m_instance;
        }
        
    }

    #endregion
}

/// <summary>
/// 去银行存钱的状态
/// </summary>
public sealed class VisitBankAndDepositGoldState : MinerState
{
    #region Overrides of MinerState

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Enter([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 切换矿工的位置
        if(miner.CurrentLocation == Location.MinerLocationType.Bank)
        {
            return;
        }
        miner.CurrentLocation = Location.MinerLocationType.Bank;
        
        WriteExt.WriteBgWhiteAndFgYellow($"MinerId:{miner.InsId}, VisitBankAndDepositGoldState，进入银行");
        base.Enter(miner);
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Execute([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 存钱
        miner.CurrentGoldCarried = 0;
        
        WriteExt.WriteBgWhiteAndFgBlue($"MinerId:{miner.InsId}, VisitBankAndDepositGoldState，正在银行存钱");
        base.Execute(miner);
        
        // 2. 检查饥渴的状态
        if (miner.IsThirsty())
        {
            miner.ChangeState(QuenchThirstState.Instance);
        }
        
        // 3. 检查疲惫状态
        if (miner.IsFatigued())
        {
            miner.ChangeState(GoHomeAndSleepTilRestedState.Instance);
        }
        
        // 4. 状态正常去挖矿
        miner.ChangeState(EnterMineAndDigForNuggetState.Instance);
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Exit([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        WriteExt.WriteBgWhiteAndFgRed($"MinerId:{miner.InsId}, VisitBankAndDepositGoldState，银行存钱动作完成");
        base.Exit(miner);
        Console.WriteLine('\n');
    }

    #endregion

    #region Singleton

    /// <summary>
    /// 实例缓存
    /// </summary>
    private static VisitBankAndDepositGoldState? m_instance;
    
    /// <summary>
    /// 获取实例
    /// </summary>
    public static VisitBankAndDepositGoldState Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new VisitBankAndDepositGoldState();
            }
            
            return m_instance;
        }
    }

    #endregion
}

/// <summary>
/// 回家休息的状态
/// </summary>
public sealed class GoHomeAndSleepTilRestedState : MinerState
{
    private GoHomeAndSleepTilRestedState()
    {
       m_instance = this; 
    }
    
    #region Overrides of MinerState

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Enter([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 切换矿工位置
        if(miner.CurrentLocation == Location.MinerLocationType.Home)
        {
            return;
        }
        miner.CurrentLocation = Location.MinerLocationType.Home;
        
        WriteExt.WriteBgWhiteAndFgYellow($"MinerId:{miner.InsId}, GoHomeAndSleepTilRestedState，回到家里");
        base.Enter(miner);
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Execute([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        base.Execute(miner);
        miner.CurrentTirednessThreshold--;
        WriteExt.WriteBgWhiteAndFgBlue($"MinerId:{miner.InsId}, GoHomeAndSleepTilRestedState，在家里休息中");
        
        // 1. 检查饥渴状态
        if(miner.IsThirsty())
        {
            miner.ChangeState(QuenchThirstState.Instance);
        }
        
        // 2. 状态正常 - 去挖矿
        if (miner.IsNotFatigued())
        {
            miner.ChangeState(EnterMineAndDigForNuggetState.Instance);
        }
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Exit([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        WriteExt.WriteBgWhiteAndFgRed($"MinerId:{miner.InsId}, GoHomeAndSleepTilRestedState，休息好了");
        base.Exit(miner);
        Console.WriteLine('\n');
    }

    #endregion

    #region Singleton

    /// <summary>
    /// 对象缓存
    /// </summary>
    private static GoHomeAndSleepTilRestedState? m_instance;
    
    /// <summary>
    /// 实例获取
    /// </summary>
    public static GoHomeAndSleepTilRestedState Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GoHomeAndSleepTilRestedState();
            }
            
            return m_instance;
        }
    }

    #endregion
}


/// <summary>
/// 解决口渴的状态
/// </summary>
public class QuenchThirstState : MinerState
{
    private QuenchThirstState()
    {
        m_instance = this;
    }
    
    #region Overrides of MinerState

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Enter([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 切换位置
        if(miner.CurrentLocation == Location.MinerLocationType.Saloon)
        {
            return;
        }
        miner.CurrentLocation = Location.MinerLocationType.Saloon;
        
        WriteExt.WriteBgWhiteAndFgYellow($"MinerId:{miner.InsId}, QuenchThirstState，进入酒吧");
        base.Enter(miner);
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Execute([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        base.Execute(miner);
        WriteExt.WriteBgWhiteAndFgBlue($"MinerId:{miner.InsId}, QuenchThirstState，正在解决口渴");
        miner.CurrentThirstLevel--;

        // 1. 解决完口渴，去挖矿
        if (miner.IsNotThirsty())
        {
            miner.ChangeState(EnterMineAndDigForNuggetState.Instance);
        }
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="miner"></param>
    public override void Exit([NotNull]IMiner miner)
    {
        var entity = EntityManger.Instance.TryGetEntity(miner.InsId);
        if(entity == null)
        {
            return;
        }
        
        WriteExt.WriteBgWhiteAndFgRed($"MinerId:{miner.InsId}, QuenchThirstState，解决了口渴");
        base.Exit(miner);
        Console.WriteLine('\n');
    }

    #endregion
    
    #region Singleton

    /// <summary>
    /// 单例
    /// </summary>
    private static QuenchThirstState? m_instance;
    
    /// <summary>
    /// 获取单例接口
    /// </summary>
    public static QuenchThirstState Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new QuenchThirstState();
            }
            
            return m_instance;
        }
    }
    
    #endregion
}