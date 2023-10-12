using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*--------�ű�����-----------

�������䣺
    1607388033@qq.com
����:
    ����
����:
    ��Ϸ������

-----------------------*/

namespace RPGGame
{
    public class GameManager : SinglentMono<GameManager>, ISaveManager
    {

        public static GameManager instance;

        private Transform player;

        [SerializeField] private Checkpoint[] checkpoints;
        [SerializeField] private string closestCheckpointId;

        [Header("Lost currency")]
        [SerializeField] private GameObject lostCurrencyPrefab;
        public int lostCurrencyAmount;
        [SerializeField] private float lostCurrencyX;
        [SerializeField] private float lostCurrencyY;

        private void Start()
        {
            checkpoints = FindObjectsOfType<Checkpoint>();
            player = ModelPlayerManager.Instance.player.transform;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                RestartScene();
        }
        public void RestartScene()
        {
            ModelSave.Instance.SaveGame();
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }


        private void LoadCheckpoints(GameData _data)
        {
            foreach (KeyValuePair<string, bool> pair in _data.checkpoints)
            {
                foreach (Checkpoint checkpoint in checkpoints)
                {
                    if (checkpoint.id == pair.Key && pair.Value == true)
                        checkpoint.ActivateCheckpoint();
                }
            }
        }

        private void LoadLostCurrency(GameData _data)
        {
            lostCurrencyAmount = _data.lostCurrencyAmount;
            lostCurrencyX = _data.lostCurrencyX;
            lostCurrencyY = _data.lostCurrencyY;

            if (lostCurrencyAmount > 0)
            {
                GameObject newLostCurrency = Instantiate(lostCurrencyPrefab, new Vector3(lostCurrencyX, lostCurrencyY), Quaternion.identity);
                newLostCurrency.GetComponent<LostCurrencyController>().currency = lostCurrencyAmount;
            }

            lostCurrencyAmount = 0;
        }

        private IEnumerator LoadWithDelay(GameData _data)
        {
            yield return new WaitForSeconds(.1f);

            LoadCheckpoints(_data);
            LoadClosestCheckpoint(_data);
            LoadLostCurrency(_data);
        }

        private void LoadClosestCheckpoint(GameData _data)
        {
            if (_data.closestCheckpointId == null)
                return;


            closestCheckpointId = _data.closestCheckpointId;

            foreach (Checkpoint checkpoint in checkpoints)
            {
                if (closestCheckpointId == checkpoint.id)
                    player.position = checkpoint.transform.position;
            }
        }


        /// <summary>
        /// �Ƿ���ͣ��Ϸ
        /// </summary>
        /// <param name="_pause"></param>
        public void PauseGame(bool _pause)
        {
            Time.timeScale = _pause ? 0 : 1;
        }


        //�������ش浵
        public void LoadData(GameData _data)
        {
            StartCoroutine(LoadWithDelay(_data));
        }

        public void SaveData(ref GameData _data)
        {
            _data.lostCurrencyAmount = lostCurrencyAmount;
            _data.lostCurrencyX = player.position.x;
            _data.lostCurrencyY = player.position.y;


            if (FindClosestCheckpoint() != null)
                _data.closestCheckpointId = FindClosestCheckpoint().id;

            _data.checkpoints.Clear();

            foreach (Checkpoint checkpoint in checkpoints)
                _data.checkpoints.Add(checkpoint.id, checkpoint.activationStatus);
        }

        private Checkpoint FindClosestCheckpoint()
        {
            float closestDistance = Mathf.Infinity;
            Checkpoint closestCheckpoint = null;

            foreach (var checkpoint in checkpoints)
            {
                float distanceToCheckpoint = Vector2.Distance(player.position, checkpoint.transform.position);

                if (distanceToCheckpoint < closestDistance && checkpoint.activationStatus == true)
                {
                    closestDistance = distanceToCheckpoint;
                    closestCheckpoint = checkpoint;
                }
            }

            return closestCheckpoint;
        }
    }
}