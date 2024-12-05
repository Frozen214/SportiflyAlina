using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using Sportifly;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestButton1_Click_AllFieldsEmpty()
        {
            // Arrange
            Registr form = new Registr();
            form.guna2TextBox1.Text = "";
            form.guna2TextBox2.Text = "";
            form.guna2TextBox3.Text = "";
            form.guna2TextBox4.Text = "";
            form.guna2TextBox5.Text = "";
            form.guna2TextBox6.Text = "";

            // Act
            var result = form.ValidateForm();

            // Assert
            Assert.AreEqual("Заполните все поля!", result);
        }

        [TestMethod]
        public void TestButton1_Click_PasswordsDoNotMatch()
        {
            // Arrange
            Registr form = new Registr();
            form.guna2TextBox1.Text = "Иванов Иван Иванович";
            form.guna2TextBox2.Text = "89642009785";
            form.guna2TextBox3.Text = "ivanov@bk.ru";
            form.guna2TextBox4.Text = "Yasaq$32";
            form.guna2TextBox5.Text = "Yasaq$32!";
            form.guna2TextBox6.Text = "DifferentPassword";

            // Act
            var result = form.ValidateForm();

            // Assert
            Assert.AreEqual("Пароли не совпадают!", result);
        }

        [TestMethod]
        public void TestButton1_Click_ValidInput()
        {
            // Arrange
            Registr form = new Registr();
            form.guna2TextBox1.Text = "Иванов Иван Иванович";
            form.guna2TextBox2.Text = "89642009785";
            form.guna2TextBox3.Text = "ivanov@bk.ru";
            form.guna2TextBox4.Text = "Yasaq$32";
            form.guna2TextBox5.Text = "ValidPassword123!";
            form.guna2TextBox6.Text = "ValidPassword123!";

            // Act
            var result = form.ValidateForm();

            // Assert
            Assert.IsNull(result); // Ожидаем, что ошибок нет
        }
    }

}


