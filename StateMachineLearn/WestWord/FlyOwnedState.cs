namespace StateMachineLearn;
using Location = ConstDefine.Location.LocationType;

/// <summary>
/// 苍蝇的全局状态 - 待机状态，接收到有可骚扰对象的时候，按照一定概率去执行骚扰动作
/// </summary>
public sealed class FlyGlobalState : State<Fly>
{
    private FlyGlobalState(){}
    
    #region Overrides of State<Fly>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Enter(Fly owner)
    {
        // 1. 如果当前不在酒馆，则更新位置到酒馆
        if (owner.CurrentLocation != Location.Saloon)
        {
            owner.CurrentLocation = Location.Saloon;
        }
        
        WriteExt.WriteBgWhiteAndFgYellow($"" +
                                          $"{owner.Name} 进入苍蝇的全局状态 - 待机状态");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Execute(Fly owner)
    {
        if (owner.CurrentLocation != Location.Saloon)
        {
            owner.CurrentLocation = Location.Saloon;
        }
        WriteExt.WriteBgWhiteAndFgBlue($"" +
                                          $"{owner.Name} 执行苍蝇的全局状态 - 待机状态");
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Exit(Fly owner)
    {
        WriteExt.WriteBgWhiteAndFgRed($"" +
                                          $"{owner.Name} 跳出苍蝇的全局状态 - 待机状态");
    }

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override bool OnMessage(in Telegram message, Fly owner)
    {
        // 1. 如果不是通知在酒馆的状态，则返回 false
        if (message.MessageType != ConstDefine.MessageType.FlyImSaloon)
        {
            return false;
        }
        
        // 2. 如果是通知在酒馆的状态 - 则随机按照一定概率去执行骚扰动作 - todo
        owner.FSM.ChangState(HarassmentState.Instance);
        return true;
    }

    #endregion

    #region Singleton

    /// <summary>
    /// 对象缓存
    /// </summary>
    private static FlyGlobalState? m_state;
    
    /// <summary>
    /// 实例获取接口
    /// </summary>
    public static FlyGlobalState Instance
    {
        get
        {
            if (m_state == null)
            {
                m_state = new FlyGlobalState();
            }
            return m_state;
        }
    }

    #endregion
}

/// <summary>
/// 苍蝇骚扰人的状态
/// </summary>
public sealed class HarassmentState: State<Fly>
{
    private HarassmentState()
    {
        
    }
    
    #region Singleton

    /// <summary>
    /// 实例缓存
    /// </summary>
    private static HarassmentState? m_state;

    /// <summary>
    /// 实例获取方法
    /// </summary>
    public static HarassmentState Instance
    {
        get
        {
            if (m_state == null)
            {
                m_state = new HarassmentState();
            }
            return m_state;
       }
    }

    #region Overrides of State<Fly>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Enter(Fly owner)
    {
        WriteExt.WriteBgWhiteAndFgYellow($"" +
                                          $"{owner.Name} 进入苍蝇的骚扰人状态");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Execute(Fly owner)
    {
        WriteExt.WriteBgWhiteAndFgBlue($"" +
                                          $"{owner.Name} 运行苍蝇的骚扰人状态");
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Exit(Fly owner)
    {
        WriteExt.WriteBgWhiteAndFgRed($"" +
                                          $"{owner.Name} 退出苍蝇的骚扰人状态");
    }

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override bool OnMessage(in Telegram message, Fly owner)
    {
        // 1. 如果不是通知在酒馆的状态，则返回 false
        if(owner.CurrentLocation != Location.Saloon)
        {
            return false;
        }
        
        // 2. 发信息给矿工
        MessageDispatcher.Instance.DispatchMessage(EntityName.EntityMinerBob, EntityName.EntityFly,
            ConstDefine.MessageType.MinerImFlyAttackU, 0, null);
        
        WriteExt.WriteBgWhiteAndFgBlue($"{owner.Name} 苍蝇正在骚扰矿工");

        return true;
    }

    #endregion

    #endregion
}

/// <summary>
/// 被攻击的状态
/// </summary>
public sealed class AttackedState : State<Fly>
{
    private AttackedState()
    {
        
    }

    #region Overrides of State<Fly>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Enter(Fly owner)
    {
        // 1. 打日志
        WriteExt.WriteBgWhiteAndFgYellow($"{owner.Name} 苍蝇进入被攻击状态");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Execute(Fly owner)
    {
        // 1. 打日志
        WriteExt.WriteBgWhiteAndFgBlue($"{owner.Name} 苍蝇运行被攻击状态");
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Exit(Fly owner)
    {
        // 1. 打日志
        WriteExt.WriteBgWhiteAndFgRed($"{owner.Name} 苍蝇跳出被攻击状态");
    }

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override bool OnMessage(in Telegram message, Fly owner)
    {
        if(owner.CurrentLocation!=Location.Saloon)
        {
            return false;
        }
        
        // 1. 被击中了
        if (DateTime.Now.Ticks % 2 == 0)
        { 
            // 1.1 切换状态到被击中状态
            // 1.1 发信息通知矿工，不用继续攻击了，苍蝇投降了      
            MessageDispatcher.Instance.DispatchMessage(EntityName.EntityMinerBob, EntityName.EntityFly,
                ConstDefine.MessageType.FlySurrender, 0, null);
            WriteExt.WriteBgWhiteAndFgRed($"{owner.Name} 苍蝇被击中了，投降了");
            return true;
        }
        
        // 2. 未被击中
        if(DateTime.Now.Ticks % 2 != 0)
        { 
            // 2.1 切换状态到躲避状态
            owner.FSM.ChangState(DodgeState.Instance);
            return true;
        }

        return true;
    }

    #endregion

    #region Singleton

    /// <summary>
    /// 实例缓存
    /// </summary>
    private static AttackedState? m_state;

    /// <summary>
    /// 实例获取方法
    /// </summary>
    public static AttackedState Instance
    {
        get
        {
            if (m_state == null)
            {
                m_state = new AttackedState();
            }
            return m_state;
       }
    }

    #endregion
}

/// <summary>
/// 被击中的状态
/// </summary>
public sealed class HitBySomethingState : State<Fly>
{
    private HitBySomethingState()
    {
        
    }
    
    #region Singleton

    /// <summary>
    /// 实例缓存
    /// </summary>
    private static HitBySomethingState? m_state;

    /// <summary>
    /// 实例获取方法
    /// </summary>
    public static HitBySomethingState Instance
    {
        get
        {
            if (m_state == null)
            {
                m_state = new HitBySomethingState();
            }
            return m_state;
       }
    }

    #region Overrides of State<Fly>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Enter(Fly owner)
    {
        // 1. 发消息给矿工，苍蝇被击中了
        MessageDispatcher.Instance.DispatchMessage(EntityName.EntityMinerBob, EntityName.EntityFly,
            ConstDefine.MessageType.FlySurrender, 0, null);
        
        // 2. 打日志
        WriteExt.WriteBgWhiteAndFgRed($"{owner.Name} 苍蝇被击中了，投降了");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Execute(Fly owner)
    {
        // 1. 更改为待机状态
        owner.FSM.ChangState(FlyGlobalState.Instance);
        
        // 2. 打日志
        WriteExt.WriteBgWhiteAndFgRed($"{owner.Name} 苍蝇待机，投降了");
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Exit(Fly owner)
    {
        // 2. 打日志
        WriteExt.WriteBgWhiteAndFgRed($"{owner.Name} 苍蝇待机，投降了");
    }

    #endregion

    #endregion
}

/// <summary>
/// 躲避的状态
/// </summary>
public sealed class DodgeState: State<Fly>
{
    private DodgeState()
    {
    }

    #region Overrides of State<Fly>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Enter(Fly owner)
    {
        // 1. 打日志
        WriteExt.WriteBgWhiteAndFgYellow($"{owner.Name} 苍蝇进入躲避状态");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Execute(Fly owner)
    {
        // 1. 回到全局待机状态
        owner.FSM.ChangState(FlyGlobalState.Instance);
        
        // 2. 打日志
        WriteExt.WriteBgWhiteAndFgBlue($"{owner.Name} 苍蝇运行躲避状态");
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Exit(Fly owner)
    {
        // 1. 打日志
        WriteExt.WriteBgWhiteAndFgRed($"{owner.Name} 苍蝇跳出躲避状态");
    }

    #endregion

    #region Singleton

    /// <summary>
    /// 实例缓存
    /// </summary>
    private static DodgeState? m_state;

    /// <summary>
    /// 实例获取方法
    /// </summary>
    public static DodgeState Instance
    {
        get
        {
            if (m_state == null)
            {
                m_state = new DodgeState();
            }
            return m_state;
       }
    }

    #endregion
}
