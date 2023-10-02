using Core;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

/*--------�ű�����-----------

�������䣺
	1607388033@qq.com
����:
	����
����:
	��ʼ����Ϸ

-----------------------*/

namespace RPGGame
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
            SwitchInitGameProcess(EInitGameProcess.FSMInitCore);
        }

        private void SwitchInitGameProcess(EInitGameProcess initGameProcess) 
        {
            switch (initGameProcess)
            {
                case EInitGameProcess.FSMInitCore: FSMInitCore().Forget(); break;
                case EInitGameProcess.FSMInitManagerCore: FSMInitManagerCore(); break;
                //case EInitGameProcess.FSMInitModel: FSMInitModel(); break;
                //case EInitGameProcess.FSMInitUI: FSMInitUI(); break;
                case EInitGameProcess.FSMEnterGame: FSMEnterGame().Forget(); break;
            }
        }

        private async UniTask FSMInitManagerCore()
        {
            List<IModelInit> _initHs = new List<IModelInit>();
            _initHs.Add(new ModelEffectManager());//��Ч����

            foreach (var init in _initHs)
                await init.Init();
            SwitchInitGameProcess(EInitGameProcess.FSMEnterGame);
        }

        private async UniTask FSMEnterGame()
        {
            Debug.Log("���س���");
            await ConfigScenes.unityMainScene.LoadSceneAsync(ELoadSceneModel.Single);
        }

        private async UniTask FSMInitCore()
        {
            List<ICore> _initHs = new List<ICore>();
            _initHs.Add(new CoreDebugManager());//��־����
            _initHs.Add(new CoreEventManager());//�¼�����
            _initHs.Add(new CoreDataManager());//�¼�����
            _initHs.Add(new CoreLoadResManager());//���ع���
            _initHs.Add(new CroeUIManager());//UI����
            _initHs.Add(new CoreSceneManager());//��������

            foreach (var init in _initHs)
            {
                init.ICroeInit();
                await UniTask.Yield();
            }
             SwitchInitGameProcess(EInitGameProcess.FSMInitManagerCore);
        }
    }
}
