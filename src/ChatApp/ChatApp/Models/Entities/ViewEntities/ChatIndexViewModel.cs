using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Entities.ViewEntities
{
    public class ChatIndexViewModel
    {
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public IList<Detail> Details { get; set; }

        public ChatIndexViewModel()
        {
            Details = new List<Detail>();
        }

        public class Detail
        {
            public DateTime PostAt { get; set; }
            public string Message { get; set; }
            public string Name { get; set; }
            public string IconUrl { get; set; }
            public bool IsMine { get; set; }
        }
    }
}
