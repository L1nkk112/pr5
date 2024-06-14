using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsApp12
{
    public partial class Form1 : Form
    {
        List<Airplane<string, DateTime>> airplane = new List<Airplane<string, DateTime>>();
        ErrorProvider errorProvider = new ErrorProvider();
        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Short;
        }
        delegate void AddApplicationDelegate();
        private void AddApplicationLogic()
        {
            if (!String.IsNullOrEmpty(txtPassangerName.Text) &&
            !String.IsNullOrEmpty(txtFlightNumber.Text) &&
            !String.IsNullOrEmpty(txtDestination.Text))
            {
                if (dateTimePicker1.Value.Date == DateTime.Today)
                {
                    errorProvider.SetError(dateTimePicker1, "���� ������ �� ����� ��������� � ����������� �����.");
                    Task.Delay(2000).Wait();
                    errorProvider.SetError(dateTimePicker1, "");
                    return;
                }
                else if (dateTimePicker1.Value.Date < DateTime.Today)
                {
                    MessageBox.Show("�� �� �� �� ������ � ������� ������ =).");
                    Task.Delay(2000).Wait();
                    errorProvider.SetError(dateTimePicker1, "");
                    return;
                }
                Airplane<string, DateTime> newAirplane = new Airplane<string, DateTime>(txtDestination.Text, txtPassangerName.Text, txtFlightNumber.Text, dateTimePicker1.Value.Date);
                airplane.Add(newAirplane);
                UpdateAirplaneInformation();
            }
            else
            {
                errorProvider.SetError(txtFlightNumber, "������� ���������� ����� �����.");
                Task.Delay(2000).Wait();
                errorProvider.SetError(txtFlightNumber, "");
                errorProvider.SetError(txtPassangerName, "������� ���������� ��� ���������.");
                Task.Delay(2000).Wait();
                errorProvider.SetError(txtPassangerName, "");
                errorProvider.SetError(txtDestination, "");
                errorProvider.SetError(txtDestination, "������� ���������� ����� ����������.");
                Task.Delay(2000).Wait();
                errorProvider.SetError(txtDestination, "");
            }
        }
        private async void AddApplication()
        {
            AddApplicationDelegate addDelegate = new AddApplicationDelegate(AddApplicationLogic);
            await Task.Run(() =>
            {
                Invoke(addDelegate);
            });
        }
        delegate void UpdateInformationDelegate(System.Windows.Forms.TextBox textBox);
        private void UpdateInformation(System.Windows.Forms.TextBox textBox)
        {
            textBox.Clear();
            foreach (Airplane<string, DateTime> b in airplane)
            {
                string ApplicationsInfo = $"����� �����: {b.FlightNumber}\r\n��������: {b.PassangerName}\r\n����� ����������: {b.Destination}\r\n�������� ���� ������: {b.DepartureDate}\r\n";
                textBox.AppendText(ApplicationsInfo + Environment.NewLine);
            }
        }
        private void UpdateAirplaneInformation()
        {
            UpdateInformationDelegate updateDelegate = new UpdateInformationDelegate(UpdateInformation);
            updateDelegate.Invoke(txtApplications);
        }

        private void UpdateAllApplicationsInformation()
        {
            if (txtAllApplications.Text.Length > 0)
            {
                txtAllApplications.AppendText(Environment.NewLine + "-------------------------------------" + Environment.NewLine);
            }

            UpdateInformationDelegate updateDelegate = new UpdateInformationDelegate(UpdateInformation);
            updateDelegate.Invoke(txtAllApplications);
        }
        private void btnAllApplications_Click_1(object sender, EventArgs e)
        {
            UpdateAllApplicationsInformation();
        }
        delegate void SearchByFlightNumberDelegate(string flightNumber);
        private void SearchByFlightNumber(string flightNumber)
        {
            List<Airplane<string, DateTime>> flights = airplane.Where(a => a.FlightNumber == flightNumber).ToList();

            if (flights.Count > 0)
            {
                txtFlightInfo.Clear();
                foreach (Airplane<string, DateTime> flight in flights)
                {
                    string flightInfo = $"����� �����: {flight.FlightNumber}\r\n��������: {flight.PassangerName}\r\n����� ����������: {flight.Destination}\r\n�������� ���� ������: {flight.DepartureDate}\r\n";
                    txtFlightInfo.AppendText(flightInfo + Environment.NewLine);
                }
            }
            else
            {
                MessageBox.Show("������ � ����� ������� ����� �� �������.");
                txtFlightInfo.Clear();
            }
        }
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            SearchByFlightNumberDelegate searchDelegate = new SearchByFlightNumberDelegate(SearchByFlightNumber);
            string flightNumberToSearch = txtFlightSearch.Text;
            searchDelegate.Invoke(flightNumberToSearch);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnAddAplication_Click(object sender, EventArgs e)
        {
            AddApplication();
        }
        private void btnRemoveApplication_Click(object sender, EventArgs e)
        {
            txtApplications.Clear();
            airplane.Clear();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtAllApplications.Clear();
            txtApplications.Clear();
            txtDestination.Clear();
            txtFlightNumber.Clear();
            txtPassangerName.Clear();
            airplane.Clear();
        }
        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnClose_Click(sender, e);
        }

        
    }
}