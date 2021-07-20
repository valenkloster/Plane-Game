using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core
{
    class TableroPuntaje : EntidadEstatica
    {
        private SpriteFont fuente;
        public TableroPuntaje(int pantalla_alto,
            int pantalla_largo,
            ContentManager cm,
            SpriteBatch sb,
            Color color) : base(pantalla_alto, pantalla_largo, cm, sb, color)
        {
            fuente = cm.Load<SpriteFont>("Fuente");
            
            this.Init();
        }
        protected override void Init()
        {
            //this.X = 0;
            //this.Y = 0;
            ////this.Ancho = this.Imagen.Width;
            ////this.Alto = this.Imagen.Height;
        }
        public override void Dibujar()
        {
            spriteBatch.DrawString(fuente, "Puntaje: " + this.Puntaje.ToString(), new Vector2(420,10), this.Color);
            spriteBatch.DrawString(fuente, "Balas  : " + this.Balas.ToString(), new Vector2(420, 35), this.Color);
        }
        public void Dibujar(string mensaje)
        {
            spriteBatch.DrawString(fuente, mensaje, new Vector2(200, 250), this.Color);
            
        }
        public override void Mover(float velX, float velY, ImgDireccion dir)
        {
            velX = 0;
            velY = 0;
        }
        public int Puntaje { get; set; }
        public int Balas { get; set; }
        public int Naves { get; set; }
    }
}
