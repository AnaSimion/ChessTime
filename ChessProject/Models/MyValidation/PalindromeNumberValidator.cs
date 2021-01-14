using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessProject.Models.MyValidation
{
    public class PalindromeNumberValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var board = (Board)validationContext.ObjectInstance;
            int price = board.Price;
            bool cond = false;
            int aux = 0;
            int aux2 = price;

            while (aux2 > 0)
            {
                aux = aux * 10 + aux2 % 10;
                aux2 /= 10;
            }
            if (aux == price)
            {
                cond = true;
            }

            return cond ? ValidationResult.Success : new ValidationResult("This is not a palindrom number!");
        }
    }
}