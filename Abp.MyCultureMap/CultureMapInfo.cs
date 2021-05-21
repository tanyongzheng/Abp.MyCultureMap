using System;
using System.Collections.Generic;

namespace Abp.MyCultureMap
{
    /// <summary>
    /// Abp语言映射信息
    /// </summary>
    public class CultureMapInfo
    {
        /// <summary>
        ///  Abp的语言，
        /// 如繁体中文表示为：zh-Hant
        /// 简体中文：zh-Hans
        /// 英文：en
        /// </summary>
        public string TargetCulture { get; set; }

        /// <summary>
        ///  HTTP请求附带的语言，如前端HTTP请求头accept-language: zh-CN
        /// 香港地区繁体：zh-HK
        /// 台湾地区繁体：zh-TW
        /// </summary>
        public List<string> SourceCultures { get; set; }
    }
}
