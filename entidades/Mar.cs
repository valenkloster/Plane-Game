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
    class Mar:EntidadEstatica
    {
        public Mar(int pantalla_alto,
            int pantalla_largo,
            ContentManager cm,
            SpriteBatch sb,
            Color color) : base(pantalla_alto, pantalla_largo, cm, sb, color)
        {
            this.Imagen = cm.Load<Texture2D>("mar");
            this.Img_Colision = cm.Load<Texture2D>("mar");
            this.Init();
        }
        protected override void Init()
        {
            this.X = 0;
            this.Y = 0;
            this.Ancho = this.Imagen.Width;
            this.Alto = this.Imagen.Height;
        }
        public override void Mover(float velX, float velY, ImgDireccion dir)
        {
            velX = 0;
            velY = 0;
        }
    }
}

