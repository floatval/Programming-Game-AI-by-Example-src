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
    public int InsId { get; set; }
}

/// <summary>
/// 基础游戏条目抽象类
/// </summary>
public class BaseGameEntity : IBaseGameEntity
{
    public class BaseGameEntityBuilder
    {
        private readonly BaseGameEntity m_baseGameEntity = new BaseGameEntity();

        /// <summary>
        /// 构造游戏条目的实例 Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">id 无效时抛出异常</exception>
        public BaseGameEntityBuilder BuildEntityId(int id)
        {
            // 1. 检查传入的 Id 的有效性，如果 Id 无效，则抛出异常
            if (id < NextValidId)
            {
                throw new ArgumentException("Invalid entity id " + id);
            }
            
            // 2. 构造 Id
            m_baseGameEntity.InsId = id;
            
            // 3. 更新有效 id
            checked
            {
                ++NextValidId;
            }
            
            return this;
        }
    }
    
    /// <summary>
    /// 防止外部绕过 Builder 创建对象
    /// </summary>
    public BaseGameEntity()
    {
    }

    /// <summary>
    /// 当前对象的实例 Id
    /// </summary>
    public int InsId { get; set; }

    /// <summary>
    /// 生成对象序列中，下一个对象的有效 Id
    /// 该 Id 自动在对象创建的时候自增 1
    /// 如果在创建对象的时候传入的 InsId 初始值是否 大于等于 _nextValidId，如果不满足条件则抛出异常
    /// </summary>
    /// <exception cref="Exception"></exception>
    protected static int NextValidId { get; set; }

    #region Implementation of IBaseGameEntity

    /// <summary>
    /// 刷新条目当前的状态 - 每帧(每次循环)调用
    /// </summary>
    public virtual void Update()
    {
    }

    #endregion
}