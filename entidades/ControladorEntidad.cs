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
    class ControladorEntidad:IControlador
    {
        public ControladorEntidad(int pantalla_alto,
            int pantalla_largo,
            ContentManager cm,
            SpriteBatch sb)
        {
            this.PANTALLA_ALTO = pantalla_alto;
            this.PANTALLA_LARGO = pantalla_largo;
            this.spriteBatch = sb;
            this.content = cm;
            this.EntidadesMovimiento = new Dictionary<int, EntidadMovimiento>();
            this.EntodadesEstaticas = new Dictionary<int, EntidadJuego>();
            tablero = new TableroPuntaje(this.PANTALLA_ALTO, this.PANTALLA_LARGO, content, this.spriteBatch, Color.White);
            tablero.Naves = 50;
            tablero.Puntaje = 0;
            tablero.Balas = 20;

             
            AgregarEntidades();
        }
        
        
       
        int PANTALLA_ALTO;
        int PANTALLA_LARGO;
        SpriteBatch spriteBatch;
        ContentManager content;
        TableroPuntaje tablero;
        int CantidadBalasLimite = 20;
        int ciclo = 0;

        public Dictionary<int, EntidadMovimiento> EntidadesMovimiento { get; set; }
        public Dictionary<int, EntidadJuego> EntodadesEstaticas { get; set; }

        public Avion Actor { get; set; }       
        public void BorrarEntidad() {
            if (ciclo >= 5)
            {
                List<int> Colisiones = new List<int>();
                //Recorro todas las balas para ver cual borrar
                foreach (var l in this.Actor.balas.Keys)
                {
                    if (this.Actor.balas[l].estado==EntidadJuego.Estado.COLISION 
                        || this.Actor.balas[l].Y<0) { Colisiones.Add(l); }
                    }
                
                foreach (int p in Colisiones)
                {
                    this.Actor.balas.Remove(p);
                }

                //Recorro todas las naves para ver cual borrar
                //Recorro todas las balas para ver cual borrar
                foreach (var l in this.EntidadesMovimiento.Keys)
                {
                    if (this.EntidadesMovimiento[l].estado == EntidadJuego.Estado.COLISION) {
                        tablero.Puntaje += 1;
                        Colisiones.Add(l); }
                    
                }

                foreach (int p in Colisiones)
                {
                    this.EntidadesMovimiento.Remove(p);
                }
                
                ciclo = 0;
            }
            else { ciclo += 1; }
        }
        public void Actualizar() {

            this.BorrarEntidad();

            //Controlo si mi Nave colisionón con otras naves
            Rectangle r_Actor = new Rectangle(this.Actor.X, this.Actor.Y, this.Actor.Ancho, this.Actor.Alto);

            //Muevo todas las naves Enemigas
            foreach (var l in this.EntidadesMovimiento.Values)
            {
                l.Mover();
            }

                //Controlo si las balas colisionaron con las naves
                int cont = 0;
            foreach (var l in this.Actor.balas.Keys)
            {
                this.Actor.balas[l].Mover();
                cont += 1;
                //Creamos el rect para la bala
                Rectangle r_bala = new Rectangle(this.Actor.balas[l].X,
                        this.Actor.balas[l].Y, this.Actor.balas[l].Ancho,
                        this.Actor.balas[l].Alto);
                foreach (var n in this.EntidadesMovimiento.Keys)
                {
                    Rectangle r_nave = new Rectangle(this.EntidadesMovimiento[n].X,
                        this.EntidadesMovimiento[n].Y, this.EntidadesMovimiento[n].Ancho,
                        this.EntidadesMovimiento[n].Alto);
                    if (r_nave.Intersects(r_bala) || r_nave.Intersects(r_Actor))
                    {
                        //pum                    
                        this.EntidadesMovimiento[n].Colision();                     
                    }
                    if (r_nave.Intersects(r_bala) )
                    {
                        //pum                    
                        this.Actor.balas[l].Colision();
                        //cuento las colisiones
                        
                    }                    
                }

                
            }


            
            #region "Teclas"
            this.Actor.Parar();
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.Actor.Mover(-5f,0f,Core.Avion.ImgDireccion.IZQUIERDA);
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.Actor.Mover(5f, 0f, Core.Avion.ImgDireccion.DERECHA);
            }
            else
            {
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.Actor.Mover(0f,-5f,Core.Avion.ImgDireccion.IDLE);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.Actor.Mover(0f, 5f, Core.Avion.ImgDireccion.IDLE);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //Creamos la Bala

                try
                {
                    //Cuento las balas
                    tablero.Balas = this.CantidadBalasLimite - this.Actor.balas.Count;
                    if (this.Actor.balas.Count < 20){
                        Bala bala_Iz = new Bala(this.PANTALLA_ALTO, this.PANTALLA_LARGO, content, this.spriteBatch, Color.White, this.Actor.X + (this.Actor.Imagen.Width / 4), this.Actor.Y);
                        this.Actor.balas.Add(int.Parse(DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString()), bala_Iz);
                        System.Threading.Thread.Sleep(10);
                        Bala bala_De = new Bala(this.PANTALLA_ALTO, this.PANTALLA_LARGO, content, this.spriteBatch, Color.White, this.Actor.X + 2 * (this.Actor.Imagen.Width / 4), this.Actor.Y);
                        this.Actor.balas.Add(int.Parse(DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString()), bala_De);
                    }
                }catch (Exception ex)
                {
                    //la dejo pasar
                }
            }
            #endregion
        }
        public void Dibujar() {

           
            //Dibujo los Elementos Estáticos
            foreach (var l in this.EntodadesEstaticas.Values)
            {
                l.Dibujar();
            }
            //Dibujo el Avión
            this.Actor.Dibujar();
            //Dibujo las balas
            foreach (var l in this.Actor.balas.Values)
            {
                l.Dibujar();
            }

            //Dibujo a todos los enemigos
            foreach (var l in this.EntidadesMovimiento.Values)
            {
                l.Dibujar();
            }

            //Dibujamos el tablero
            this.tablero.Dibujar();

            if (tablero.Puntaje == 50)
            {

                tablero.Dibujar("F E L I C i T a C i O N E S!!!");
            }
        }
        private void AgregarEntidades() {
            //fondo
            
            this.Actor = new Core.Avion(PANTALLA_ALTO, PANTALLA_LARGO, content, spriteBatch, Color.White);

            EntidadJuego objeto;
            objeto = new Mar(PANTALLA_ALTO, PANTALLA_LARGO, content, spriteBatch, Color.White);
            this.EntodadesEstaticas.Add(0, objeto);
            
            EntidadMovimiento enemigo;
            for (int i = 0; i < 50; i++)
            {
                System.Threading.Thread.Sleep(10);
                enemigo = new Core.NaveChica(PANTALLA_ALTO, PANTALLA_LARGO, content, spriteBatch, Color.White);

                this.EntidadesMovimiento.Add(i, enemigo);
            }
            
        }

        
    }
}
