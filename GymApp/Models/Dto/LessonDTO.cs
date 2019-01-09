using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymApp.Models.Dto
{
    public class LessonDTO
    {
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime? end { get; set; }
    }

    public class LessonResponse
    {
        public long id { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool editable { get; set; }
        public string color { get; set; }
        public string description { get; set; }
    }
}