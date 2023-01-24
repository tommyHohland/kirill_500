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
    public partial class MainForm : Form
    {
        void HideCells(int pos)
        {
            for (int i = pos; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }
        }
        kirillEntities conn = new kirillEntities();
        string _table;
        public MainForm()
        {
            InitializeComponent();
        }

        private void инвентарьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = conn.inventory.ToList();
            _table = "inventory";

            dataGridView1.Columns[0].HeaderText = "Номер";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].HeaderText = "Количество";
            dataGridView1.Columns[3].HeaderText = "Состояние";
            dataGridView1.Columns[4].HeaderText = "Срок эксплуатации";
            dataGridView1.Columns[5].HeaderText = "Тип";
            HideCells(6);
        }

        private void поставкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = conn.supply.ToList();
            _table = "supply";

            dataGridView1.Columns[0].HeaderText = "Номер";
            dataGridView1.Columns[1].HeaderText = "Номер товара";
            dataGridView1.Columns[2].HeaderText = "Номер поставщика";
            dataGridView1.Columns[3].HeaderText = "Дата";
            HideCells(4);
        }

        private void поставщикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = conn.Providers.ToList();
            _table = "provider";

            dataGridView1.Columns[0].HeaderText = "Номер";
            dataGridView1.Columns[1].HeaderText = "Наименование";
            dataGridView1.Columns[2].HeaderText = "Адрес";
            dataGridView1.Columns[3].HeaderText = "Телефон";
            dataGridView1.Columns[4].HeaderText = "Почта";
            HideCells(5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.SaveChanges();
        }

        private void типыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = conn.Types.ToList();
            _table = "types";
            dataGridView1.Columns[0].HeaderText = "Номер";
            dataGridView1.Columns[1].HeaderText = "Тип";
            HideCells(2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                switch (_table)
                {
                    case "types":
                        int id_type = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                        var item_type = conn.Types.Where(c => c.ID == id_type).FirstOrDefault();
                        var items_inventory = conn.inventory.Where(c => c.ID_Type == id_type).ToList();

                        foreach (inventory item in items_inventory)
                            conn.inventory.Remove(item);
                        conn.Types.Remove(item_type);

                        conn.SaveChanges();
                        dataGridView1.DataSource = conn.Types.ToList();
                        break;

                    case "provider":

                        int id_provider = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                        var item_provider = conn.Providers.Where(c => c.id == id_provider).FirstOrDefault();
                        var items_supply = conn.supply.Where(c => c.ID_provider == id_provider).ToList();
                        if (items_supply != null)
                        {
                            foreach (supply item in items_supply)
                                conn.supply.Remove(item);
                        }
                        conn.Providers.Remove(item_provider);

                        conn.SaveChanges();
                        dataGridView1.DataSource = conn.Providers.ToList();
                        break;

                    case "supply":
                        int id_supp = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                        var supp = conn.supply.Where(c => c.ID == id_supp).FirstOrDefault();

                        conn.supply.Remove(supp);

                        conn.SaveChanges();
                        dataGridView1.DataSource = conn.supply.ToList();
                        break;

                    case "inventory":
                        int id_inventory = int.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                        var item_inventory = conn.inventory.Where(c => c.ID == id_inventory).FirstOrDefault();
                        var items_charges = conn.charges.Where(c => c.ID_inventory == id_inventory).ToList();

                        foreach (charges item in items_charges)
                            conn.charges.Remove(item);
                        conn.inventory.Remove(item_inventory);

                        conn.SaveChanges();
                        dataGridView1.DataSource = conn.inventory.ToList();
                        break;
                    default:
                        MessageBox.Show("Что-то пошло не так");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:\n "+ex.Message);
            }
        }

        private void поставщикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddProvider win = new FormAddProvider();
            this.Hide();
            win.ShowDialog();
        }

        private void новоеОборуованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddInventory win = new FormAddInventory();
            this.Hide();
            win.ShowDialog();
        }

        private void новыйТипToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddType win = new FormAddType();
            this.Hide();
            win.ShowDialog();
        }

        private void заказатьИнвентарьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddSupply win = new FormAddSupply();
            this.Hide();
            win.ShowDialog();
        }
    }
}
