using Core;
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

namespace RPGGame
{
    /// <summary> ��Ϸ���� </summary>
    public enum EInitGameProcess
    {
        /// <summary> ��ʼ����ܻ������� </summary>
        FSMInitCore,
        /// <summary> ��ʼ����ܹ������� </summary>
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
                case EInitGameProcess.FSMInitManagerCore: FSMInitManagerCore().Forget(); break;
                //case EInitGameProcess.FSMInitModel: FSMInitModel(); break;
                //case EInitGameProcess.FSMInitUI: FSMInitUI(); break;
                case EInitGameProcess.FSMEnterGame: FSMEnterGame().Forget(); break;
            }
        }

        private async UniTask FSMInitManagerCore()
        {
            List<IModelInit> _initHs = new List<IModelInit>();
            _initHs.Add(new ModelEffect());
            _initHs.Add(new ModelAudio());
            _initHs.Add(new ModelUI());
            _initHs.Add(new ModelSave());
            _initHs.Add(new ModelData());
            _initHs.Add(new ModelSkill());

            foreach (var init in _initHs)
                await init.Init();
            SwitchInitGameProcess(EInitGameProcess.FSMEnterGame);
        }

        private async UniTask FSMEnterGame()
        {
            await UniTask.Yield();

            //��ʾUI  TODO����Ҫ�޸ĵ������ط�
            //��һ�ݶ����ɵ�
            ConfigPrefab.prefabUIPanel_DarkScreen.ShwoUIPanel<UIPanel_DarkScreen>();
            //�ڶ��ݶ����ɵ�
            ConfigPrefab.prefabUIPanel_RestartGame.ShwoUIPanel<UIPanel_RestartGame>();    //������Ϸ
            ConfigPrefab.prefabUIPanel_Character.ShwoUIPanel<UIPanel_Character>();        //��ɫ
            ConfigPrefab.prefabUIPanel_Options.ShwoUIPanel<UIPanel_Options>();            //����
            ConfigPrefab.prefabUIPanel_Craft.ShwoUIPanel<UIPanel_Craft>();                //���ս���
            ConfigPrefab.prefabUIPanel_MainMenu.ShwoUIPanel<UIPanel_MainMenu>();          //���˵����

            ConfigPrefab.prefabUIPanel_DarkScreen.CloseUIPanel();
            ConfigPrefab.prefabUIPanel_RestartGame.CloseUIPanel();    //������Ϸ
            ConfigPrefab.prefabUIPanel_Character.CloseUIPanel();        //��ɫ
            ConfigPrefab.prefabUIPanel_Options.CloseUIPanel();            //����
            ConfigPrefab.prefabUIPanel_Craft.CloseUIPanel();                //���ս���
            
        }

        private async UniTask FSMInitCore()
        {
            List<ICore> _initHs = new List<ICore>();
            _initHs.Add(new CoreDebug());//��־����
            _initHs.Add(new CoreMono());//Mono����
            _initHs.Add(new CoreEvent());//�¼�����
            _initHs.Add(new CoreData());//�¼�����
            _initHs.Add(new CoreLoadRes());//���ع���
            _initHs.Add(new CroeUI());//UI����
            _initHs.Add(new CoreScene());//��������

            foreach (var init in _initHs)
            {
                init.ICroeInit();
                await UniTask.Yield();
            }
            SwitchInitGameProcess(EInitGameProcess.FSMInitManagerCore);
        }
    }
}