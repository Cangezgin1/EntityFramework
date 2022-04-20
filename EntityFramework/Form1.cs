using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;



namespace EntityFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            using (var db = new StudentContext())
            {
                // GRİDTE LİSTELİYORUZ...
                dataGridView1.DataSource = db.Students.ToList();
            }
        }
        // KAYDET
        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new StudentContext())
            {
                // ÖĞRENCİ EKLEME
                var ogrenci = new Student { Adı = textBox1.Text, Soyadı = textBox2.Text };
                db.Students.Add(ogrenci);
                db.SaveChanges();
            }
            Form1_Load(this, null);

            textBox1.Clear();
            textBox2.Clear();
            textBox1.Select();
        }
        // ARA
        private void button4_Click(object sender, EventArgs e)
        {
            using (var db = new StudentContext())
            {
                var ogrenci = from b in db.Students
                              where b.Adı.StartsWith(textBox1.Text)
                              select b;
                foreach(var item in ogrenci)
                {
                    textBox1.Text = item.Adı;
                    textBox2.Text = item.Soyadı;
                }
            }
        }
        // GÜNCELLEME
        private void button2_Click(object sender, EventArgs e)
        {
            using (var db = new StudentContext())
            {
                var ogrenci = from b in db.Students
                              where b.Adı.StartsWith(textBox1.Text)
                              select b;
                foreach(var item in ogrenci)
                {
                    item.Adı = textBox1.Text;
                    item.Soyadı = textBox2.Text;
                }
                db.SaveChanges();
            }
            Form1_Load(this, null);
        }
        // SİL
        private void button3_Click(object sender, EventArgs e)
        {
            using (var db = new StudentContext())
            {
                var ogrenci = (from b in db.Students
                               where b.Adı.StartsWith(textBox1.Text)
                               select b).Single();
              //  var ogrenci1 = db.Students.FirstOrDefault(s => s.Adı == textBox1.Text);
                db.Students.Remove(ogrenci);
              //  db.Students.Remove(ogrenci1);
                db.SaveChanges();
            }
            Form1_Load(this, null);
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Select();
        }
    }

    public class Student
    {
        public int ID { get; set; }
        public string Adı { get; set; }
        public string Soyadı { get; set; }
    }
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}
