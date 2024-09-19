namespace Rogalik.Scripts.Player.Attack
{
    public class PierceBullet : BaseBullet
    {
        public int pierceCount = 2;
        
        private int _piercedEnemies;
        
        protected override void HitEnemy(EnemyHP enemy)
        {
            if (enemy == null) return;
            
            enemy.TakeDamage((int) (Stats.damage * damageMultiplier));
            _piercedEnemies++;
            
            if (_piercedEnemies >= pierceCount)
                Destroy(gameObject);
        }
    }
}