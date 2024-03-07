using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniusIdiotConsoleApp
{
    public class UsersResultStorage
    {
        private User user;

        public UsersResultStorage(User user)
        {
            this.user = user;
        }

        public void Save()
        {
            DataHelper.SaveResult(user.Name, user.RightAnswers, user.Diagnosis);
        }

        public void Show()
        {
            DataHelper.ShowResults();
        }
    }
}
