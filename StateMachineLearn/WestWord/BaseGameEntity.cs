namespace StateMachineLearn;

/// <summary>
/// 基础游戏条目接口
/// </summary>
public interface IBaseGameEntity
{
    public void Update();
    
    /// <summary>
    /// 实例 Id
    /// </summary>
    public int InsId { get; }
    
    /// <summary>
    /// 实例名字
    /// </summary>
    public EntityName Name { get; }

    /// <summary>
    /// 处理信息
    /// </summary>
    /// <param name="msg"></param>
    public void HandleMessage(in Telegram msg);
}

/// <summary>
/// 基础游戏条目抽象类
/// </summary>
public class BaseGameEntity : IBaseGameEntity
{
    /// <summary>
    /// 防止外部绕过 Builder 创建对象
    /// </summary>
    protected BaseGameEntity(EntityName name)
    {
        InsId = NextValidId;
        ++NextValidId;
        Name = name;
    }

    /// <summary>
    /// 当前对象的实例 Id
    /// </summary>
    public int InsId { get; private set; }

    /// <summary>
    /// 实例名字
    /// </summary>
    public EntityName Name { get; private set; }

    /// <summary>
    /// 处理信息
    /// </summary>
    /// <param name="msg"></param>
    public virtual void HandleMessage(in Telegram msg)
    {
    }

    /// <summary>
    /// 生成对象序列中，下一个对象的有效 Id
    /// 该 Id 自动在对象创建的时候自增 1
    /// </summary>
    /// <exception cref="Exception"></exception>
    private static int NextValidId { get; set; }

    #region Implementation of IBaseGameEntity

    /// <summary>
    /// 刷新条目当前的状态 - 每帧(每次循环)调用
    /// </summary>
    public virtual void Update()
    {
    }

    #endregion
}