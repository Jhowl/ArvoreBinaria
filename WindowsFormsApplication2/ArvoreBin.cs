using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    class ArvoreBin
    { 
        private Nodo raiz = null;// raiz da árvore
        
        private int qtde = 0;// qtde de nos internos
        
        private string resultado ="";
        
        public int qtde_nos_internos() // devolve a qtde de nós internos
        { 
            return qtde;
        }
        
        public bool no_eh_externo(Nodo no)// verifica se um determinado Nodo é externo
        { 
            return(no.get_no_direita() ==null) && (no.get_no_esquerda() ==null);
        }
        
        public Nodo cria_No_externo(Nodo Nopai) // cria um Nodo externo
        {
            Nodo no = new Nodo();
            no.set_no_pai(Nopai);
            return no;
        }

        public void insere(int valor)// insere um valor int
        {
            Nodo no_aux;
            
            if(qtde == 0)
            {
                // árvore vazia, devemos criar o primeiro Nodo, que será a raiz
                no_aux = new Nodo();
                raiz = no_aux;
            }
            else
            {
                // localiza onde deve ser inserido o novo nó.
                no_aux = raiz;
                while(no_eh_externo(no_aux) == false)
                {
                    if(valor > no_aux.get_valor())
                        no_aux = no_aux.get_no_direita();
                    else
                        no_aux = no_aux.get_no_esquerda();
                }
            }
            // este era um Nodo externo e portanto não tinha filhos.
            // Agora ele passará a ter valor. Também devemos criar outros 2
            // Nodos externos (filhos) para ele.
            no_aux.set_valor(valor);
       
            no_aux.set_no_direita(cria_No_externo(no_aux));
        
            no_aux.set_no_esquerda(cria_No_externo(no_aux));
        
            qtde++;
        }
        
        private void Le_Nodo(Nodo no)
        {
            if(no_eh_externo(no))
                return;
        
            Le_Nodo(no.get_no_esquerda());
            
            resultado = resultado +" - "+Convert.ToInt32(no.get_valor());
            Le_Nodo(no.get_no_direita());
        }

        // devolve um string com os elementos da árvore, em ordem crescentepublic 
        public string listagem()
        {
            resultado ="";
            Le_Nodo(raiz);
        
            return resultado;
        }
    }
}
