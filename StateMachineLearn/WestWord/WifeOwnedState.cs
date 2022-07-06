namespace StateMachineLearn;
using EntityManger = GameEntityManger;
using Location = ConstDefine.Location.LocationType;

/// <summary>
/// 妻子的初始化状态
/// </summary>
public class WifeInitState : IState<Wife>
{
    #region Implementation of IState<in Wife>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Enter(Wife owner)
    {
        var entity = EntityManger.Instance.TryGetEntity(owner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 将妻子放置到家里
        owner.CurrentLocation= Location.Home;

        WriteExt.WriteBgWhiteAndFgYellow($"MinerId:{entity.InsId}, 初始化状态：在家里");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Execute(Wife owner)
    {
        owner.FSM.ChangState(DoHouseWork.Instance);
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Exit(Wife owner)
    {
    }

    #endregion

    #region Singleton

    /// <summary>
    /// 数据缓存
    /// </summary>
    private static WifeInitState? m_instance;
    
    /// <summary>
    /// 数据获取接口
    /// </summary>
    public static WifeInitState Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new WifeInitState();
            }
            return m_instance;
        }
    }

    #endregion
}

/// <summary>
/// 去洗手间
/// </summary>
public class VisitBathroom : IState<Wife>
{
    #region Implementation of IState<in Wife>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Enter(Wife owner)
    {
        var entity = EntityManger.Instance.TryGetEntity(owner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 将妻子放置到家里
        owner.CurrentLocation= Location.Home;

        WriteExt.WriteBgWhiteAndFgYellow($"MinerId:{entity.InsId}, 初始化状态：在家里");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Execute(Wife owner)
    {
        owner.CurrentTirednessThreshold++;
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Exit(Wife owner)
    {
        throw new NotImplementedException();
    }

    #endregion
}

/// <summary>
/// 做家务
/// </summary>
public class DoHouseWork : IState<Wife>
{
    #region Implementation of IState<in Wife>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Enter(Wife owner)
    {
        var entity = EntityManger.Instance.TryGetEntity(owner.InsId);
        if(entity == null)
        {
            return;
        }

        if (owner.CurrentLocation != Location.LivingRoom)
        {
            // 1. 将妻子放置到客厅
            owner.CurrentLocation= Location.LivingRoom;
        }

        WriteExt.WriteBgWhiteAndFgYellow($"Wife:{entity.InsId}, 进入客厅：在家里");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Execute(Wife owner)
    {
        var entity = EntityManger.Instance.TryGetEntity(owner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 更新妻子的疲劳值
        owner.CurrentTirednessThreshold++;
        
        // 2. 打印日志
        WriteExt.WriteBgWhiteAndFgBlue($"Wife:{entity.InsId}, 正在做家务");
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Exit(Wife owner)
    {
        var entity = EntityManger.Instance.TryGetEntity(owner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 打印日志
        WriteExt.WriteBgWhiteAndFgRed($"Wife:{entity.InsId}, 退出做家务的状态");
    }

    #endregion

    #region Singleton

    /// <summary>
    /// 数据缓存
    /// </summary>
    private static DoHouseWork? m_instance;
    
    /// <summary>
    /// 数据获取接口
    /// </summary>
    public static DoHouseWork Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new DoHouseWork();
            }
            
            return m_instance;
        }
    }

    #endregion
}

/// <summary>
/// 妻子的全局状态
/// </summary>
public class WifeGlobalState : IState<Wife>
{
    #region Implementation of IState<in Wife>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Enter(Wife owner)
    {
        var entity = EntityManger.Instance.TryGetEntity(owner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 将妻子放置到洗手间
        if (owner.CurrentLocation != Location.BathRoom)
        {
            
            owner.CurrentLocation= Location.BathRoom;
        }

        WriteExt.WriteBgWhiteAndFgYellow($"MinerId:{entity.InsId}, 全局状态");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Execute(Wife owner)
    {
        if (!owner.IsNeedToGoBathroom())
        {
            return;
        }
        
        // 1. 情况妻子的疲劳度
        owner.CurrentTirednessThreshold = 0;
        
        // 2. 回到进入全局状态前的状态
        owner.FSM.RevertToPreviousState();
        
        // 3. 打日志
        WriteExt.WriteBgWhiteAndFgBlue($"entryId{owner.InsId}, 全局状态解决完毕，切换状态到进入全局状态前的状态");
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public void Exit(Wife owner)
    {
        WriteExt.WriteBgWhiteAndFgRed($"entryId{owner.InsId}, 退出全局状态");
    }

    #endregion

    #region Singleton

    /// <summary>
    /// 数据缓存
    /// </summary>
    private static WifeGlobalState? m_instance;
    
    /// <summary>
    /// 数据获取接口
    /// </summary>
    public static WifeGlobalState Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new WifeGlobalState();
            }
            
            return m_instance;
        }
    }

    #endregion
}