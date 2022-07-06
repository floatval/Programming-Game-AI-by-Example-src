﻿using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

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