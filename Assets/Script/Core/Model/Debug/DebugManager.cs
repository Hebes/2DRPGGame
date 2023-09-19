using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	日志系统

-----------------------*/

namespace Core
{

    public class DebugManager : ICore
    {
        public static DebugManager Instance;
        public void ICroeInit()
        {
            Instance = this;
            //主动日志
            Debug.InitSettings(new LogConfig()
            {
                enableSave = false,
                loggerType = LoggerType.Unity,
#if !UNITY_EDITOR
                //savePath = $"{Application.persistentDataPath}/LogOut/ActiveLog/",
#endif
                savePath = $"{Application.dataPath}/LogOut/ActiveLog/",
                saveName = "Debug主动输出日志.txt",
            });
            //被动日志
            SystemExceptionDebug.InitSystemExceptionDebug();
            Debug.Log("日志模块初始化完毕!");
        }
    }
}

