using Core;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    音频模块

-----------------------*/

namespace RPGGame
{
    public enum EAudioSourceType
    {
        /// <summary> 背景音乐 </summary>
        BGM,
        /// <summary> 音效 </summary>
        SFX,
    }

    public class ModelAudioManager : IModelInit
    {
        public static ModelAudioManager Instance;
        private AudioSource sfx;
        private AudioSource bgm;

        private Dictionary<string, AudioClip> audioClipsDic;

        public async UniTask Init()
        {
            Instance = this;

            audioClipsDic = new Dictionary<string, AudioClip>();
            GameObject AudioManagerGo = new GameObject("AudioManager");
            GameObject.DontDestroyOnLoad(AudioManagerGo);
            this.sfx = AudioManagerGo.AddComponent<AudioSource>();
            this.bgm = AudioManagerGo.AddComponent<AudioSource>();
            await UniTask.Yield();

            //事件监听
            ConfigEvent.EventStopAudioSource.AddEventListener<EAudioSourceType>(StopAudioSource);
            ConfigEvent.EventPlayAudioSource.AddEventListenerUniTask<string, EAudioSourceType, bool>(PlayAudioSource);
        }


        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioClipName">音效名称</param>
        /// <param name="audioSourceType">音效的类型</param>
        /// <param name="isLoop">是否循环</param>
        /// <returns></returns>
        private async UniTask PlayAudioSource(string audioClipName, EAudioSourceType audioSourceType, bool isLoop = false)
        {
            AudioClip audioClip = null;
            if (audioClipsDic.TryGetValue(audioClipName, out audioClip))
            {
                Play(audioClip, audioSourceType, isLoop);
                return;
            }
            audioClip = await LoadResExtension.LoadAsync<AudioClip>(audioClipName);
            Play(audioClip, audioSourceType, isLoop);
            audioClipsDic.Add(audioClipName, audioClip);
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioClip"></param>
        /// <param name="audioSourceType"></param>
        /// <param name="isLoop"></param>
        private void Play(AudioClip audioClip, EAudioSourceType audioSourceType, bool isLoop = false)
        {
            AudioSource audioSource = null;
            switch (audioSourceType)
            {
                case EAudioSourceType.BGM: audioSource = bgm; break;
                case EAudioSourceType.SFX: audioSource = sfx; break;
            }

            audioSource.clip = audioClip;
            audioSource.loop = isLoop;
            audioSource.volume = Random.Range(.85f, 1.1f);
            audioSource.Play();
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <param name="audioSourceType"></param>
        public void StopAudioSource(EAudioSourceType audioSourceType)
        {
            switch (audioSourceType)
            {
                case EAudioSourceType.BGM: bgm.Stop(); break;
                case EAudioSourceType.SFX: sfx.Stop(); break;
            }
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="_sfxIndex"></param>
        /// <param name="_source"></param>
        public void PlaySFX(int _sfxIndex, Transform _source)
        {
            //if (canPlaySFX == false)
            //    return;

            //if (_source != null && Vector2.Distance(PlayerManager.Instance.player.transform.position, _source.position) > sfxMinimumDistance)
            //    return;


            //if (_sfxIndex < sfx.Length)
            //{
            //    sfx[_sfxIndex].pitch = Random.Range(.85f, 1.1f);//音源的音高。
            //    sfx[_sfxIndex].Play();
            //}
        }

        public void StopSFX(int _index)
        {
            //sfx[_index].Stop();
        }

        public void StopSFXWithTime(int _index)
        {
            //StartCoroutine(DecreaseVolume(sfx[_index]));
        }

        private IEnumerator DecreaseVolume(AudioSource _audio)
        {
            float defaultVolume = _audio.volume;

            while (_audio.volume > .1f)
            {
                _audio.volume -= _audio.volume * .2f;
                yield return new WaitForSeconds(.6f);

                if (_audio.volume <= .1f)
                {
                    _audio.Stop();
                    _audio.volume = defaultVolume;
                    break;
                }
            }

        }

        public void PlayRandomBGM()
        {
            //bgmIndex = Random.Range(0, bgm.Length);
            //PlayBGM(bgmIndex);
        }

        public void PlayBGM(int _bgmIndex)
        {
            //bgmIndex = _bgmIndex;

            //StopAllBGM();
            //bgm[bgmIndex].Play();
        }



    }
}