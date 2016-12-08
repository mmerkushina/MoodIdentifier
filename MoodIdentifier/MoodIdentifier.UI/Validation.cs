using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodIdentifier.UI
{
    class Validation
    {
        public bool IsValid(DateTime? date)
        {
            return (date <= DateTime.Now.Date && date != null);
        }

        public bool IsValid(string login)
        {
            return (!string.IsNullOrWhiteSpace(login) && !string.IsNullOrEmpty(login));
        }
    }
}
