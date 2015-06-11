<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShouYinTai.ascx.cs"
    Inherits="Enow.TZB.Web.ShouYin.ShouYinTai" %>
<%--收银台用户控件--%>
<!-------散客收银------>
<div style="display: <%=Display%>;" class="list_table" id="div_shouyin_shouyintai">
    <h3 style="text-align:right;">
        应收款<span class="fontred price" id="span_shouyin_shouyintai_heji">0.00</span>元</h3>
    <div class="table_head" id="div_shouyin_shouyintai_toubu">
        <table cellspacing="0" cellpadding="0" border="0" width="100%" id="table_shouyin_shouyintai_toubu">
            <tr>
                <th>
                    名称
                </th>
                <th width="50">
                    单位
                </th>
                <th width="90">
                    数量
                </th>
                <th width="70">
                    单价
                </th>
                <th width="80">
                    应收款
                </th>
                <th width="80">
                    铁丝价
                </th>
                <th width="50">
                    操作
                </th>
            </tr>
        </table>
    </div>
    <div class="table_cont" id="div_shouyin_shouyintai_mingxi">
        <table cellspacing="0" cellpadding="0" width="100%" id="table_shouyin_shouyintai_mingxi">
        </table>
    </div>
</div>
