// See https://aka.ms/new-console-template for more information

using StateMachineLearn;

var miner = new Miner(new InitState(), new InitState(), EntityName.EntityMinerBob);

var wife = new Wife(new WifeInitState(), new WifeInitState(), EntityName.EntityElsa)
{
    FSM =
    {
        GlobalState = WifeGlobalState.Instance
    }
};

var fly = new Fly(EntityName.EntityFly, FlyGlobalState.Instance, FlyGlobalState.Instance)
{
    FSM = { GlobalState = FlyGlobalState.Instance }
};

GameEntityManger.Instance.TryAddNewEntity(miner);
GameEntityManger.Instance.TryAddNewEntity(wife);
GameEntityManger.Instance.TryAddNewEntity(fly);

int loopLimit = 100;
var message = MessageDispatcher.Instance;
while (loopLimit-- >0)
{
    Thread.Sleep(100);
    
    // 1. 更新所有实体状态
    GameEntityManger.Instance.UpdateAllEntity();
    
    // 2. 派发消息
    message.DispatchDelayMessage();
}
