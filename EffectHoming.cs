using Raylib_cs;
using System.Numerics;

public class EffectHoming : Effect {
    Vector2 dirVec = Vector2.Zero;
    Vector2 velocity = Vector2.Zero;
    public EffectHoming (Player player, Vector2 dirVec, Vector2 explosionPos, Spell spell){
        rect = player.rect;
        effectTexture = Textures.fireParticles;
        //effectExplosionTexture = Textures.waterExplosion;
        this.damage = 4;
        this.speed = 4;
        this.player = player;
        this.dirVec = dirVec;
        this.frames = 10;
        this.explosionPos = explosionPos;
        this.onlyAnimation = true;
        this.spell = spell;
        this.pos = spell.pos;
    }

    public EffectHoming (Enemy enemy, Vector2 dirVec, Vector2 explosionPos, Spell spell){
        rect = enemy.rect;
        effectTexture = Textures.waterball;
        effectExplosionTexture = Textures.waterExplosion;
        this.damage = 4;
        this.speed = 4;
        this.enemy = enemy;
        this.dirVec = dirVec;
        this.frames = 10;
        this.explosionPos = explosionPos;
        this.spell = spell;
        this.pos = spell.pos;
    }

    public override void UpdatePlayer(Player player) {

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
            pos+= velocity;
            //spell.changePos(pos+= velocity) ;
            rect.Position = pos;

        base.UpdatePlayer(player);
    }

    public override void UpdateEnemy(Enemy enemy) {

        if (rect.X >= enemy.rect.X + enemy.rect.Width) {
            velocity.X = -speed;
        }

        if (rect.X + rect.Width <= enemy.rect.X) {
            velocity.X = speed;
        }

        if (rect.Y >= enemy.rect.Y + enemy.rect.Height) {
            velocity.Y = -speed;
        }

        if (rect.Y + rect.Height <= enemy.rect.Y) {
            velocity.Y = speed;
        }

        if (velocity.X != 0 && velocity.Y != 0) {
            float x = (float)Math.Floor((velocity.X*velocity.X) / velocity.Length()); 
            float y = (float)Math.Floor((velocity.Y*velocity.Y) / velocity.Length()); 
            velocity.X = velocity.X < 0 ? -x : x;
            velocity.Y = velocity.Y < 0 ? -y : y;
        } 
            pos+= velocity;
            //spell.changePos(pos+= velocity);
            rect.Position = pos;
        
        base.UpdateEnemy(enemy);
    }

    public override void Draw() {
        base.Draw();
    }

}
