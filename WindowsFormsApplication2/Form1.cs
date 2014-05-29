using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private ArvoreBin minhaArvore = new ArvoreBin();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                DateTime t_begin;
                DateTime t_end;
                TimeSpan t_difference;
                t_begin = DateTime.Now;

                //inserção manual
                //int y = Convert.ToInt32(txtValor.Text);


                Random num = new Random();
                int random = 0;
                for ( int x = 1; x <= 10000000; x++ )
                {
                    random = num.Next(0, 1000);
                    //int y = Convert.ToInt32(txtValor.Text);
                    minhaArvore.insert(random);
                }

                t_end = DateTime.Now;
                t_difference = t_end.Subtract(t_begin);
                listBox1.Items.Add(t_difference.TotalSeconds.ToString("0.000000") + " segundos");
            }
            catch
            {
                MessageBox.Show("Valor inválido! Digite apenas números!");
            }

            txtValor.Clear();
            txtValor.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Qtde: " + minhaArvore.qtde_nos_internos());
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                int y = Convert.ToInt32(txtValor.Text);

                if (minhaArvore.Consulta(y) != null)
                    listBox1.Items.Add("Encontrado " + minhaArvore.nos_for_search());
                else
                    listBox1.Items.Add("Não encontrado");
            }
            catch
            {
                MessageBox.Show("Valor inválido! Digite apenas números!");
            }
        }


    }
}
