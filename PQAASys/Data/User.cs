using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace PQAASys
{
    public partial class User
    {
        public User()
        {
            Requests = new HashSet<Request>();
        }
        [HiddenInput(DisplayValue =false)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; } 
        [Required(ErrorMessage = "Поле 'Фамилия' обязательно для заполнения")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Поле 'Имя' обязательно для заполнения")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Поле 'Отчество' обязательно для заполнения")]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Выберите должность из списка")]
        public int Position { get; set; }
        [Required(ErrorMessage = "Выберите отдел из списка")]
        public int Department { get; set; }
        [Required(ErrorMessage = "Поле 'Логин' обязательно для заполнения")]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле 'Пароль' обязательно для заполнения")]
        public string PasswordHash { get; set; }

        public virtual Department DepartmentNavigation { get; set; }
        public virtual Position PositionNavigation { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
