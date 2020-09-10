using System;
using System.Collections.Generic;
using FrameWork.JianChen.Core;
using game.main;
using LitJson;

public class ConfigDataManager : Model {

    public void Reset()
    {
        
    }

    //每个model初始化的时候都要加载规则！
    //error 这个不能直接用泛型！！
    public static List<T> LoadRuleDataById<T>(string id,Action<T> onComplete)
    {
        try
        {
            string text = new AssetLoader().LoadTextSync(AssetLoader.GetConfigRulePate(id));//assetLoader;
            List<T> jsonObjectList = JsonMapper.ToObject<List<T>>(text);
            return jsonObjectList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    
    public static List<T> LoadPlayRuleDataById<T>(string id,Action<List<T>> onComplete)
    {
        try
        {
            string text = new AssetLoader().LoadTextSync(AssetLoader.GetConfigRulePate(id));//assetLoader;
            List<T> jsonObjectList = JsonMapper.ToObject<List<T>>(text);
            if (onComplete!=null)
            {
                onComplete(jsonObjectList);
            }
            
            return jsonObjectList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    
    public static List<T> LoadUserDataById<T>(string id,Action<List<T>> onComplete)
    {
        try
        {
            string text = new AssetLoader().LoadTextSync(AssetLoader.GetUserDataPath(id));//assetLoader;
            if (String.IsNullOrEmpty(text))
            {
                if (onComplete!=null)
                {
                    onComplete(null);
                }
                return null;
            }
            List<T> jsonObjectList = JsonMapper.ToObject<List<T>>(text);
            if (onComplete!=null)
            {
                onComplete(jsonObjectList);
            }
            
            return jsonObjectList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    
    public static T LoadMapDataById<T>(string id,Action<T> onComplete)
    {
        try
        {
            string text = new AssetLoader().LoadTextSync(AssetLoader.GetMapDataPath(id));//assetLoader;
            T jsonObjectList = JsonMapper.ToObject<T>(text);
            if (onComplete!=null)
            {
                onComplete(jsonObjectList);
            }
            
            return jsonObjectList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    
    public static List<T> LoadDialogDataById<T>(string id,Action<List<T>> onComplete)
    {
        try
        {
            string text = new AssetLoader().LoadTextSync(AssetLoader.GetDialogDataPath(id));//assetLoader;
            List<T> jsonObjectList = JsonMapper.ToObject<List<T>>(text);
            if (onComplete!=null)
            {
                onComplete(jsonObjectList);
            }
            
            return jsonObjectList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    
    
    
    /*litJson的大坑，只能读取list<string>或者list<Doublt>或者List<T>其他列表类型都不能序列化出来，超级巨坑！！*/
    

}
