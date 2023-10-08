using Core;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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

            //�¼�����
            ConfigEvent.EventStopAudioSource.AddEventListener<EAudioSourceType>(StopAudioSource);
            ConfigEvent.EventPlayAudioSource.AddEventListenerUniTask<string, EAudioSourceType, bool>(PlayAudioSource);
        }


        /// <summary>
        /// ������Ч
        /// </summary>
        /// <param name="audioClipName">��Ч����</param>
        /// <param name="audioSourceType">��Ч������</param>
        /// <param name="isLoop">�Ƿ�ѭ��</param>
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
        public void StopAudioSource(EAudioSourceType audioSourceType)
        {
            switch (audioSourceType)
            {
                case EAudioSourceType.BGM: bgm.Stop(); break;
                case EAudioSourceType.SFX: sfx.Stop(); break;
            }
        }

        /// <summary>
        /// ������Ч
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
            //    sfx[_sfxIndex].pitch = Random.Range(.85f, 1.1f);//��Դ�����ߡ�
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