//using UnityEngine;
//using System.Collections;
//using cn.bmob.io;
//
//
////这些Bomb的东西考虑统一放到MVC的Data目录中去！
//public class UserAccountTable : BmobUser
//{
//    /// <summary>
//    /// Bmob服务端我们定义的表名
//    /// </summary>
//    //public const string UserTable = "UserAccountInfo";
//
//
//    public BmobInt HasPlayer { get; set; }
//
//
//    //public string PlayerPassword { get; set; }
//
//
//    public override void readFields(BmobInput input)
//    {
//        base.readFields(input);
//        this.HasPlayer = input.getInt("hasPlayer");
//        //this.PlayerPassword = input.getString("PlayerPassword");
//    }
//
//    public override void write(BmobOutput output, bool all)
//    {
//        base.write(output, all);
//        output.Put("hasPlayer",this.HasPlayer);
//        //output.Put("PlayerPassword",this.PlayerPassword);
//    }
//}