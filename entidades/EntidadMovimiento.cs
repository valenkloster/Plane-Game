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
    public class EntidadMovimiento: EntidadJuego
    {
       
        public EntidadMovimiento(int pantalla_alto, int pantalla_largo, 
            ContentManager cm, SpriteBatch sb, Color color):base(pantalla_alto, pantalla_largo,
            cm, sb, color)
        {

            
            


            
            this.Color = color;
            this.estado = Estado.MOVIENDOSE;
            this.spriteBatch = sb;
        }
        public int Velocidad { get; set; }
        
        protected void Init()
        {
            this.X = (int)this.Posicion.X;
            this.Y = (int)this.Posicion.Y;
            this.Ancho = this.Imagen.Width;
            this.Alto = this.Imagen.Height;
        }
        
        public void Colision()
        {
            this.estado = Estado.COLISION;
            this.Imagen = this.Img_Colision;
        }
        public virtual void Mover()
        {

            this.VELOCIDAD_SPRITE_Y = (this.Velocidad);

            if (this.Posicion.Y > this.PANTALLA_LARGO)
            {
                this.Posicion.Y = -15;
            }

            this.X = (int)this.Posicion.X;
            this.Y = (int)this.Posicion.Y;

        }

      
    }
}

