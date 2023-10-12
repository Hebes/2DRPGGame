using Core;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    火炬

-----------------------*/

namespace RPGGame
{
    public class Checkpoint : MonoBehaviour
    {
        private Animator anim;
        public string id;
        public bool activationStatus;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        [ContextMenu("Generate checkpoint id")]
        private void GenerateId()
        {
            id = System.Guid.NewGuid().ToString();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                ActivateCheckpoint();
            }
        }

        /// <summary>
        /// 激活检查点
        /// </summary>
        public void ActivateCheckpoint()
        {
            if (activationStatus == false)
                ConfigEvent.EventPlayAudioSource.EventTrigger(ConfigAudio.mp3sfx_checkpoint, EAudioSourceType.SFX, false);

            activationStatus = true;
            anim.SetBool("active", true);
        }
    }

}
