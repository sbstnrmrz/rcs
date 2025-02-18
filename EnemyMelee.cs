using Raylib_cs;
using System.Numerics;

public class EnemyMelee : Enemy {
    public Vector2 predictedPlayerPos; 

    public EnemyMelee(Vector2 initialPos) : base(initialPos) {
        this.speed = 3;
    }

    public override void Update(Player player, float deltaTime) {
        velocity = Vector2.Zero;
        if (rect.X >= player.rect.X + player.rect.Width) {
            velocity.X = -speed;
        }

        if (rect.X + rect.Width <= player.rect.X) {
            velocity.X = speed;
        }

        if (rect.Y >= player.rect.Y + player.rect.Height) {
            velocity.Y = -speed;
        }

        if (rect.Y + rect.Height <= player.rect.Y) {
            velocity.Y = speed;
        }

        if (velocity.X != 0 && velocity.Y != 0) {
            float x = (float)Math.Floor((velocity.X*velocity.X) / velocity.Length()); 
            float y = (float)Math.Floor((velocity.Y*velocity.Y) / velocity.Length()); 
            velocity.X = velocity.X < 0 ? -x : x;
            velocity.Y = velocity.Y < 0 ? -y : y;
        } 
        pos += velocity;
        rect.Position = pos;

        base.Update(player, deltaTime);
    }
    public override void Draw() {
        base.Draw();
//      Raylib.DrawCircleV(predictedPlayerPos, 8, Color.Magenta);
//      Raylib.DrawLineV(Util.GetRectCenter(rect), predictedPlayerPos, Color.Black);
    }

}
