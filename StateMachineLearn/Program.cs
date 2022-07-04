// See https://aka.ms/new-console-template for more information

using StateMachineLearn;

var miner = new Miner(0)
{
    CurrentLocation = ConstDefine.Location.MinerLocationType.None,
    StateMachine = new EnterMineAndDigForNuggetState()
};

GameEntityManger.Instance.TryAddNewEntity(miner);


int loopLimit = 100;
while (loopLimit-- >0)
{
    miner.Update();
}