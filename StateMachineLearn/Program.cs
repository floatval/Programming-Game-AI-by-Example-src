// See https://aka.ms/new-console-template for more information

using StateMachineLearn;

var miner = new Miner(new InitState(), new InitState());

var wife = new Wife(new WifeInitState(), new WifeInitState())
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
while (loopLimit-- >0)
{
    gameEntities.ForEach(entity =>
    {
        entity.Update();
    });
}
