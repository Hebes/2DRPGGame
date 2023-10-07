using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    玩家动画触发

-----------------------*/


namespace RPGGame
{
    public class PlayerAnimationTriggers : MonoBehaviour
    {
        private Player player => GetComponentInParent<Player>();

        private void AnimationTrigger()
        {
            player.AnimationTrigger();
        }

        private void AttackTrigger()
        {
            ConfigEvent.EventPlayAudioSource.EventTriggerUniTask(ConfigAudio.mp3sfx_attack3, EAudioSourceType.SFX, false).Forget();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    EnemyStats _target = hit.GetComponent<EnemyStats>();
                    if (_target != null)
                        player.stats.DoDamage(_target);
                    ItemData_Equipment weaponData = Inventory.Instance.GetEquipment(EquipmentType.Weapon);
                    if (weaponData != null)
                        weaponData.Effect(_target.transform);
                }
            }
        }

        private void ThrowSword()
        {
            ModelSkillManager.Instance.GetSkill<Sword_Skill>().CreateSword();
        }
    }

}
