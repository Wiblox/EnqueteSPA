using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EnquteSPA
{
    public class LoginUser
    {
  
        public string user { get; set; }
        public string password { get; set; }
        public LoginUser(string user,string password)
        {
            this.user = user;
            this.password = password;
        }


        public bool CheckUser()
        {
            LoginUser user1 = JsonConvert.DeserializeObject<LoginUser>(File.ReadAllText("test.json"));
            return Equals(user1);
        }


        public void putUser(LoginUser user)
        {
            File.WriteAllText("test.json", JsonConvert.SerializeObject(user));
        }

        public override bool Equals(object obj)
        {
            return obj is LoginUser user &&
                   this.user == user.user &&
                   password == user.password;
        }
    }
}
