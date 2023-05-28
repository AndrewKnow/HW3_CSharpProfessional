using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Customer
    {
        //������ �������������� �� �ustomers
        
        public long Id { get; init; }
        
        [Required]
        public string Firstname { get; init; }

        [Required]
        public string Lastname { get; init; }
    }
}