using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    UIģ��

-----------------------*/

namespace RPGGame
{
    public class ModelUI : IModelInit, ISaveManager
    {

        public static ModelUI Instance;

        [SerializeField] private UI_VolumeSlider[] volumeSettings;

        public async UniTask Init()
        {
            Instance = this;
            CoreMono.Instance.UpdateAddEvent(OnUpdate);
            await UniTask.Yield();
        }



        void OnUpdate()
        {
            Debug.Log("������");
            //��ɫ����
            if (Input.GetKeyDown(KeyCode.C))
                SwitchWithKeyTo<UIPanel_Character>(ConfigPrefab.prefabUIPanel_Character);

            //ְҵ����
            if (Input.GetKeyDown(KeyCode.B))
                SwitchWithKeyTo<UIPanel_Craft>(ConfigPrefab.prefabUIPanel_Craft);

            //����������
            if (Input.GetKeyDown(KeyCode.K))
                SwitchWithKeyTo<UIPanel_SkillTree>(ConfigPrefab.prefabUIPanel_SkillTree);

            //���ý���
            if (Input.GetKeyDown(KeyCode.O))
                SwitchWithKeyTo<UIPanel_Options>(ConfigPrefab.prefabUIPanel_Options);
        }




        /// <summary>
        /// ������ر�ָ���Ľ���
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="UIPanelName">���������</param>
        public void SwitchWithKeyTo<T>(string UIPanelName) where T : UIBase, new()
        {
            //ѡ���UI�����ǿ���״̬
            T t = UIPanelName.GetUIPanl<T>();
            if (t != null && t.ChakckUIPanelOpen())
            {
                ConfigPrefab.prefabUIPanel_InGame.CloseUIPanel();
                ConfigPrefab.prefabUIPanel_InGame.ShwoUIPanel<UIPanel_InGame>();//����InGame����
                GameManager.Instance?.PauseGame(false);
                return;
            }

            //ѡ���UI�����ǹر�״̬,��ִ������Ĵ���
            UIPanelName.CloseUIPanel();
            UISwitch<T>(UIPanelName);
        }

        /// <summary>
        /// UI�����л�
        /// </summary>
        /// <typeparam name="T">UI����</typeparam>
        /// <param name="UIPanelName">UI�������</param>
        public void UISwitch<T>(string UIPanelName, bool PauseGame = true) where T : UIBase, new()
        {
            ConfigEvent.EventPlayAudioSource.EventTrigger(ConfigAudio.mp3sfx_click, EAudioSourceType.SFX, false);
            CoreUIManagerExpansion.ShwoUIPanel<T>(UIPanelName);
            GameManager.Instance?.PauseGame(PauseGame);
        }

        /// <summary>
        /// �����������
        /// </summary>
        public async UniTaskVoid SwitchOnEndScreen()
        {
            ConfigEvent.EventFadeUI.EventTrigger(true);
            await UniTask.Delay(System.TimeSpan.FromSeconds(2.5f), false);
            CoreUIManagerExpansion.ShwoUIPanel<UIPanel_RestartGame>(ConfigPrefab.prefabUIPanel_RestartGame);
        }

        //���ر�������
        public void LoadData(GameData _data)
        {
            //foreach (KeyValuePair<string, float> pair in _data.volumeSettings)
            //{
            //    foreach (UI_VolumeSlider item in volumeSettings)
            //    {
            //        if (item.parametr == pair.Key)
            //            item.LoadSlider(pair.Value);
            //    }
            //}
        }

        public void SaveData(ref GameData _data)
        {
            //_data.volumeSettings.Clear();

            //foreach (UI_VolumeSlider item in volumeSettings)
            //{
            //    _data.volumeSettings.Add(item.parametr, item.slider.value);
            //}
        }
    }
}