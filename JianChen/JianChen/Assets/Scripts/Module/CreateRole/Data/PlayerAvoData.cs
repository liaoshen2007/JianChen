//using cn.bmob.io;
//
//
//public class PlayerBmobData : BmobTable
//{
//
//    public string PlayerName { get; set; }
//    
//
//    public BmobInt Occupation   
//    {
//        get; set;
//    }
//    
//
//    public BmobInt Sexual   
//    {
//        get; set;
//    }
//    
//    //本来最好应该是map
//    public BmobInt Equip    
//    {
//        get; set;
//    }
//
//
//    public string UserName    
//    {
//        get; set;
//    }
//    
//
//    public BmobInt ServerId  
//    {
//        get; set;
//    }
//
//    public override void readFields(BmobInput input)
//    {
//        base.readFields(input);
//        this.PlayerName = input.getString("playerName");
//        this.Occupation = input.getInt("occupation");
//        this.Sexual = input.getInt("sexual");
//        this.Equip = input.getInt("equip");
//        this.UserName = input.getString("userName");
//        this.ServerId = input.getInt("serverId");
//    }
//
//    public override void write(BmobOutput output, bool all)
//    {
//        base.write(output, all);
//        output.Put("playerName",this.PlayerName);
//        output.Put("occupation",this.Occupation);
//        output.Put("sexual",this.Sexual);
//        output.Put("equip",this.Equip);
//        output.Put("userName",this.UserName);
//        output.Put("serverId",this.ServerId);
//    }
//
//
//}
