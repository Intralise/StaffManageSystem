using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyCourseWork.AuthorizationService.Entities
{
    public class ApplicationsEntity
    {
        [Key]
        public int AppId { get; set; }
        public string AppHeader { get; set; }
        public string Breakage { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }
        public string Room { get; set; }
        public string Answer { get; set; }
    }
}