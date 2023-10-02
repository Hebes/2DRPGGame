using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGGame
{
    public class AudioManager : SinglentMono<AudioManager>
    {

        [SerializeField] private float sfxMinimumDistance;
        [SerializeField] private AudioSource[] sfx;
        [SerializeField] private AudioSource[] bgm;


        public bool playBgm;
        private int bgmIndex;

        private bool canPlaySFX;
        protected override void Awake()
        {
            base.Awake();
            Invoke("AllowSFX", 1f);
        }

        private void Update()
        {
            if (!playBgm)
                StopAllBGM();
            else
            {
                if (!bgm[bgmIndex].isPlaying)
                    PlayBGM(bgmIndex);
            }
        }


        /// <summary>
        /// ������Ч
        /// </summary>
        /// <param name="_sfxIndex"></param>
        /// <param name="_source"></param>
        public void PlaySFX(int _sfxIndex, Transform _source)
        {
            if (canPlaySFX == false)
                return;

            if (_source != null && Vector2.Distance(PlayerManager.Instance.player.transform.position, _source.position) > sfxMinimumDistance)
                return;


            if (_sfxIndex < sfx.Length)
            {
                sfx[_sfxIndex].pitch = Random.Range(.85f, 1.1f);
                sfx[_sfxIndex].Play();
            }
        }

        public void StopSFX(int _index) => sfx[_index].Stop();

        public void StopSFXWithTime(int _index) => StartCoroutine(DecreaseVolume(sfx[_index]));

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
            bgmIndex = Random.Range(0, bgm.Length);
            PlayBGM(bgmIndex);
        }

        public void PlayBGM(int _bgmIndex)
        {
            bgmIndex = _bgmIndex;

            StopAllBGM();
            bgm[bgmIndex].Play();
        }

        public void StopAllBGM()
        {
            for (int i = 0; i < bgm.Length; i++)
            {
                bgm[i].Stop();
            }
        }

        private void AllowSFX() => canPlaySFX = true;
    }
}