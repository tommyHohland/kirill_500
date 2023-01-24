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
    public partial class FormAddInventory : Form
    {
        kirillEntities conn = new kirillEntities();
        public FormAddInventory()
        {

            InitializeComponent();
            var types = conn.Types.Where(c => c.ID > 0).ToList();
            foreach (Types type in types)
                comboBox1.Items.Add(type.Type);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                inventory inventory = new inventory();
                inventory.Name = textBox1.Text;
                inventory.quantity = int.Parse(numericUpDown1.Value.ToString());
                inventory.condition = int.Parse(numericUpDown2.Value.ToString());
                inventory.lifetime = int.Parse(numericUpDown3.Value.ToString());
                var type = conn.Types.Where(c => c.Type == comboBox1.SelectedItem.ToString()).FirstOrDefault();
                inventory.ID_Type = type.ID;
                conn.inventory.Add(inventory);
                conn.SaveChanges();
                MessageBox.Show("Успех");
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
