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
    public partial class FormAddType : Form
    {
        kirillEntities conn = new kirillEntities();
        public FormAddType()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Types type = new Types();
            type.Type = textBox1.Text;
            conn.Types.Add(type);
            conn.SaveChanges();
            MessageBox.Show("Успешно");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm win = new MainForm();
            this.Hide();
            win.ShowDialog();
        }
    }
}
