using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    class Nodo
    {
        private Nodo no_pai = null;
        private Nodo no_direita = null;
        private Nodo no_esquerda = null;
        private int valor = 0;
        private int balance = 0; //cada nó teve ter fator de balanceamento igual a -1, 0, +1

        public int get_valor()  { return valor;  }

        public int get_balance() { return balance; }
        
        public void set_valor( int v ) { valor = v; }

        public void set_balance( int b ) { balance = b; }
        
        public void set_no_pai( Nodo no ) { no_pai = no;}
        
        public void set_no_direita( Nodo no ) { no_direita = no; }
        
        public void set_no_esquerda( Nodo no ) { no_esquerda = no; }
        
        public Nodo get_no_pai() { return no_pai; }
        
        public Nodo get_no_direita() { return no_direita; } 
        
        public Nodo get_no_esquerda() { return no_esquerda; }

    }
}
