using System.Collections.Concurrent;
using System.Diagnostics;

namespace StateMachineLearn;

public class GameEntityManger
{
    private GameEntityManger()
    {
        
    }
    
    /// <summary>
    /// 添加新的实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool TryAddNewEntity(IBaseGameEntity entity)
    {
        Debug.Assert(entity!= null, "entity is null");
        
        return GameEntities.TryAdd(entity.InsId, entity);
    }
    
    /// <summary>
    /// 通过实体id获取实体
    /// </summary>
    /// <param name="insId"></param>
    /// <returns></returns>
    public IBaseGameEntity? TryGetEntity(int insId)
    {
        return GameEntities.TryGetValue(insId, out var entity) ? entity : null;
    }
    
    /// <summary>
    /// 通过实例名字找实例
    /// </summary>
    /// <param name="entityName"></param>
    /// <returns></returns>
    public IBaseGameEntity? TryGetEntityByEntityName(EntityName entityName)
    {
        return GameEntities.Values.FirstOrDefault(entity => entity.Name == entityName);
    }

    /// <summary>
    /// 尝试通过实体名字移除实体
    /// </summary>
    /// <param name="insId"></param>
    /// <returns></returns>
    public bool TryRemoveEntityByEntityInsId(int insId)
    {
        // 1. 尝试在实体管理器中找到实体
        return GameEntities.TryRemove(insId, out _);
    }

    /// <summary>
    /// 更新所有的实例
    /// </summary>
    public void UpdateAllEntity()
    {
        foreach (var entity in GameEntities.Values)
        {
            entity.Update();
        }
    }
    
    #region Singleton

    /// <summary>
    /// 对象缓存
    /// </summary>
    private static GameEntityManger? m_instance;
    
    /// <summary>
    /// 获取实例
    /// </summary>
    public static GameEntityManger Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameEntityManger();
            }
            return m_instance;
        }
    }
    
    #endregion

    #region 数据成员

    private ConcurrentDictionary<int, IBaseGameEntity> GameEntities { get; set; } =
        new ConcurrentDictionary<int, IBaseGameEntity>();

    #endregion
}