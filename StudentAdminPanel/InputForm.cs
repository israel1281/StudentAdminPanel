using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace StudentAdminPanel
{
    public partial class InputForm : Form
    {
        public static IMongoClient client = new MongoClient();

        public static IMongoDatabase db = client.GetDatabase("student_admin");

        public static IMongoCollection<Students> collection = db.GetCollection<Students>("student");
        public InputForm()
        {
            InitializeComponent();
        }

        public class Students
        {
            [BsonId]
            public ObjectId Id { get; set; }
            [BsonElement("Full Name")]
            public string FullName { get; set; }
            [BsonElement("Matric Number")]
            public string MatricNumber { get; set; }
            [BsonElement("Age")]
            public string Age { get; set; }
            [BsonElement("Gender")]
            public string Gender { get; set; }
            [BsonElement("State of Origin")]
            public string StateOfOrigin { get; set; }
            [BsonElement("Gpa")]
            public string Gpa { get; set; }

            public Students(string fullname, string matric_number, string age, string gender, string state_of_origin, string gpa)
            {
                FullName = fullname;
                MatricNumber = matric_number;   
                Age = age;
                Gender = gender;
                StateOfOrigin = state_of_origin;
                Gpa = gpa;
            }
        }

        private void InputForm_Load(object sender, EventArgs e)
        {

        }

        private void addData_Click(object sender, EventArgs e)
        {
            if(txtFullname.Text == "" && txtMatricNumber.Text == "" && txtStateOfOrigin.Text == "" && txtAge.Text == "" && txtGender.Text == "" && txtGpa.Text == "")
            {
                MessageBox.Show(
                    "Please fill a input fields",
                    "Data Registration Failed!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
               );
            }
            else
            {
                Students student = new Students(
                       txtFullname.Text,
                       txtMatricNumber.Text,
                       txtStateOfOrigin.Text,
                       txtAge.Text,
                       txtGender.Text,
                       txtGpa.Text
                    );

                collection.InsertOne(student);

                MessageBox.Show(
                    "Registration Successful please click ok to be redirected to login page!",
                    "Registration Success!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.None
                );

                readData();

                txtFullname.Text = "";
                txtMatricNumber.Text = "";
                txtStateOfOrigin.Text = "";
                txtAge.Text = "";
                txtGender.Text = "";
                txtGpa.Text = "";
            }
        }

        public void readData()
        {
            List<Students> list = collection.AsQueryable().ToList();
            dataGridView1.DataSource = list;
            txtFullname.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
            txtMatricNumber.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
            txtStateOfOrigin.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
            txtAge.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
            txtGender.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
            txtGpa.Text = dataGridView1.Rows[0].Cells[5].Value.ToString();
        }
    }
}
