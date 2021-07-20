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
   
    public class Avion:EntidadJuego
    {

        public Dictionary<int,Bala> balas { get; set; }

        #region "Constructores"
        public Avion()
        {
        }
        public Avion(int pantalla_alto, int pantalla_largo,
            ContentManager cm, SpriteBatch sb, Color color):base(pantalla_alto,pantalla_largo,cm,sb,color)
        {
            this.balas = new Dictionary<int, Bala>();
            this.Init(cm,sb);
        }
        #endregion


        #region "Métodos"
        public void Init(ContentManager cm, SpriteBatch sb)
        {
            //this.Imagen = cm.Load<Texture2D>("avion_Piloto_IDLE");
            ListaAnimacion.Add(cm.Load<Texture2D>("avion_Piloto_IZ_2"));
            ListaAnimacion.Add(cm.Load<Texture2D>("avion_Piloto_IDLE"));
            ListaAnimacion.Add(cm.Load<Texture2D>("avion_Piloto_DE_2"));
            this.Posicion = new Vector2(this.PANTALLA_LARGO / 2, this.PANTALLA_ALTO- 100);
            this.X = (int)this.Posicion.X;
            this.Y = (int)this.Posicion.Y;
            this.Imagen = ListaAnimacion[1];
            this.Ancho = this.Imagen.Width;
            this.Alto = this.Imagen.Height;


        }

        public override void Mover(float velX, float velY, ImgDireccion dir)
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
        public override void Parar()
        {
            this.VELOCIDAD_SPRITE_X = 0;
            this.VELOCIDAD_SPRITE_Y =0;
            if (this.Posicion.Y < this.PANTALLA_ALTO - this.Imagen.Height)
            {
                this.VELOCIDAD_SPRITE_Y = 0.43f;
            }
            this.estado = Estado.IDLE;
            this.Imagen = ListaAnimacion[1];
        }

        #endregion
    }
}
