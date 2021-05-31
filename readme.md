# AbpvNext.MyCultureMap

[![nuget](https://img.shields.io/nuget/v/AbpvNext.MyCultureMap.svg?style=flat-square)](https://www.nuget.org/packages/AbpvNext.MyCultureMap) 
[![stats](https://img.shields.io/nuget/dt/AbpvNext.MyCultureMap.svg?style=flat-square)](https://www.nuget.org/stats/packages/AbpvNext.MyCultureMap?groupby=Version)
[![License](https://img.shields.io/badge/license-Apache2.0-blue.svg)](https://github.com/tanyongzheng/Abp.MyCultureMap/blob/master/LICENSE)
![.net5.0](https://img.shields.io/badge/net-%3D5.0-green.svg)

## 介绍
Abp vNext多语言替换


## 使用说明

1. 安装nuget包： Install-Package AbpvNext.MyCultureMap

2. 配置服务：

    2.1 使用配置文件
```cs
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
      // 其他配置...

      Configure<MyCultureMapOptions>(configuration.GetSection("MyCultureMapOptions"));
    }
```
    配置文件：
```js
     "MyCultureMapOptions": {
        "CulturesMaps": [
          {
            "TargetCulture": "zh-Hans",
            "SourceCultures": [ "zh", "zh-CN" ]
          },
          {
            "TargetCulture": "zh-Hant",
            "SourceCultures": [ "zh-TW", "zh-HK" ]
          },
          {
            "TargetCulture": "en",
            "SourceCultures": [ "en-US", "en-AU", "en-CA", "en-IN", "en-IE", "en-IE", "en-MY", "en-NZ", "en-SG", "en-ZA", "en-GB" ]
          }
        ] ,
        "UiCulturesMaps": [
          {
            "TargetCulture": "zh-Hans",
            "SourceCultures": [ "zh", "zh-CN" ]
          },
          {
            "TargetCulture": "zh-Hant",
            "SourceCultures": [ "zh-TW", "zh-HK" ]
          },
          {
            "TargetCulture": "en",
            "SourceCultures": [ "en-US", "en-AU", "en-CA", "en-IN", "en-IE", "en-IE", "en-MY", "en-NZ", "en-SG", "en-ZA", "en-GB" ]
          }
        ]
      } 
    }
```


    2.2 使用默认配置（默认Chrome浏览器的语言标识）

```cs
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
      // 其他配置...

      Configure<MyCultureMapOptions>(options => options.SetCultureMaps());
    }
```


3. 启用多语言替换

```cs
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
      // 其他配置...

      if (env.IsDevelopment())
      {
          app.UseDeveloperExceptionPage();
      }

      app.UseAbpRequestLocalization();

      if (!env.IsDevelopment())
      {
          app.UseErrorPage();
      }

      app.UseCorrelationId();
      app.UseVirtualFiles();
      app.UseRouting();
      //启用多语言替换
      app.UseMyRequestLocalization();
      app.UseCors(DefaultCorsPolicyName);
      app.UseAuthentication();

      if (MultiTenancyConsts.IsEnabled)
      {
          app.UseMultiTenancy();
      }

      app.UseAuthorization();
      // Swagger设置 ...
      app.UseAuditing();
      app.UseAbpSerilogEnrichers();
      app.UseConfiguredEndpoints();
    }
```

