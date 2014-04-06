using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                minhaArvore.insere(Convert.ToInt32(txtValor.Text));
                listBox1.Items.Add("Inserido: " + txtValor.Text);
            }
            catch
            {
                MessageBox.Show("Valor inválido! Digite apenas números!");
            }
            txtValor.Clear();
            txtValor.Focus();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Add(minhaArvore.listagem());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Qtde: " + minhaArvore.qtde_nos_internos());
        }
    }
}
