using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;


class AndroidChannelBuild : AndroidAutoBuild
{
    /////////////////////////////内网包///////////////////////////////////////////
    [MenuItem("Build System/Android渠道打包/内网/官网内网包", false, 0)]
    static void BuildBigStarOfficialIntranet()
    {
        ChangeCGFile("简体");
        GradleBuildCommand("official:assembleIntranet");
    }
    
    [MenuItem("Build System/Android渠道打包/内网/一键打包官网内网包", false, 10)]
    static void BuildBigStarOfficialIntranetOnekey()
    {
        Publishil2cppProject_32Bit();
        BuildBigStarOfficialIntranet();
    }
                                                     
    /////////////////////////////外网包///////////////////////////////////////////
    [MenuItem("Build System/Android渠道打包/官网/官网包", false, 0)]
    static void BuildBigStarOfficialRelease()
    {
        ChangeCGFile("简体");
        GradleBuildCommand("official:assembleRelease");
    }

    [MenuItem("Build System/Android渠道打包/官网/一键打包官网包", false, 10)]
    static void BuildBigStarOfficialReleaseOnekey()
    {
        Publishil2cppProject_32Bit();
        BuildBigStarOfficialRelease();
    }

    /**
    [MenuItem("Build System/Android渠道打包/发布所有渠道apk", false, 210)]
    public static void PublishAllApk()
    {
        PublishMono2xProject();

        GradleBuildCommand("assembleRelease");
    }
    */
    
}