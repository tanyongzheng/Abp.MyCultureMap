using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AbpvNext.MyCultureMap
{
    public class MyCultureMapOptions
    {
        /// <summary>
        /// 获取用于设置格式的区域性映射列表
        /// </summary>
        public List<CultureMapInfo> CulturesMaps { get; set; }

        /// <summary>
        /// 获取要用于文本的 ui 区域性的y映射列表，即 language;
        /// </summary>
        public List<CultureMapInfo> UiCulturesMaps { get; set; }

        /// <summary>
        /// 设置多语言映射
        /// 默认按照浏览器的语言标识替换简体中文，繁体中文，英语
        /// </summary>
        public void SetCultureMaps()
        {
            CulturesMaps = new List<CultureMapInfo>();
            UiCulturesMaps = new List<CultureMapInfo>();
            // 简体
            var zhHansCultureMapInfo = new CultureMapInfo
            {
                TargetCulture = "zh-Hans",
                SourceCultures = new List<string>
                {
                    "zh", "zh-CN"
                }
            };
            CulturesMaps.Add(zhHansCultureMapInfo);
            UiCulturesMaps.Add(zhHansCultureMapInfo);

            // 繁体
            var zhHantCultureMapInfo = new CultureMapInfo
            {
                TargetCulture = "zh-Hant",
                SourceCultures = new List<string>
                {
                    "zh-TW","zh-HK"
                }
            };

            CulturesMaps.Add(zhHantCultureMapInfo);
            UiCulturesMaps.Add(zhHantCultureMapInfo);

            // 英语
            var enCultureMapInfo = new CultureMapInfo
            {
                TargetCulture = "en",
                SourceCultures = new List<string>
                {
                    "en-US", "en-AU", "en-CA", "en-IN", "en-IE", "en-IE", "en-MY", "en-NZ", "en-SG" ,"en-ZA" ,"en-GB"
                }
            };
            CulturesMaps.Add(enCultureMapInfo);
            UiCulturesMaps.Add(enCultureMapInfo);
        }

        /// <summary>
        /// 设置多语言映射
        /// </summary>
        /// <param name="cultureMapInfos"></param>
        /// <param name="uiCultureMapInfos"></param>
        public void SetCultureMaps([NotNull]List<CultureMapInfo> cultureMapInfos,[NotNull]List<CultureMapInfo> uiCultureMapInfos)
        {
            if (cultureMapInfos.Count==0)
            {
                throw new ArgumentException($"The list count in the parameter '{nameof(cultureMapInfos)}' cannot be 0.");
            }
            if (uiCultureMapInfos.Count==0)
            {
                throw new ArgumentException($"The list count in the parameter '{nameof(uiCultureMapInfos)}' cannot be 0.");
            }
            CulturesMaps = cultureMapInfos;
            UiCulturesMaps = uiCultureMapInfos;
        }

    }
}