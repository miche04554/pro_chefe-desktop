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
    public partial class telaMesas : Form
    {
        pro_chefeEntities9 bd = new pro_chefeEntities9();
        public telaMesas()
        {
            InitializeComponent();
            montaMesa();
            btnVoltar.Click += Voltar;
            btnNovaMesa.Click += novaMesa;
        }

        private void novaMesa(object sender, EventArgs e)
        {
            new telaCadastroMesas().Show();
            this.Close();
        }

        private void Voltar(object sender, EventArgs e)
        {
            this.Close();
            new telaPosLogin().Show();
        }

        private void montaMesa()
        {
            bd.mesa.ToList().ForEach(m =>
            {
                Panel p = new Panel();
                p.Width = 200;
                p.Height = 200;
                if (m.disponivel == true)
                {
                    p.BackColor = Color.Lime;
                }
                else
                {
                    p.BackColor = Color.Red;
                }
                p.Name = m.id.ToString();
                p.Click += selecionaMesa;
                PictureBox foto = new PictureBox();
                foto.Name = m.id.ToString();
                foto.Location = new Point(20, 30);
                foto.Height = 160;
                foto.Width = 160;
                foto.Click += selecionaMesaFoto;
                Label l = new Label();
                l.Height = 30;
                l.Width = 200;
                l.Text = $"\n         Mesa {m.id}                   {m.capacidade} lugares ";
                p.Controls.Add(l);
                foto.Image = Properties.Resources.mesa;
                foto.SizeMode = PictureBoxSizeMode.StretchImage;
                p.Controls.Add(foto);
                flowLayoutPanel1.Controls.Add(p);
            });
        }

        private void selecionaMesaFoto(object sender, EventArgs e)
        {
            PictureBox fotoSelecionada = (PictureBox)sender;
            mesa selecionada = bd.mesa.Find(Convert.ToInt32(fotoSelecionada.Name));
            new telaNovoPedido(selecionada).Show();
            this.Hide();
        }

        private void selecionaMesa(object sender, EventArgs e)
        {
            Panel mesaSelecionada = (Panel)sender;
            mesa selecionada = bd.mesa.Find(Convert.ToInt32(mesaSelecionada.Name));
            new telaNovoPedido(selecionada).Show();
            this.Hide();
        }
    }
}
