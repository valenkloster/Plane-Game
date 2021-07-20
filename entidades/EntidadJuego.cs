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
    public abstract class EntidadJuego 
    {

        #region "Enumeradores"
        public enum Estado
        {
            IDLE = 0,
            MOVIENDOSE = 1,
            SALTANDO = 2,
            COLISION=3
                
        }
        public enum ImgDireccion
        {
            IZQUIERDA = 0,
            IDLE = 1,
            DERECHA = 2,
            ARRIBA=3,
            ABAJO=4
        }
        #endregion
        #region "Variables"
        protected float VELOCIDAD_SPRITE_X = 0;
        protected float VELOCIDAD_SPRITE_Y = 0;
        protected Vector2 Posicion;
        protected int PANTALLA_ALTO = 0;
        protected int PANTALLA_LARGO = 0;
        protected List<Texture2D> ListaAnimacion = new List<Texture2D>();
        protected SpriteBatch spriteBatch;
        protected Texture2D Img_Colision;
        #endregion

        #region "Propiedades"
        public int X { get; set; }
        public int Y { get; set; }
        public int Ancho { get; set; }
        public int Alto { get; set; }
        public Texture2D Imagen { get; set; }
        public Estado estado { get; set; }
        public Color Color { get; set; }

        #endregion

        #region "Constructores"
        public EntidadJuego()
        {
        }
        public EntidadJuego(int pantalla_alto, int pantalla_largo, ContentManager cm, SpriteBatch sb, Color color)
        {
            this.estado = Estado.IDLE;
            this.spriteBatch = sb;
            this.PANTALLA_ALTO = pantalla_alto;
            this.PANTALLA_LARGO = pantalla_largo;
            this.Color = color;


        }
        #endregion


        #region "Métodos"

        public virtual void Dibujar()
        {
            this.Posicion.X += this.VELOCIDAD_SPRITE_X;
            this.Posicion.Y += this.VELOCIDAD_SPRITE_Y;
            this.X = (int)this.Posicion.X;
            this.Y = (int)this.Posicion.Y;

            
            spriteBatch.Draw(this.Imagen, this.Posicion, this.Color);
        }

        public virtual void Mover(float velX, float velY, ImgDireccion dir)
        {
            /*
             * Las velicidades se envían con + y -
             */
            this.estado = Estado.MOVIENDOSE;

            //Controlo movimiento horizontal --Proyecto control de colisiones
            if (this.Posicion.X < 1)
            {
                this.Posicion.X = 1;
                this.estado = Estado.IDLE;
            }
            else
            {
                if (this.Posicion.X > this.PANTALLA_LARGO - this.Imagen.Width)
                {
                    this.Posicion.X = this.PANTALLA_LARGO - this.Imagen.Width;
                    this.estado = Estado.IDLE;
                }
            }

            if (this.Posicion.Y > this.PANTALLA_ALTO - this.Imagen.Height)
            {
                this.Posicion.Y = this.PANTALLA_ALTO - this.Imagen.Height;
                this.estado = Estado.IDLE;

            }
            else
            {
                if (this.Posicion.Y < this.Imagen.Height * 6)
                {
                    this.Posicion.Y = this.Imagen.Height * 6;
                    this.estado = Estado.IDLE;

                }
            }


            //Izquierda-Derecha
            if (this.estado == Estado.MOVIENDOSE)
            {
                this.VELOCIDAD_SPRITE_X = velX;
                this.VELOCIDAD_SPRITE_Y = velY;
            }

            this.Imagen = this.ListaAnimacion[(int)dir];


        }

        
        public virtual void Parar()
        {
            this.VELOCIDAD_SPRITE_X = 0;
            this.VELOCIDAD_SPRITE_Y = 0;
            this.estado = Estado.IDLE;
            this.Imagen = ListaAnimacion[1];
        }

        
        #endregion
    }
}
