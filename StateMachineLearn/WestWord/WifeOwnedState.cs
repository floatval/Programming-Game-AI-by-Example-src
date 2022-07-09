namespace StateMachineLearn;
using EntityManger = GameEntityManger;
using Location = ConstDefine.Location.LocationType;

/// <summary>
/// 妻子的初始化状态
/// </summary>
public class WifeInitState : State<Wife>
{
    #region Implementation of IState<in Wife>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Enter(Wife owner)
    {
        var entity = EntityManger.Instance.TryGetEntity(owner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 将妻子放置到家里
        owner.CurrentLocation= Location.Home;

        WriteExt.WriteBgWhiteAndFgYellow($"WifeId:{entity.InsId}, 初始化状态：在家里");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Execute(Wife owner)
    {
        owner.FSM.ChangeState(DoHouseWork.Instance);
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Exit(Wife owner)
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
public class VisitBathroom : State<Wife>
{
    #region Implementation of IState<in Wife>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Enter(Wife owner)
    {
        var entity = EntityManger.Instance.TryGetEntity(owner.InsId);
        if(entity == null)
        {
            return;
        }
        
        // 1. 将妻子放置到家里
        owner.CurrentLocation= Location.Home;

        WriteExt.WriteBgWhiteAndFgYellow($"WifeId:{entity.InsId}, 初始化状态：在家里");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Execute(Wife owner)
    {
        owner.CurrentTirednessThreshold++;
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Exit(Wife owner)
    {
        throw new NotImplementedException();
    }

    #endregion
}

/// <summary>
/// 做家务
/// </summary>
public class DoHouseWork :State<Wife>
{
    #region Implementation of IState<in Wife>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Enter(Wife owner)
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
    public override void Execute(Wife owner)
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
    public override void Exit(Wife owner)
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
public class WifeGlobalState :State<Wife>
{
    #region Implementation of IState<in Wife>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Enter(Wife owner)
    {
        var entity = EntityManger.Instance.TryGetEntity(owner.InsId);
        if(entity == null)
        {
            return;
        }
        
        WriteExt.WriteBgWhiteAndFgYellow($"wifeId:{entity.InsId}, 全局状态");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Execute(Wife owner)
    {
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Exit(Wife owner)
    {
        WriteExt.WriteBgWhiteAndFgRed($"WifeId:{owner.InsId}, 退出全局状态");
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

    #region Overrides of State<Wife>

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override bool OnMessage(in Telegram message, Wife owner)
    {
        // 1. 不是矿工回家的消息
        if (message.MessageType != ConstDefine.MessageType.HiHoneyImHome)
        {
            return false;
        }

        WriteExt.WriteBgWhiteAndFgYellow($"WifeId:{owner.InsId}, 收到消息，开始进入全局状态");
        
        // 2. 更改状态到烹饪
        owner.FSM.ChangeState(CookStew.Instance);
        
        return true;

    }

    #endregion

    #endregion
}

/// <summary>
/// 煮肉的状态
/// </summary>
public class CookStew : State<Wife>
{
    private CookStew()
    {
        m_stew = this;
    }

    #region Overrides of State<Wife>

    /// <summary>
    /// 进入状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Enter(Wife owner)
    {
        // 1. 检查当前状态
        if(owner.CurrentLocation!= Location.Kitchen)
        {
            owner.CurrentLocation = Location.Kitchen;
        }

        // 2. 检查是否处于烹饪状态
        if (owner.IsInCooking)
        {
            return;
        }
        
        // 3. 设置当前处于烹饪状态
        owner.IsInCooking = true;
        
        WriteExt.WriteBgWhiteAndFgYellow($"wifeId:{owner.InsId}, 进入煮肉的状态");
    }

    /// <summary>
    ///  运行状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Execute(Wife owner)
    {
        // 1. 开始烹饪，并在烹饪好的时候提醒自己
        MessageDispatcher.Instance.DispatchMessage(owner.Name, EntityName.EntityElsa,
            ConstDefine.MessageType.StewReady, 0.01, null);
        
        WriteExt.WriteBgWhiteAndFgBlue($"wifeId:{owner.InsId}, 正在煮肉");    
    }

    /// <summary>
    /// 退出状态处理流程
    /// </summary>
    /// <param name="owner"></param>
    public override void Exit(Wife owner)
    {
        WriteExt.WriteBgWhiteAndFgRed($"wifeId:{owner.InsId}, 退出煮肉的状态");
    }

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override bool OnMessage(in Telegram message, Wife owner)
    {
        switch (message.MessageType)
        {
            // 1. 煮肉完成通知矿工
            case ConstDefine.MessageType.StewReady:
            {
                WriteExt.WriteBgWhiteAndFgRed($"wifeId:{owner.InsId}, 收到煮肉完成的消息");
                MessageDispatcher.Instance.DispatchMessage(EntityName.EntityMinerBob, owner.Name,
                    ConstDefine.MessageType.StewReady, 0, null);
                owner.IsInCooking = false;
                owner.FSM.ChangeState(DoHouseWork.Instance); 
                return true;
            }
            case ConstDefine.MessageType.HiHoneyImHome:
            default:
                return false;
        }
    }

    #endregion


    #region Singleton

    /// <summary>
    /// 对象引用
    /// </summary>
    private static CookStew? m_stew;

    /// <summary>
    /// 缓存
    /// </summary>
    public static CookStew Instance
    {
        get
        {
            if (m_stew == null)
            {
                m_stew = new CookStew();
            }
            
            return m_stew;
        }
    }

    #endregion
}