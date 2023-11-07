﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Messenger.DB;

namespace Messenger
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public static string host = Dns.GetHostName();
        public static IPAddress[] address = Dns.GetHostAddresses(host);
        public SignIn()
        {
            InitializeComponent();
        }

        public event EventHandler ButtonClicked;
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ApplicationContext())
            {
                var users = db.Users.ToList();

                foreach (Users u in users)
                {
                    if (LoginSignIn.Text == u.Login && PasswordSignIn.Password == u.Password)
                    {
                        DataBank.UserLog = u.Nickname;      //Указываем программе авторизированного пользователя

                        Users u1 = db.Users.FirstOrDefault();

                        u1.IP = address[4].ToString();
                        db.SaveChanges();   // сохраняем изменения

                        LoginSignIn.Text = null;            //Очищаем поля авторизации
                        PasswordSignIn.Password = null;     //   

                        MainMenu mainMenu = new MainMenu();
                        mainMenu.Show();
                        Application.Current.MainWindow.Close();
                    }
                }

                Password.Content = "PASSWORD - wrong username or password";
                Password.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                Login.Content = "LOGIN - wrong username or password";
                Login.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#C22F1F");

                PasswordSignIn.Password = null;//Очищаем поля авторизации
            }
        }

        private void OpenSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUp());
        }
    }
}
