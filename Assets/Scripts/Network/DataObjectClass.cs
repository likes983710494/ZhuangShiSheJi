using System;
using System.Collections.Generic;
/// <summary>
/// 固定表单数据类
/// </summary>
[Serializable]
public class FixedDataForm
{
    /// <summary>
    /// 实验空间用户账号
    /// </summary>
    public string Username;
    /// <summary>
    /// 实验名称：用户学习的实验名称（100字以内）
    /// </summary>
    public string Title;
    /// <summary>
    /// 子实验/模块名称：用户学习的子实验名称（100字以内）
    /// </summary>
    public string ChildProjectTitle;
    /// <summary>
    /// 实验状态：1 - 完成；2 - 未完成
    /// </summary>
    public int Status;
    /// <summary>
    /// 实验成绩：0 ~100，百分制
    /// </summary>
    public int Score;
    /// <summary>
    /// 实验开始时间：13位时间戳
    /// </summary>
    public long StartTime;
    /// <summary>
    /// 实验结束时间：13位时间戳
    /// </summary>
    public long EndTime;
    /// <summary>
    /// 实验用时：非零整数，单位秒
    /// </summary>
    public int TimeUsed;
    /// <summary>
    /// 接入平台编号：由“实验空间”分配给实验教学项目的编号
    /// </summary>
    public string Appid;
    /// <summary>
    /// 实验平台实验记录ID：平台唯一且由大小写字母、数字、“_”组成
    /// </summary>
    public string OriginId;
    /// <summary>
    /// 实验步骤记录
    /// </summary>
    public List<StepDataForm> Steps;
    /// <summary>
    /// 分组标识，最多20个字符
    /// </summary>
    public string Group_Id;
    /// <summary>
    /// 分组名称，最多20个字符
    /// </summary>
    public string Group_Name;
    /// <summary>
    /// 组里的角色，最多20个字符
    /// </summary>
    public string Role_In_Group;
    /// <summary>
    /// 组员名称标识
    /// </summary>
    public string Group_Members;
    /// <summary>
    /// 实验平台自定义扩展数据
    /// </summary>
    public string Ext_data;
}
[Serializable]
public class StepDataForm
{
    /// <summary>
    /// 实验步骤序号
    /// </summary>
    public int Seq;
    /// <summary>
    /// 步骤名称：100字以内
    /// </summary>
    public string Title;
    /// <summary>
    /// 实验开始时间：13位时间戳
    /// </summary>
    public long StartTime;
    /// <summary>
    /// 实验结束时间：13位时间戳
    /// </summary>
    public long EndTime;
    /// <summary>
    /// 实验步骤用时：非零整数，单位秒
    /// </summary>
    public int TimeUsed;
    /// <summary>
    /// 实验步骤合理用时：单位秒
    /// </summary>
    public int ExpectTime;
    /// <summary>
    /// 实验步骤满分：0 ~100，百分制
    /// </summary>
    public int MaxScore;
    /// <summary>
    /// 实验步骤得分：0 ~100，百分制
    /// </summary>
    public int Score;
    /// <summary>
    /// 实验步骤操作次数
    /// </summary>
    public int RepeatCount;
    /// <summary>
    /// 步骤评价：200字以内
    /// </summary>
    public string Evaluation;
    /// <summary>
    /// 赋分模型：200字以内
    /// </summary>
    public string ScoringModel;
    /// <summary>
    /// 备注：200字以内
    /// </summary>
    public string Remarks;
    /// <summary>
    /// 实验步骤的自定义扩展数据
    /// </summary>
    public string Ext_data;
}
