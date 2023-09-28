using Cysharp.Threading.Tasks;
using System.Collections.Generic;

/*--------�ű�����-----------

�������䣺
	1607388033@qq.com
����:
	����
����:
	��ʼ����Ϸ

-----------------------*/

namespace Core
{
    /// <summary> ��Ϸ���� </summary>
    public enum EInitGameProcess
    {
        /// <summary> ��ʼ����ܻ������� </summary>
        FSMInitCore,
        /// <summary> ��ʼ����ܹ������ </summary>
        FSMInitManagerCore,
        /// <summary> ��ʼ������ </summary>
        FSMInitModel,
        /// <summary> ��ʼ��UI </summary>
        FSMInitUI,
        /// <summary> ������Ϸ </summary>
        FSMEnterGame,
    }

    public class InitGame
    {
        public void Start()
        {
            SwitchInitGameProcess(EInitGameProcess.FSMInitCore).Forget();
        }

        private async UniTaskVoid SwitchInitGameProcess(EInitGameProcess initGameProcess)
        {
            switch (initGameProcess)
            {
                case EInitGameProcess.FSMInitCore: await FSMInitCore(); break;
                    //case EInitGameProcess.FSMInitManagerCore: FSMInitManagerCore(); break;
                    //case EInitGameProcess.FSMInitModel: FSMInitModel(); break;
                    //case EInitGameProcess.FSMInitUI: FSMInitUI(); break;
                    //case EInitGameProcess.FSMEnterGame: FSMEnterGame().Forget(); break;
            }
        }

        private async UniTask FSMInitCore()
        {
            List<ICore> _initHs = new List<ICore>();
            _initHs.Add(new DebugManager());//��־����
            _initHs.Add(new EventManager());//�¼�����
            _initHs.Add(new DataManager());//�¼�����
            _initHs.Add(new ResourceManager());//���ع���
            _initHs.Add(new UIManager());//UI����

            foreach (var init in _initHs)
            {
                init.ICroeInit();
                await UniTask.Yield();
            }
            SwitchInitGameProcess(EInitGameProcess.FSMInitManagerCore);
        }
    }
}
