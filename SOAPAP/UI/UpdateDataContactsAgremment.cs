using Newtonsoft.Json;
using SOAPAP.Enums;
using SOAPAP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOAPAP.UI
{
    public partial class UpdateDataContactsAgremment : Form
    {
        private List<Model.Client> Clients;
        Model.Client Client;
        List<Model.Contact> Contacts ;
        private RequestsAPI Requests = null;
        private string UrlBase = Properties.Settings.Default.URL;
        public UpdateDataContactsAgremment(List<Model.Client> Clients)
        {
            Requests = new RequestsAPI(UrlBase);
            this.Clients = Clients;
            
            InitializeComponent();
        }



        private void UpdateDataContactsAgremment_Load(object sender, EventArgs e)
        {

            int index = 0;
            
            
            if (Clients.Count> 0) {
                Clients.ForEach(x => {

                    List<object> item = new List<object>(){ x.Name + " " + x.LastName,x.TypeUser == "CLI01" ? "Propietario" : "Usuario "};

                  
                    var indexRow = TableClients.Rows.Add(item.ToArray());
                    TableClients.Rows[index].Tag = x.Id;


                    index++;



                });
                drawDataClient(Clients.FirstOrDefault());
            }
        }

        

        private void drawDataClient(Model.Client client)
        {
            if (client != null)
            {
                Client = JsonConvert.DeserializeObject<Model.Client> (JsonConvert.SerializeObject( client.Clone()));
                Model.Contact[] conta = new Model.Contact[client.Contacts.Count];
                client.Contacts.ToList().CopyTo(conta);
                Contacts = conta.ToList();


                int index = 1;
                panelContacts.Controls.Clear();
                lblContacto.Text = "Datos de contacto " + " " + client.Name + " " + client.LastName + " " + client.SecondLastName;
                txtEdit.Text = client.EMail;
                lblErrorEmail.Text = "";
                client.Contacts.ToList().ForEach(x =>
                {
                    var CloneText = createLabel();
                    var CloneTextBox = createtextBox(x);
                    
                    

                   




                    //CloneTextBox.Name = "Phone_" + x.Id;
                    CloneTextBox.Text = "Escriba el telefono";
                    CloneTextBox.Name = "textBox_" + x.Id;


                    CloneText.Text = "Telefono " + index;
                    CloneText.Name = "PhoneText_" + x.Id;
                    if (panelContacts.Controls.Count >0) {
                        CloneText.Location = new Point(CloneText.Location.X, CloneText.Location.Y + 64);
                        CloneTextBox.Location = new Point(CloneTextBox.Location.X, CloneTextBox.Location.Y +64);
                        //lblError.Location = new Point(lblErrorEmail.Location.X, CloneTextBox.Location.Y + 22);
                    }
                    else
                    {
                      // lblError.Location = new Point(lblErrorEmail.Location.X, lblErrorEmail.Location.Y-3);
                       
                    }
                   // lblError.Height = lblError.Height - 5;
                  

                    panelContacts.Controls.Add(CloneText);
                    panelContacts.Controls.Add(CloneTextBox);
                    //panelContacts.Controls.Add(lblError);
                    index++;
                });

            }
        }

        private Label createLabel()
        {
            var label = new Label();
            
            label.Location = lbltext.Location;
            label.Size = lbltext.Size;
            return label;
        }

        private GroupBox createtextBox(Model.Contact x)
        {
            var textBox = new GroupBox();
            textBox.Location = groupBox1.Location;
            textBox.Size = groupBox1.Size;
            textBox.Font = groupBox1.Font;

            var lblError = new Label();

            lblError.ForeColor = lblErrorEmail.ForeColor;
            lblError.Text = "";
            lblError.Location = new Point(lblErrorEmail.Location.X, lblErrorEmail.Location.Y);
            lblError.Size = lblErrorEmail.Size;
            lblError.Width = lblErrorEmail.Width + 100;
            lblError.Name = "PhoneError_" + x.Id;
           // lblError.Height = lblError.Height - 5;

            var text = new TextBox();
            text.Font = txtEdit.Font;
            text.Location = txtEdit.Location;
            text.Size = txtEdit.Size;
            text.Name = "Phone_" + x.Id;
            text.Text = x.PhoneNumber;
            var pict = new PictureBox();
            pict.Location = new Point( pictureBox4.Location.X, pictureBox4.Location.Y-5);
            //pict.Height = pict.Height - 5;
            //pict.Width = pict.Width - 5;
            //pict.Location.Y = pict.Location.Y - 5;
            pict.Size = pictureBox4.Size;
            pict.Image = pictureBox4.Image;
            textBox.Controls.Add(text);
            textBox.Controls.Add(pict);
            textBox.Name = "picy_" + x.Id;
            textBox.Controls.Add(lblError);
            return textBox;
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (!validarData())
            {
                return;
            }
            Client.LastName = "asas";
            Client.SecondLastName = "as";
            var data = new { ClientVM = Client, ContactVM = Contacts };
         
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var result = await Requests.SendURIAsync("/api/Agreements/UpdateContacts", HttpMethod.Post, Variables.LoginModel.Token, content);
            if (result.Contains("success"))
            {
               var cl = Variables.Agreement.Clients.Where(x => x.Id == Client.Id).FirstOrDefault();
                if (cl != null)
                {
                    cl.EMail = Client.EMail;
                    cl.Contacts = Contacts;
                }
                new MessageBoxForm("Éxito", "Información actualizada correctamente", TypeIcon.Icon.Success).ShowDialog();
            }
            else
            {
                new MessageBoxForm("Error", "Ocurrio un error al actualizar la información", TypeIcon.Icon.Cancel).ShowDialog();
               
            }
            

        }

        private bool validarData()
        {
            int numError = 0;
            Regex regex = new Regex(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.([a-zA-Z]{2,4})+$");
            if (txtEdit.Text != "" && !regex.IsMatch(txtEdit.Text))
            {
                lblErrorEmail.Text = "Este campo no es valido";
                numError++;


            }
            else
            {
                lblErrorEmail.Text = "";
                Client.EMail = txtEdit.Text;
            }
            
           
            regex = new Regex(@"[0-9]{10}$");
            Contacts.ForEach(x =>
            {
                var parentC = panelContacts.Controls.Find("textBox_" + x.Id, false);
               
                var control = parentC[0].Controls.Find("Phone_" + x.Id, false);
                var controlError = parentC[0].Controls.Find("PhoneError_" + x.Id, false);
                if (control.Length > 0 && control[0].Text != "" && !regex.IsMatch(control[0].Text))
                {
                    controlError[0].Text = "Este campo no es valido";
                    numError++;
                   
                }
                else
                {
                    x.PhoneNumber = control[0].Text;
                    if (controlError.Length > 0)
                    {
                        controlError[0].Text = "";
                    }

                }

            });
            if (numError > 0)
            {
                return false;
            }
            return true;

        }
        private void TableClients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            var row = TableClients.Rows[e.RowIndex];
            var client = this.Clients.Where(x => x.Id.ToString() == row.Tag.ToString()).FirstOrDefault();
            drawDataClient(client);
        }
    }
}
