/*--------脚本描述-----------

电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
	模块初始化接口

-----------------------*/

using Cysharp.Threading.Tasks;

namespace RPGGame
{
    public interface IModelInit
    {
        public UniTask Init();
    }
}
