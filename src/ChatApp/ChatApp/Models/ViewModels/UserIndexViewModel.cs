using System.Collections.Generic;

namespace ChatApp.Models.ViewModels
{
    public class UserIndexViewModel
    {
        public IList<Detail> Details { get; set; }

        public UserIndexViewModel()
        {
            Details = new List<Detail>();
        }

        public class Detail
        {
            public string UserId { get; set; }
            public string Name { get; set; }
            public bool IsAdministrator { get; set; }
        }
    }
}