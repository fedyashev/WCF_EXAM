using System;
using System.Collections.Generic;
using System.Linq;
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
using System.ComponentModel;
using CarsApp.Entities;
using CarsApp.Store;
using CarsApp.Service;

namespace CarsApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppStore _store;

        public MainWindow()
        {
            var service = new ComplexService();

            _store = new AppStore();
            _store.Cars = new BindingList<Car>((List<Car>)service.GetCars());

            InitializeComponent();

            Render(_store);
        }

        // Helpers

        private void SetLoggedUserUI()
        {
            this.tbLogin.IsEnabled = false;
            this.tbPassword.IsEnabled = false;
            this.btnLogin.IsEnabled = false;
            this.btnLogout.IsEnabled = true;
            this.btnSignup.IsEnabled = false;

            this.grdLogin.Visibility = System.Windows.Visibility.Collapsed;
            this.grdLogout.Visibility = System.Windows.Visibility.Visible;
        }

        private void SetNotLoggedUserUI()
        {
            this.tbLogin.IsEnabled = true;
            this.tbLogin.Text = String.Empty;
            this.tbLogin.Focus();

            this.tbPassword.IsEnabled = true;
            this.tbPassword.Text = String.Empty;

            this.btnLogin.IsEnabled = true;
            this.btnLogout.IsEnabled = false;
            this.btnSignup.IsEnabled = true;

            this.grdLogin.Visibility = System.Windows.Visibility.Visible;
            this.grdLogout.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void Render(AppStore store)
        {
            if (store.Username != null)
            {
                SetLoggedUserUI();
            }
            else
            {
                SetNotLoggedUserUI();
            }

            this.lstCarsList.ItemsSource = _store.Cars;
        }

        // Handlers

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = this.tbLogin.Text;
                var password = this.tbPassword.Text;

                if (String.IsNullOrWhiteSpace(username) ||
                    String.IsNullOrWhiteSpace(password)) throw new ArgumentException("Incorrect username or password.");

                var service = new ComplexService();
                if (service.Authenticate(username, password))
                {
                    _store.Username = username;
                    this.lbUsername.Content = String.Format("Username: {0}", _store.Username);
                    MessageBox.Show(String.Format("User {0} was logged.", _store.Username), "Authentication");

                    Render(_store);
                }
                else
                {
                    MessageBox.Show("Authentication failed!", "Authentication");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _store.Username = null;

                Render(_store);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = this.tbLogin.Text;
                var password = this.tbPassword.Text;

                if (String.IsNullOrWhiteSpace(username) ||
                    String.IsNullOrWhiteSpace(password)) throw new ArgumentException("Incorrect username or password.");

                var service = new ComplexService();
                if (service.SignUp(username, password))
                {
                    MessageBox.Show("Registration success!", "Sign up");
                }
                else
                {
                    MessageBox.Show("Registration failed!");
                }

                Render(_store);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void btnCarAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_store.Username == null)
            {
                MessageBox.Show("You are not authorized!", "Message");
                return;
            }

            try
            {
                var model = this.tbCarModel.Text;
                var price = Decimal.Parse(this.tbCarPrice.Text);
                var year = Int32.Parse(this.tbCarModel.Text);

                if (String.IsNullOrWhiteSpace(model) || price < 0 || year < 0)
                {
                    MessageBox.Show("Incorrect model, price or year parameters.", "Message");
                    return;
                }

                var service = new ComplexService();
                if (service.AddCar(model, price, year))
                {
                    MessageBox.Show("Car added!", "Success");
                }
                else
                {
                    MessageBox.Show("Car not added!", "Failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't add a new car!", "Exception");
            }
        }

    }
}
