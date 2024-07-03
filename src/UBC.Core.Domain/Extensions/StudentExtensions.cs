using System.ComponentModel.DataAnnotations;
using UBC.Core.Domain.Entities;

namespace UBC.Core.Domain.Extensions
{
    public static class StudentExtensions
    {
        public static void ValidateName(this StudentEntity studentEntity)
        {
            if (string.IsNullOrEmpty(studentEntity.Name))
                throw new ValidationException("O nome do estudante é obrigatório.");
        }

        public static void ValidateAge(this StudentEntity studentEntity)
        {
            if (studentEntity.Age <= 0)
                throw new ValidationException("A idade do estudante é obrigatória.");
        }

        public static void ValidateDateBirth(this StudentEntity studentEntity)
        {
            int age = GetAge(studentEntity.DateBirth);
            if (age < 0 || age > 150)
            {
                throw new ValidationException("A data de nascimento do estudante é obrigatória.");
            }
        }

        public static int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Now;
            int age = today.Year - birthDate.Year;
            if (today.Month < birthDate.Month || (today.Month == birthDate.Month && today.Day < birthDate.Day)) { age--; }
            return age;
        }

        public static void ValidateStudentId(this StudentEntity studentEntity)
        {
            if (studentEntity.Code == 0)
                throw new ValidationException("O código do estudante é obrigatório.");
        }
    }
}
