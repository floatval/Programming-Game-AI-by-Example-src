using System.Numerics;
using StateMachineLearn;

namespace SteeringBehaviors;

/// <summary>
/// 可移动实例
/// </summary>
public interface IMovingEntity : IBaseGameEntity
{
    #region 数据成员

    /// <summary>
    /// 速度
    /// </summary>
    public Vector2 Velocity { get; set; }

    /// <summary>
    /// 朝向
    /// </summary>
    public Vector2 Heading { get; set; }
    
    /// <summary>
    /// 垂直于朝向的向量
    /// </summary>
    public Vector2 Side { get; set; }

    /// <summary>
    /// 质量
    /// </summary>
    public double Mass { get; set; }
    
    /// <summary>
    /// 最大速度
    /// </summary>
    public double MaxSpeed { get; set; }
    
    /// <summary>
    /// 最大出力
    /// </summary>
    public double MaxForce { get; set; }
    
    /// <summary>
    /// 最大转向速率 - 单位：弧度/秒
    /// </summary>
    public double MaxTurnRate { get; set; }
    
    #endregion
}

/// <summary>
/// 可移动实例
/// </summary>
public class MovingEntity : BaseGameEntity, IMovingEntity
{
    /// <summary>
    /// 防止外部绕过 Builder 创建对象
    /// </summary>
    public MovingEntity(EntityName name) : base(name)
    {
    }
    
    /// <summary>
    /// 速度
    /// </summary>
    public Vector2 Velocity { get; set; }

    /// <summary>
    /// 朝向
    /// </summary>
    public Vector2 Heading { get; set; }
    
    /// <summary>
    /// 垂直于朝向的向量
    /// </summary>
    public Vector2 Side { get; set; }

    /// <summary>
    /// 质量
    /// </summary>
    public double Mass { get; set; }
    
    /// <summary>
    /// 最大速度
    /// </summary>
    public double MaxSpeed { get; set; }
    
    /// <summary>
    /// 最大出力
    /// </summary>
    public double MaxForce { get; set; }
    
    /// <summary>
    /// 最大转向速率 - 单位：弧度/秒
    /// </summary>
    public double MaxTurnRate { get; set; }
}