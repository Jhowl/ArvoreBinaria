using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WindowsFormsApplication2
{
    class ArvoreBin
    { 
        private Nodo raiz = null;// raiz da árvore
        
        private int qtde = 0;// qtde de nos internos

        private bool flag = false;

        private string resultado = "";
        
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
            Nodo no;

            if (raiz == null)
            {
                // árvore vazia, devemos criar o primeiro Nodo, que será a raiz
                no = new Nodo();
                raiz = no;
                _aloca(valor, raiz);
            }
            else
            {
                // localiza onde deve ser inserido o novo nó.
                raiz = insere_arvore(raiz, valor);
                raiz = CorrigeAVL(raiz);
                Seta_FB(raiz);
            }
        }

        public Nodo insere_arvore(Nodo no, int value)
        {
            if (no_eh_externo(no))
            {
                /*árvore vazia: insere e sinaliza alteração de FB*/
                no =_aloca(value, no); 
                flag = true;
                return no;
            }

            if (value > no.get_valor())
               no.set_no_direita(insere_arvore(no.get_no_direita(), value));

            if (value < no.get_valor())
               no.set_no_esquerda(insere_arvore(no.get_no_esquerda(), value));

            return no;
        }


        public Nodo CorrigeAVL(Nodo pNodo)
        {
            Nodo aux;

            if (pNodo != null)
            {
                pNodo.set_balance(Calcula_FB(pNodo));
                if (pNodo.get_balance() == 2)
                {
                    aux = pNodo.get_no_esquerda();
                    aux.set_balance(Calcula_FB(pNodo.get_no_esquerda()));
                    if (aux.get_balance() > 0)
                    {
                        pNodo = rot_dir(pNodo);
                    }
                    else
                    {
                        pNodo = rot_esq_dir(pNodo);
                    }
                }
                else if (pNodo.get_balance() == -2)
                {
                    aux = (pNodo.get_no_direita());
                    aux.set_balance(Calcula_FB(pNodo.get_no_direita()));
                    if (pNodo.get_balance() < 0)
                    {
                        pNodo = rot_esq(pNodo);
                    }
                    else
                    {
                        pNodo = rot_dir_esq(pNodo);
                    }
                }
                pNodo.set_no_esquerda(CorrigeAVL(pNodo.get_no_esquerda()));
                pNodo.set_no_direita(CorrigeAVL(pNodo.get_no_direita()));
            }
            return pNodo;
        }

        int Calcula_FB(Nodo pNodo)
        {
            if (pNodo == null) 
                return 0;
            return (Altura(pNodo.get_no_esquerda()) - Altura(pNodo.get_no_direita()));
        }

        int Altura(Nodo pNodo)
        {
            int Alt_Esq, Alt_Dir;
            if (pNodo == null) return 0;
            else
            {
                Alt_Esq = Altura(pNodo.get_no_esquerda());
                Alt_Dir = Altura(pNodo.get_no_direita());
                if (Alt_Esq > Alt_Dir)
                {
                    return (1 + Alt_Esq);
                }
                else
                {
                    return (1 + Alt_Dir);
                }
            }
        }

        void Seta_FB(Nodo pNodo)
        {
            if (pNodo != null)
            {
                pNodo.set_balance(Altura(pNodo.get_no_esquerda()) - Altura(pNodo.get_no_direita()));
                Seta_FB(pNodo.get_no_esquerda());
                Seta_FB(pNodo.get_no_direita());
            }
        }

        void CASO1(Nodo p)
        {
            /*x foi inserido à esq. de p e causou FB= -2*/
            Nodo u;
            u = p.get_no_esquerda();
            if (u.get_balance() == -1) /*caso sinais iguais e negativos: rotação à direita*/
                rot_dir(p);
            else /*caso sinais trocados: rotação dupla u + p*/
                rot_esq_dir(p);
           
            p.set_balance(0);
        }

        public void CASO2( Nodo no )
        {
            Nodo aux;

            aux = no.get_no_direita();

            if (aux.get_balance() == 1)
                rot_esq(no);
            else
                rot_dir_esq(no);

            aux.set_balance(0);
        }

        private Nodo rot_esq (Nodo p)
        {
            Nodo q, temp;
            q = p.get_no_direita();
            temp = q.get_no_esquerda();
            q.set_no_esquerda(p);
            p.set_no_direita(temp);
            p = q;
            return p;
        }

       private Nodo rot_dir(Nodo p)
        {
            Nodo q, temp;
            q = p.get_no_esquerda();
            temp = q.get_no_direita();
            q.set_no_direita(p);
            p.set_no_esquerda(temp);
            p = q;
            return p;
        }

        private Nodo  rot_dir_esq(Nodo p)
        {
            Nodo z, v;
            z = p.get_no_direita();
            v = z.get_no_esquerda();
            z.set_no_esquerda(v.get_no_direita());
            v.set_no_direita(z);
            p.set_no_direita(v.get_no_esquerda());
            v.set_no_esquerda(p);

            /*atualizar FB de z e p em função de FB de v – a nova raiz*/
            if (v.get_balance() == 1)
            {
                p.set_balance(-1);
                z.set_balance(0);
            }
            else
            {
                p.set_balance(0);
                z.set_balance(1);
            }
            p = v;
            return p;
        }

        public Nodo rot_esq_dir(Nodo p)
        {
            Nodo u, v;
            u = p.get_no_esquerda();
            v = u.get_no_direita();
            u.set_no_direita(v.get_no_esquerda());
            v.set_no_esquerda(u);
            p.set_no_esquerda(v);
            v.set_no_direita(p);

            /*atualizar FB de u e p em função de FB de v - a nova raiz*/
            if (v.get_balance() == -1)
            { /*antes: u^.bal=1 e p^.bal=-2*/
                u.set_balance(0);
                p.set_balance(1);
            }
            else
            {
                p.set_balance(0);
                u.set_balance(-1);
            }
            p = v;
            return p;
        }


        public string listagem()
        {
            Le_Nodo(raiz);
        
            return resultado;
        }

        private void Le_Nodo(Nodo no)
        {
            if (no_eh_externo(no))
                return;

            Le_Nodo(no.get_no_esquerda());

            resultado = resultado + " - " + Convert.ToInt32(no.get_valor());
            Le_Nodo(no.get_no_direita());
        }

        private Nodo _aloca(int valor, Nodo no_aux)
        {
            no_aux.set_valor(valor);

            no_aux.set_balance(0);

            no_aux.set_no_direita(cria_No_externo(no_aux));

            no_aux.set_no_esquerda(cria_No_externo(no_aux));

            return no_aux;
        }

        // devolve um string com os elementos da árvore, em ordem crescentepublic 
    }
}
