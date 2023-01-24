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
    public partial class FormAddSupply : Form
    {
        kirillEntities conn = new kirillEntities();
        DateTime date;
        Providers prov;
        int id;
        class inv
        {
            public string name;
            public int quantity;
            public inv(string t, int q)
            {
                name = t;
                quantity = q;
            }
        }
        List<inv> inventoryList = new List<inv>();  
        public FormAddSupply()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(comboBox1.Text);
                date = dateTimePicker1.Value;
                prov = conn.Providers.Where(c => c.name== comboBox1.Text).FirstOrDefault();
                id = prov.id;
                comboBox1.Enabled = false;
                dateTimePicker1.Enabled = false;
            }catch(Exception ex)
            {
                MessageBox.Show("Укажите дату и поставщика\n"+ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            inv item = new inv(comboBox2.Text, int.Parse(numericUpDown1.Value.ToString()));
            richTextBox1.Text += item.name + "\t" + item.quantity+"\n";
            inventoryList.Add(item);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < inventoryList.Count; i++)
                {
                    string tmp = inventoryList[i].name;
                    inventory item = conn.inventory.Where(c => c.Name == tmp).FirstOrDefault();
                    item.quantity += inventoryList[i].quantity;
                    supply supp = new supply();
                    supp.date = date;
                    supp.ID_inventory = item.ID;
                    supp.ID_provider = id;
                    supp.quantity = inventoryList[i].quantity;
                    conn.supply.Add(supp);
                    conn.SaveChanges();
                }
                MessageBox.Show("Успешно");
            }catch  (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MainForm win = new MainForm();
            this.Hide();
            win.Show();
        }

        private void FormAddSupply_Load(object sender, EventArgs e)
        {

            var providerlist = conn.Providers.Where(c => c.id > 0).ToList();
            var inventorylist = conn.inventory.Where(c => c.ID > 0).ToList();
            foreach (Providers item in providerlist) comboBox1.Items.Add(item.name);
            foreach (inventory item in inventorylist) comboBox2.Items.Add(item.Name);
        }
    }
}
