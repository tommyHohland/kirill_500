using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proga
{
    public partial class FormAddProvider : Form
    {
        kirillEntities conn = new kirillEntities();
        public FormAddProvider()
        {
            InitializeComponent();
        }

        private void FormAddProvider_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Providers providers = new Providers();
                providers.name = textBox1.Text;
                providers.adres = textBox2.Text;
                providers.phone = textBox3.Text;
                providers.email = textBox4.Text;
                conn.Providers.Add(providers);
                conn.SaveChanges();
                MessageBox.Show("Успешно");
            }catch (Exception ex)
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm win = new MainForm();
            this.Hide();
            win.ShowDialog();
        }
    }
}
