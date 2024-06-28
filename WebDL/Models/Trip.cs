using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDL.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày khởi hành")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày kết thúc")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập điểm đến")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng người tham gia")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng người tham gia phải là số nguyên dương")]
        public int Participants { get; set; }
    }
}
