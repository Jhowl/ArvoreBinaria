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

        private bool flag = true;

        private int balance = 0;

        private string resultado = "";
        
        public int qtde_nos_internos() // devolve a qtde de nós internos
        { 
            return qtde;
        }

        public int qtde_de_comp() // devolve a qtde de nós internos
        {
            return balance;
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

            if (qtde == 0)
            {
                // árvore vazia, devemos criar o primeiro Nodo, que será a raiz
                no_aux = new Nodo();
                raiz = no_aux;
            }
            else
            {
                // localiza onde deve ser inserido o novo nó.
                no_aux = raiz;
                while (no_eh_externo(no_aux) == false)
                {
                    balance++;

                    if (valor > no_aux.get_valor())
                    {
                        no_aux = no_aux.get_no_direita();
                        if (flag) /*inseriu: verificar balanceamento*/
                            switch (no_aux.get_balance())
                            {
                                case -1: /*era mais alto à esq.: zera FB*/
                                    no_aux.set_balance(0);
                                    flag = false;
                                    break;
                                case 0:
                                    no_aux.set_balance(1);
                                    break;
                                /*direita fica maior: propaga verificação*/
                                case 1: /*FB(p) = 2 e p retorna balanceado*/
                                    CASO2(no_aux);
                                    flag = false;
                                    break;
                            }
                    }
                    else
                    {
                        no_aux = no_aux.get_no_esquerda();
                        if (flag)
                            switch (no_aux.get_balance())
                            {
                                case 1: /*mais alto a direita*/
                                    no_aux.set_balance(0); /*balanceou com ins. esq*/
                                    flag = false; /*interrompe propagação*/
                                    break;
                                case 0:
                                    no_aux.set_balance(-1); /*ficou maior à esq.*/
                                    break;
                                case -1: /*FB(p) = -2*/
                                    CASO1(no_aux); /*p retorna balanceado*/
                                    flag = false;
                                    break; /*não propaga mais*/
                            }
                    }
                }
            }
            // este era um Nodo externo e portanto não tinha filhos.
            // Agora ele passará a ter valor. Também devemos criar outros 2
            // Nodos externos (filhos) para ele.
            no_aux.set_valor(valor);
       
            no_aux.set_no_direita(cria_No_externo(no_aux));
        
            no_aux.set_no_esquerda(cria_No_externo(no_aux));

            flag = true;
        
            qtde++;
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

        private void rot_esq (Nodo p)
        {
            Nodo q, temp;
            q = p.get_no_direita();
            temp = q.get_no_esquerda();
            q.set_no_esquerda(p);
            p.set_no_direita(temp);
            p = q;
        }

       private void rot_dir(Nodo p)
        {
            Nodo q, temp;
            q = p.get_no_esquerda();
            temp = q.get_no_direita();
            q.set_no_direita(p);
            p.set_no_esquerda(temp);
            p = q;
        }
        private void rot_dir_esq(Nodo p)
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
        }

        void rot_esq_dir(Nodo p)
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

        // devolve um string com os elementos da árvore, em ordem crescentepublic 
    }
}
