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
    public partial class telaCadastrarClientes : Form
    {
        pro_chefeEntities3 bd = new pro_chefeEntities3();
        public telaCadastrarClientes()
        {
            InitializeComponent();
            btnSalvar.Click += salvarUsuario;
            montarTabela();
        }

        private void montarTabela()
        {
            dataGridView1.DataSource = bd.usuario.Select(p => new {
                p.id,
                p.nome,
                p.login,
                p.senha
            }).ToList();
        }

        private void salvarUsuario(object sender, EventArgs e)
        {
            if (txtConfirmarSenha.Text == txtSenhaCadastro.Text)
            {
                usuario novo = new usuario()
                {
                    nome = txtNomeCadastro.Text,
                    login = txtLoginCadastro.Text,
                    senha = txtSenhaCadastro.Text
                };
                bd.usuario.Add(novo);
                bd.SaveChanges();
                MessageBox.Show("Usuário salvo com sucesso");
                montarTabela();
            }
            else
            {
                MessageBox.Show("As senhas não são iguais");
            }
        }
        private void TelaCadastrar_Load(object sender, EventArgs e)
        {

        }
    }
}
