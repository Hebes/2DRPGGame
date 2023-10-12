using Core;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ��Ƶģ��

-----------------------*/

namespace RPGGame
{
    public enum EAudioSourceType
    {
        /// <summary> �������� </summary>
        BGM,
        /// <summary> ��Ч </summary>
        SFX,
    }

    public class ModelAudio : IModelInit
    {
        public static ModelAudio Instance;
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

            //�¼�����
            ConfigEvent.EventStopAudioSource.EventAdd<EAudioSourceType>(StopAudioSource);
            ConfigEvent.EventPlayAudioSource.EventAdd<string, EAudioSourceType, bool>(PlayAudioSource);
        }


        /// <summary>
        /// ������Ч
        /// </summary>
        /// <param name="audioClipName">��Ч����</param>
        /// <param name="audioSourceType">��Ч������</param>
        /// <param name="isLoop">�Ƿ�ѭ��</param>
        /// <returns></returns>
        private void PlayAudioSource(string audioClipName, EAudioSourceType audioSourceType, bool isLoop = false)
        {
            AudioClip audioClip = null;
            if (audioClipsDic.TryGetValue(audioClipName, out audioClip))
            {
                Play(audioClip, audioSourceType, isLoop);
                return;
            }
            audioClip = LoadResExtension.Load<AudioClip>(audioClipName);
            Play(audioClip, audioSourceType, isLoop);
            audioClipsDic.Add(audioClipName, audioClip);
        }

        /// <summary>
        /// ������Ч
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
        /// ��ͣ����
        /// </summary>
        /// <param name="audioSourceType"></param>
        private void StopAudioSource(EAudioSourceType audioSourceType)
        {
            DecreaseVolume(audioSourceType).Forget();
        }

        private async UniTask DecreaseVolume(EAudioSourceType audioSourceType)
        {

            AudioSource audioSource = null;
            switch (audioSourceType)
            {
                case EAudioSourceType.BGM: audioSource = bgm; break;
                case EAudioSourceType.SFX: audioSource = sfx; break;
            }

            float defaultVolume = audioSource.volume;

            while (audioSource.volume > .1f)
            {
                audioSource.volume -= audioSource.volume * .2f;
                await UniTask.Delay(TimeSpan.FromSeconds(.6f), false);
                if (audioSource.volume <= .1f)
                {
                    audioSource.Stop();
                    audioSource.volume = defaultVolume;
                    break;
                }
            }

        }
    }
}