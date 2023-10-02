
namespace Core
{
	public interface IStateNode
	{
		void OnCreate(CoreStateMachineSystem machine);
		void OnEnter();
		void OnUpdate();
		void OnExit();
	}
}