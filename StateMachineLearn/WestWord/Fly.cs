namespace StateMachineLearn;
using Location = ConstDefine.Location.LocationType;

/// <summary>
/// 苍蝇接口
/// </summary>
public interface IFly : IBaseGameEntity
{
    /// <summary>
    /// 当前位置
    /// </summary>
    public ConstDefine.Location.LocationType CurrentLocation { get; set; }

    /// <summary>
    /// 状态机
    /// </summary>
    public StateMachine<Fly> FSM { get; }
}

/// <summary>
/// 苍蝇对象
/// </summary>
public class Fly : BaseGameEntity, IFly
{
    /// <summary>
    /// 防止外部绕过 Builder 创建对象
    /// </summary>
    public Fly(EntityName name, IState<Fly> currentState, IState<Fly> preState) : base(name)
    {
        FSM = new StateMachine<Fly>(this, currentState, preState);
    }

    #region Implementation of IFly

    /// <summary>
    /// 当前位置
    /// </summary>
    public Location CurrentLocation { get; set; }

    #region Overrides of BaseGameEntity

    /// <summary>
    /// 处理信息
    /// </summary>
    /// <param name="msg"></param>
    public override void HandleMessage(in Telegram msg)
    {
        FSM.HandleMessage(msg);
    }

    #endregion

    #region Implementation of IBaseGameEntity

    public override void Update()
    {
        FSM.Update();
    }

    #endregion

    /// <summary>
    /// 状态机
    /// </summary>
    public StateMachine<Fly> FSM { get; }

    #endregion
}