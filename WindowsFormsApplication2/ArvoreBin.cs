using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WindowsFormsApplication2
{
    class ArvoreBin
    {
        private Nodo root = null; // raiz da árvore

        private int qtde = 0; // qtde de nos internos

        private bool flag = false;

        private int search = 0;

        public int qtde_nos_internos() { return qtde; }

        public int nos_for_search() { return search; }

        public bool no_eh_externo(Nodo no)// verifica se um determinado Nodo é externo
        {
            return (no.get_no_direita() == null) && (no.get_no_esquerda() == null);
        }

        public Nodo cria_No_externo(Nodo Nopai) // cria um Nodo externo
        {
            Nodo no = new Nodo();
            //no.set_no_pai(Nopai);
            return no;
        }

        public void insert(int valor)// insere um valor int
        {
            Nodo no;

            if (root == null)
            {
                // árvore vazia, devemos criar o primeiro Nodo, que será a raiz
                no = new Nodo();
                root = no;
                _aloca(valor, root);
            }
            else
            {
                // localiza onde deve ser inserido o novo nó.
                root = insert_tree(root, valor);
            }
        }

        private Nodo insert_tree(Nodo no, int value)
        {

            if (no_eh_externo(no))
            {
                /*árvore vazia: insere e sinaliza alteração de FB*/
                no = _aloca(value, no);
                flag = true;
                qtde++;
                return no;
            }

            if (value > no.get_valor())
            {
                no.set_no_direita(insert_tree(no.get_no_direita(), value));
                if (flag) /*inseriu: verificar balanceamento*/
                    switch (no.get_balance())
                    {
                        case -1:
                            no.set_balance(0);
                            flag = false;
                            break;
                        case 0:
                            no.set_balance(1);
                            break;
                        /*direita fica maior: propaga verificação*/
                        case 1: /*FB(p) = 2 e p retorna balanceado*/
                            no = _SideRight(no);
                            flag = false;
                            break;
                    }
            }

            if (value < no.get_valor())
            {
                no.set_no_esquerda(insert_tree(no.get_no_esquerda(), value));
                if (flag)
                    switch (no.get_balance())
                    {
                        case 1: /*mais alto a direita*/
                            no.set_balance(0); /*balanceou com ins. esq*/
                            flag = false; /*interrompe propagação*/
                            break;
                        case 0:
                            no.set_balance(-1); /*ficou maior à esq.*/
                            break;
                        case -1: /*FB(p) = -2*/
                            no = _SideLeft(no); /*p retorna balanceado*/
                            flag = false;
                            break; /*não propaga mais*/
                    }
            }

            return no;
        }

        private Nodo _SideLeft(Nodo no)
        {
            /*x foi inserido à esq. de p e causou FB= -2*/
            Nodo u;
            u = no.get_no_esquerda();
            if (u.get_balance() == -1) /*caso sinais iguais e negativos: rotação à direita*/
                no = _rot_right(no);
            else /*caso sinais trocados: rotação dupla u + p*/
                no = _rot_left_right(no);

            no.set_balance(0);

            return no;
        }

        private Nodo _SideRight(Nodo no)
        {
            Nodo aux;

            aux = no.get_no_direita();

            if (aux.get_balance() == 1)
                aux = _rot_left(no);
            else
                aux = _rot_right_left(no);

            aux.set_balance(0);

            return aux;
        }

        private Nodo _rot_left(Nodo p)
        {
            Nodo q, temp;
            q = p.get_no_direita();
            temp = q.get_no_esquerda();
            q.set_no_esquerda(p);
            p.set_no_direita(temp);
            p = q;
            return p;
        }

        private Nodo _rot_right(Nodo p)
        {

            Nodo q, temp;
            q = p.get_no_esquerda();
            temp = q.get_no_direita();
            q.set_no_direita(p);
            p.set_no_esquerda(temp);
            p = q;
            return p;
        }

        private Nodo _rot_right_left(Nodo p)
        {
            Nodo z, v;

            
            z = _rot_right(p.get_no_direita());
            p.set_no_direita(z);
            v = _rot_left(p);
            
            /*
            z = p.get_no_direita();
            v = z.get_no_esquerda();

            z.set_no_esquerda(v.get_no_direita());
            p.set_no_direita(v.get_no_esquerda());

            /*atualizar FB de z e p em função de FB de v – a nova raiz*/

            if (v.get_balance() == -1)
            {
                p.set_balance(0);
                z.set_balance(1);
            }
            else
            {
                p.set_balance(-v.get_balance());
                z.set_balance(0);
            }

            v.set_no_direita(z);
            v.set_no_esquerda(p);
           
            p = v;
            return p;
        }

        private Nodo _rot_left_right(Nodo p)
        {
            Nodo u, v;
            
            u = _rot_left(p.get_no_esquerda());
            p.set_no_esquerda(u);
            v =_rot_right(p);
            /*
            
            u = p.get_no_esquerda();
            v = u.get_no_direita();

            u.set_no_direita(v.get_no_esquerda());
            p.set_no_esquerda(v.get_no_direita());

            /*atualizar FB de u e p em função de FB de v - a nova raiz*/
            /*antes: u^.bal=1 e p^.bal=-2*/

            if (v.get_balance() == -1)
            { 
                u.set_balance(0);
                p.set_balance(1);
            }
            else
            {
                p.set_balance(0);
                u.set_balance(-v.get_balance());
            }

            v.set_no_esquerda(u);
            v.set_no_direita(p);
            p = v;
            return p;
        }

        public Nodo Consulta(int ch)
        {
            search = 0;
            Nodo pNodo = root;
            while (!no_eh_externo(pNodo))
            {
                search++;
                if (ch == pNodo.get_valor())
                {
                    return pNodo;
                }
                else
                {
                    if (ch < pNodo.get_valor())
                    {
                        pNodo = pNodo.get_no_esquerda();
                    }
                    else
                    {
                        pNodo = pNodo.get_no_direita();
                    }
                }
            }
            return null;
        }

        private Nodo _aloca(int valor, Nodo no_aux)
        {
            no_aux.set_valor(valor);

            no_aux.set_balance(0);

            no_aux.set_no_direita(cria_No_externo(no_aux));

            no_aux.set_no_esquerda(cria_No_externo(no_aux));

            return no_aux;
        }
    }
}
