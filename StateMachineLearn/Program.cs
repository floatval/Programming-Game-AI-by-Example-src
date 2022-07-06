// See https://aka.ms/new-console-template for more information

using StateMachineLearn;

var miner = new Miner(new InitState(), new InitState());

GameEntityManger.Instance.TryAddNewEntity(miner);


int loopLimit = 100;
while (loopLimit-- >0)
{
    miner.Update();
}