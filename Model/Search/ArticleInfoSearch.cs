using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Search
{
    public class ArticleInfoSearch : BaseSearch
    {
        public string ArticelClassID { get; set; }
        public string ArticeTitle { get; set; }
        public string ArticelAuthor { get; set; }
        public string FormDatepicker { get; set; }
        public string ToDatepicker { get; set; }

    }
}
