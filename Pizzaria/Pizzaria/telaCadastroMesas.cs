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
    public partial class telaCadastroMesas : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        mesa selecionada = new mesa();
        public telaCadastroMesas()
        {
            InitializeComponent();
            btnVoltar.Click += Voltar;
            btnNovaMesa.Click += novaMesa;
            btnExcluir.Click += ExcluirMesa;
            montaTabelaMesa();
        }
        private void ExcluirMesa(object sender, EventArgs e)
        {
            try
            {
                int idSelecionado = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                selecionada = bd.mesa.Where(u => u.id.Equals(idSelecionado)).FirstOrDefault();
                mesa excluirI = new mesa();
                produto_mesa exf = new produto_mesa();
                bd.mesa.ToList().ForEach(f =>
                {
                    if (f.id.Equals(idSelecionado))
                    {
                        excluirI = f;
                    }
                });
                bd.mesa.Remove(excluirI);
                bd.produto_mesa.ToList().ForEach(pm =>
                {
                    if (pm.id_mesa == idSelecionado)
                    {
                        exf = pm;
                        bd.produto_mesa.Remove(exf);
                    }
                });
                bd.SaveChanges();
                montaTabelaMesa();
            }
            catch
            {
                MessageBox.Show("Erro ao excluir");
            }
        }

        private void montaTabelaMesa()
        {
            dataGridView1.DataSource = bd.mesa.Select(p => new
            {
                p.id,
                p.capacidade,
                p.disponivel
            }).ToList();
        }
        private void novaMesa(object sender, EventArgs e)
        {
            mesa nova = new mesa();
            nova.disponivel = true;
            nova.capacidade = Convert.ToInt16(txtCapacidade.Text);
            nova.observacao = "";
            bd.mesa.Add(nova);
            bd.SaveChanges();
            montaTabelaMesa();
            bd.SaveChanges();
            MessageBox.Show("Mesa Salvo com Sucesso!");
        }
        private void Voltar(object sender, EventArgs e)
        {
            this.Close();
            new telaMesas().Show();
        }
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
