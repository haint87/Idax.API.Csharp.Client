// ************************************************************************** //
// 项目名称：CoinTigerSDK
// 项目描述：
// 类 名 称：Timestamp
// 说    明：
// 作    者：Email@yangkaijin.cn
// 创建时间：2018-07-29
// 更新时间：2018-07-29
// ************************************************************************** //
using System;
using System.Text;

namespace Idax.Api.Csharp.Client
{
    public class Timestamp
    {
        public Int64 timestamp = 0;

        public static Timestamp FromString(string strResponseData)
        {
            Json.Dictionary dict = Json.ToDictionary(strResponseData);
            if (dict == null)
                return null;

            string _timestamp = Json.GetAt(dict, nameof(timestamp));
            if (string.IsNullOrEmpty(_timestamp))
                return null;

            return new Timestamp()
            {
                timestamp = Convert.ToInt64(_timestamp)
            };
        }
    }
}
