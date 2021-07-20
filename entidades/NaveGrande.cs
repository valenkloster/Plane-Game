using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core
{
    class NaveGrande: EntidadMovimiento
    {
        public NaveGrande(int pantalla_alto,
            int pantalla_largo,
            ContentManager cm,
            SpriteBatch sb,
            Color color) : base(pantalla_alto, pantalla_largo, cm, sb, color)
        {
            this.Imagen = cm.Load<Texture2D>("Avion_IDLE");
            this.Img_Colision = cm.Load<Texture2D>("pum_nave_Grande");
            var seed = Environment.TickCount;
            var rand = new Random(seed);
            int x = rand.Next(100, 400);
            int y = rand.Next(-500, -1);
            this.Velocidad = rand.Next(1, 3);
            this.Posicion = new Vector2(x, y);
            this.Init();
        }
    }
}
