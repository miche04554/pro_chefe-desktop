using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizzaria
{
    public partial class telaPosLogin : Form
    {
        public telaPosLogin()
        {
            InitializeComponent();
            lbluser.Text = telaLogin.logado.nome;
            btnlogoff.Click += logoff;
            button1.Click += EntrarTelaMesas;
            button2.Click += EntrarTelaEntregas;
            button3.Click += EntrarTelaClientes;
            button4.Click += EntrarTelaFuncionarios;
            button5.Click += EntrarTelaProdutos;
            btnCadastroUsuários.Click += entrarTelaCadastroUsuario;
            if (telaLogin.logado.tipo_usuario == 2) {
                btnCadastroUsuários.Visible = true;
            }
        }

        private void entrarTelaCadastroUsuario(object sender, EventArgs e)
        {
            new telaCadastroUsuario().Show();
            this.Close();
        }

        private void EntrarTelaProdutos(object sender, EventArgs e)
        {
            new telaEstoque().Show();
            this.Close();
        }

        private void EntrarTelaFuncionarios(object sender, EventArgs e)
        {
            new telaFuncionarios().Show();
            this.Close();
        }

        private void EntrarTelaClientes(object sender, EventArgs e)
        {
            new telaCadastroClientes().Show();
            this.Close();
        }

        private void EntrarTelaEntregas(object sender, EventArgs e)
        {
            new telaDelivery().Show();
            this.Close();
        }

        private void EntrarTelaMesas(object sender, EventArgs e)
        {
            new telaMesas().Show();
            this.Close();
        }

        private void logoff(object sender, EventArgs e)
        {
            telaLogin.logado = null;
            Environment.Exit(0);
        }
    }
}
