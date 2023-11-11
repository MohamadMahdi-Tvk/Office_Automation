using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Automation.ServiceLayer
{
    public class LetterService : EntityService<Letter>, ILetterService
    {
        OfficeAutomationContext db;
        public LetterService(OfficeAutomationContext context) : base(context)
        {
            db = new OfficeAutomationContext();
        }

        public IEnumerable<Letter> SearchLetter(string search)
        {
            return db.letters.Where(t => t.Title.Contains(search) || t.LetterContent.Contains(search) || t.Number.Contains(search)).Distinct();
        }
    }
}
