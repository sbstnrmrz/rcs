using Raylib_cs;
using System.Numerics;
using System.Text.Json;

public class TextureJSON {
    Dictionary<string, object>? dict;
    public TextureJSON(string jsonFile) {
        string textureInfoJSON = File.ReadAllText(jsonFile);
        dict = JsonSerializer.Deserialize<Dictionary<string, object>>(textureInfoJSON);
        if (dict == null) {
            return;
        }

    }

    public TextureInfo GetTextureInfo(string textureID) {
        if (dict.TryGetValue(textureID, out object? value)) {
            if (dict == null) {
                Console.WriteLine("error getting value: " + textureID);
                return new TextureInfo(0, 0, 0, 0, 0);
            }
            return JsonSerializer.Deserialize<TextureInfo>(value.ToString());
        }

        return new TextureInfo(0, 0, 0, 0, 0);
    }
}

public class TextureAtlas {
    public TextureAtlas() {

    }
}

public class TextureInfo {
    public string id = "";
    public int x {get; set;}
    public int y {get; set;}
    public int rotation {get; set;}
    public int width {get; set;}
    public int height {get; set;}

    public TextureInfo(int x, int y, int rotation, int width, int height) {
        this.x = x;
        this.y = y;
        this.rotation = rotation;
        this.width = width;
        this.height = height;
    }
}

public static class Textures {
    public static Texture2D player = Raylib.LoadTexture("assets/player1.png");
    public static Texture2D arrow = Raylib.LoadTexture("arrow.png");
    public static Texture2D fireball = Raylib.LoadTexture("assets/fireball.png");
    public static Texture2D fireParticles = Raylib.LoadTexture("assets/fire_particles.png");
    public static Texture2D fireExplosion = Raylib.LoadTexture("assets/fire_explosion.png");
    public static Texture2D waterball = Raylib.LoadTexture("assets/waterball.png");
    public static Texture2D waterExplosion = Raylib.LoadTexture("assets/water_explosion.png");
    public static Texture2D iceshard = Raylib.LoadTexture("assets/iceshard.png");
    public static Texture2D iceParticles = Raylib.LoadTexture("assets/ice_particles.png");
    public static Texture2D iceshardExplosion = Raylib.LoadTexture("assets/ice_explosion.png");
    public static Texture2D lighting = Raylib.LoadTexture("assets/lightning.png");
    public static Texture2D lightingExplosion = Raylib.LoadTexture("assets/lightning_explosion.png");
    public static Texture2D bomb = Raylib.LoadTexture("assets/bomb.png");
    public static Texture2D walls = Raylib.LoadTexture("assets/walls.png");
    public static Texture2D pointers = Raylib.LoadTexture("assets/pointers.png");
    public static Texture2D tiles = Raylib.LoadTexture("assets/tiles.png");
    public static Texture2D enemyMelee = Raylib.LoadTexture("assets/enemy_melee.png");
    public static Texture2D enemySpellcaster = Raylib.LoadTexture("assets/enemy_spellcaster.png");
    public static Texture2D enemyBouncer = Raylib.LoadTexture("assets/enemy_bouncer.png");
    public static Texture2D enemyCharger = Raylib.LoadTexture("assets/enemy_charger.png");
    public static Texture2D enemyTeleporter = Raylib.LoadTexture("assets/enemy_teleporter.png");
    public static Texture2D portal = Raylib.LoadTexture("assets/portal.png");
    public static Texture2D tree = Raylib.LoadTexture("assets/tree.png");
    public static Texture2D floor = Raylib.LoadTexture("assets/floor.png");

    public static Texture2D hearts = Raylib.LoadTexture("assets/player_hearts.png");
    public static Texture2D habilities = Raylib.LoadTexture("assets/player_habilities.png");
    public static Texture2D dash = Raylib.LoadTexture("assets/player_dash.png");
    public static Texture2D logo = Raylib.LoadTexture("assets/Logo.png");
    public static Texture2D menuBackground = Raylib.LoadTexture("assets/background.png");
    public static Texture2D menuRectangles = Raylib.LoadTexture("assets/main_menu_rectangles.png");
    public static Texture2D powerSelectRectangle = Raylib.LoadTexture("assets/Select_Screen.png");
    public static Texture2D arrows = Raylib.LoadTexture("assets/arrows.png");
    public static Texture2D firePreview = Raylib.LoadTexture("assets/fire_selection.png");
    public static Texture2D waterPreview = Raylib.LoadTexture("assets/water_selection.png");
    public static Texture2D icePreview = Raylib.LoadTexture("assets/ice_selection.png");
    public static Texture2D lightingPreview = Raylib.LoadTexture("assets/lighting_selection.png");
    public static Texture2D bombPreview = Raylib.LoadTexture("assets/bomb_selection.png");
    public static Texture2D player_info = Raylib.LoadTexture("assets/player_info.png");

    public static Texture2D scrollbar = Raylib.LoadTexture("assets/scrollbars_sounds.png");
    public static Texture2D scrollbarThumb = Raylib.LoadTexture("assets/scrollbars_thumb.png");


    public static TextureJSON pointersInfo = new TextureJSON("assets/pointers.json");
}
