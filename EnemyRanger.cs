using Raylib_cs;
using System.Numerics;

public class EnemyRanger : Enemy {
    public List<Spell> projectiles;
    public int projectileSpeed = 5;
    public int projectileFrames = 1;
    public int projectileCooldown = 40;
    public Vector2 predictedPlayerPos; 

    public EnemyRanger(Vector2 initialPos) : base(initialPos) {
        this.hp = 100;
        projectiles = [];
    }

    public override void Update(Player player, float deltaTime) {
        base.Update(player, deltaTime);
        targetPos = Util.GetRectCenter(player.rect);
        float distanceToPlayer = (float)Math.Floor(Vector2.Distance(targetPos, Util.GetRectCenter(rect)));
        float distanceToPlayerX = targetPos.Y - Util.GetRectCenter(rect).Y;
        float distanceToPlayerY = targetPos.X - Util.GetRectCenter(rect).X;
        float timeToTargetX = player.velocity.X != 0 ? (float)Math.Floor(distanceToPlayerX / player.velocity.X) : 0; 
        float timeToTargetY = player.velocity.Y != 0 ? (float)Math.Floor(distanceToPlayerY / player.velocity.Y) : 0; 
        predictedPlayerPos.X = targetPos.X + player.velocity.X * timeToTargetX;
        predictedPlayerPos.Y = targetPos.Y + player.velocity.Y * timeToTargetY;

        float predictedOpposite = predictedPlayerPos.Y - Util.GetRectCenter(rect).Y;
        float predictedAdjacent = predictedPlayerPos.X - Util.GetRectCenter(rect).X;
        float predictedAngle = (float)Math.Atan2(predictedOpposite, predictedAdjacent);
        Raylib.DrawText(String.Format("targetPos: {0}", targetPos),
                0, 100, 24, Color.Black);
        Raylib.DrawText(String.Format("angle: {0}, predicted: {1}", angle, predictedAngle),
                0, 130, 24, Color.Black);

        if (projectileFrames % projectileCooldown == 0) {
            SpellManager.enemySpells.Add(new SpellFireball(Util.GetRectCenter(rect), projectileSpeed, angle));

            projectileFrames = 0;
        }
        projectileFrames++;

        foreach (Spell spell in projectiles) {
            spell.Update(0);
        }

        hitbox = rect;
    }
    public override void Draw() {
        base.Draw();
        Raylib.DrawCircleV(predictedPlayerPos, 8, Color.Magenta);
        Raylib.DrawLineV(Util.GetRectCenter(rect), predictedPlayerPos, Color.Black);

        foreach (Spell spell in projectiles) {
            spell.Draw();
        }
    }

}
