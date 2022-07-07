// See https://aka.ms/new-console-template for more information

using StateMachineLearn;

var miner = new Miner(new InitState(), new InitState(), EntityName.EntityMinerBob);

var wife = new Wife(new WifeInitState(), new WifeInitState(), EntityName.EntityElsa)
{
    FSM =
    {
        GlobalState = new WifeGlobalState()
    }
};

GameEntityManger.Instance.TryAddNewEntity(miner);
GameEntityManger.Instance.TryAddNewEntity(wife);

var gameEntities = new List<BaseGameEntity>
{
    miner,
    wife
};


int loopLimit = 100;
var message = MessageDispatcher.Instance;
while (loopLimit-- >0)
{
    Thread.Sleep(100);
    gameEntities.ForEach(entity =>
    {
        entity.Update();
    });
    
    message.DispatchDelayMessage();
}
