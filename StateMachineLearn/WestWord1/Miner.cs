namespace StateMachineLearn;

/// <summary>
/// 矿工接口
/// </summary>
public interface IMiner : IBaseGameEntity
{
    
}

/// <summary>
/// 矿工类
/// </summary>
public class Miner : BaseGameEntity, IMiner
{
}