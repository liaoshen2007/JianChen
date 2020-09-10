using System.Collections.Generic;
using FrameWork.JianChen.Core;
using UnityEngine;

namespace JianChen.Data
{
    public class PoemGameData : Model
    {
        private Dictionary<int, PoemData> _poemGameDataDic;//全局诗词Data

        //这里可以拓展玩家数据相关的方法！
        public PoemGameData()
        {
            _poemGameDataDic=new Dictionary<int, PoemData>();
        }

        public void InitPoemGameData(List<PoemData> data)
        {
            foreach (var v in data)
            {
                //Debug.LogError(v.PoemContent);
                _poemGameDataDic.Add(v.PoemId,v);    
            }
            
        }
        
        /// <summary>
        /// 获取目标Id的诗句Data
        /// </summary>
        /// <param name="poemId"></param>
        /// <returns></returns>
        public PoemData GetPoemData(int poemId)
        {
            if (_poemGameDataDic.ContainsKey(poemId))
            {
                return _poemGameDataDic[poemId];
            }

            return null;
        }

        public Queue<string> GetRandomPoemParts(int poemId)
        {
            var targetPoem = GetPoemData(poemId);
            string[] poemarr = targetPoem.PoemContent.Split('，');//这个标点符号要小心啊！！是中文的逗号！
            Debug.LogError(poemarr.Length);
            var randomQueue=new Queue<string>();
            var randomPoemArr = GetRandomPoemArr(poemarr);
            foreach (var poem in randomPoemArr)
            {
//                Debug.LogError("EnPoem:"+poem);
                randomQueue.Enqueue(poem);
            }


            return randomQueue;
        }

        /// <summary>
        /// 随机算法
        /// </summary>
        /// <param name="strarr"></param>
        /// <returns></returns>
        public string[] GetRandomPoemArr(string[] strarr)
        {
            for (int i = 0; i < strarr.Length; i++)
            {
                string temp = strarr[i];
                int randomIndex = Random.Range(0, strarr.Length);
                strarr[i] = strarr[randomIndex];
                strarr[randomIndex] = temp;
            }

            return strarr;
        }

		
		

    }

}


