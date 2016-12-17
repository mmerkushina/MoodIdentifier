using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.UI
{
    class Validation
    {
        public bool IsValid(DateTime? date1,DateTime? date2)
        {
            return (date1 <= DateTime.Now.Date && date1 != null && date2 <= DateTime.Now.Date && date2 != null && date1<=date2);
        }

        public bool IsValid(string login)
        {
            return (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrEmpty(login));
        }
    }
}
