using UnityEngine;


/*--------�ű�����-----------

�������䣺
	1607388033@qq.com
����:
	����
����:
	��־ϵͳ

-----------------------*/

namespace Core
{

    public class CoreDebugManager : ICore
    {
        public static CoreDebugManager Instance;
        public void ICroeInit()
        {
            Instance = this;
            //������־
            Debug.InitSettings(new LogConfig()
            {
                enableSave = false,
                loggerType = LoggerType.Unity,
#if !UNITY_EDITOR
                //savePath = $"{Application.persistentDataPath}/LogOut/ActiveLog/",
#endif
                savePath = $"{Application.dataPath}/LogOut/ActiveLog/",
                saveName = "Debug���������־.txt",
            });
            //������־
            SystemExceptionDebug.InitSystemExceptionDebug();
            Debug.Log("��־ģ���ʼ�����!");
        }
    }
}

