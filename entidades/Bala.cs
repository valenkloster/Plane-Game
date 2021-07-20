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
    public class Bala : EntidadMovimiento
    {
        public Bala(int pantalla_alto,
            int pantalla_largo,
            ContentManager cm, 
            SpriteBatch sb,
            Color color, int X, int Y) :base(pantalla_alto, pantalla_largo, cm, sb, color){
            this.Imagen = cm.Load<Texture2D>("Bala");
            this.Img_Colision = cm.Load<Texture2D>("pum_nave_chica");
            this.Posicion = new Vector2(X, Y);
            this.X = X;
            this.Y = Y;
            this.Init();
        }

        public override void Mover()
        {
            this.VELOCIDAD_SPRITE_Y = -8;
            this.X = (int)this.Posicion.X;
            this.Y = (int)this.Posicion.Y;
        }
        
       
    }
}
