using CRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            load();
            //loadDepartFilter();
            //cbxSearchIn.ItemsSource = elements;
            cbxDept.ItemsSource = PRN221_SE1751Context.Ins.Departments.Select(x => x.Name).ToList();
        }

        private void loadDepartFilter()
        {
            var dept = PRN221_SE1751Context.Ins.Departments.Select(x => x.Name).ToList();
            dept.Insert(0, "All");
            cbxDepartFilter.ItemsSource = dept;
            cbxDepartFilter.SelectedIndex = 0;
        }
        private void load()
        {
            var students = PRN221_SE1751Context.Ins.Students
                .Include(x => x.Depart)
                .ToList();
            dgvDisplay.ItemsSource = students;
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            //filter();
        }

        private void filter()
        {
            string dept = cbxDepartFilter.SelectedItem.ToString();
            if(dept.Equals("All"))
            {
                dgvDisplay.ItemsSource = PRN221_SE1751Context.Ins.Students
                    .Include(x => x.Depart)
                    .ToList();
            }
            else
            {
                string deptId = PRN221_SE1751Context.Ins.Departments
                    .FirstOrDefault(x => x.Name.Equals(dept)).Id;
                dgvDisplay.ItemsSource = PRN221_SE1751Context.Ins.Students
                    .Include(x => x.Depart)
                    .Where(x => x.DepartId.Equals(deptId))
                    .ToList();
            }
        }

        private void cbxDepartFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter();
        }

        private void cbxGenderFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        string[] elements = { "Id", "Name", "Gender", "DepartName", "Dob", "Gpa" };
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string search = txtSearch.Text;
            if(cbxSearchIn.SelectedValue.ToString().Equals(elements[1]))
            {
                var st = PRN221_SE1751Context.Ins.Students
                    .Include(x => x.Depart)
                    .Where(x => x.Name.Contains(search)).ToList();
                dgvDisplay.ItemsSource = st;
            }
            if(cbxSearchIn.SelectedValue.ToString().Equals(elements[3]))
            {
                var st = PRN221_SE1751Context.Ins.Students
                    .Include(x => x.Depart)
                    .Where(x => x.Depart.Name.Contains(search)).ToList();
                dgvDisplay.ItemsSource = st;
            }

        }

        private void dgvDisplay_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //var st = dgvDisplay.SelectedItem as Student;
            //if(st != null)
            //{
            //    txtId.Text = st.Id.ToString();
            //    txtName.Text = st.Name.ToString();
            //    rdbMale.IsChecked = st.Gender;
            //    rdbFemale.IsChecked = !st.Gender;
            //    txtGpa.Text = st.Gpa.ToString();
            //    dpkDob.SelectedDate = st.Dob.Value;
            //    //cbxDept.SelectedValue = PRN221_SE1751Context.Ins.Departments.Find(st.DepartId).Name;
            //    cbxDept.SelectedValue = st.Depart.Name;
            //}
            //else
            //{
            //    MessageBox.Show("null");
            //}
        }
        //BTVN kết hợp Gender và Department
        //theo điều kiện and và diều kiện or

        //search dob từ ngày đến ngày, gpa trong khoảng, 
        private Student getStudent()
        {
            try
            {
                int id = int.Parse(txtId.Text);
                string name = txtName.Text;
                bool gender = rdbMale.IsChecked.Value;
                string deptId = PRN221_SE1751Context.Ins.Departments
                    .FirstOrDefault(x => x.Name == cbxDept.SelectedItem.ToString()).Id;
                DateTime? dob = dpkDob.SelectedDate.Value;
                float gpa = float.Parse(txtGpa.Text);
                return new Student()
                {
                    Id = id,
                    Name = name,
                    Gender = gender,
                    DepartId = deptId,
                    Gpa = gpa,
                    Dob = dob,
                };
            }
            catch { return null; }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Student student = getStudent();
            if (student == null) { clearForm();  return; }
            var x = PRN221_SE1751Context.Ins.Students.Find(student.Id);
            if(x == null)
            {
                PRN221_SE1751Context.Ins.Students.Add(student);
                PRN221_SE1751Context.Ins.SaveChanges();
                load();
            }
            else
            {
                MessageBox.Show("student existed");
            }
        }
        private void clearForm()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtGpa.Text = string.Empty;
            rdbFemale.IsChecked = true;
            rdbMale.IsChecked = false;
            dpkDob.SelectedDate = null;
            cbxDept .SelectedIndex = 0;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student student = getStudent();
            if(student == null)
            { clearForm(); return; }
            var x = PRN221_SE1751Context.Ins.Students.Find(student.Id);
            
            if(x != null)
            {
                x.Name = student.Name;
                x.Gpa = student.Gpa;
                x.Dob = student.Dob;
                x.Gender = student.Gender;
                x.DepartId = student.DepartId;
                PRN221_SE1751Context.Ins.Students.Update(x);
                PRN221_SE1751Context.Ins.SaveChanges();
                load();
            }
            else
            {
                MessageBox.Show("student not existed");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = PRN221_SE1751Context.Ins.Students.Find(int.Parse(txtId.Text));
                MessageBoxResult result = MessageBox.Show("Do you want to delete this student?", "Confirmation", MessageBoxButton.YesNoCancel);
                if(x != null && result==MessageBoxResult.Yes)
                {
                    PRN221_SE1751Context.Ins.Students.Remove(x);
                    PRN221_SE1751Context.Ins.SaveChanges();
                    load();
                }
            }
            catch
            {
                clearForm(); return;
            }
        }
        //BTVN:
        //Gender: checkbox, combo, radio
        //Department: checkbox(chi chon dc 1), combo, radio
        //Filter: thay datagrid = listview
        //Binding du lieu voi gender la combobox 
    }
}